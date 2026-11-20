module internal WinUI.Elmish.Diff

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System.Collections.Generic
open System.Collections.ObjectModel

open IdGenerator
open VirtualElements


type EventDiff =
    | Handle of VirtualEvent
    | UnHandle of key: string

type CollectionDiff =
    | InsertItem of index: int * item: obj
    | RemoveItem of index: int
    | MoveItem of oldIndex: int * newIndex: int
    | UpdateItem of index: int * newItem: obj

and ChildrenDiff =
    | AddChild of IVirtualNode
    | RemoveChild of IVirtualNode
    | UpdateChild of NodeDiff

and PropertyDiff =
    | SetDependencyProp of DependencyProperty * value: obj
    | ClearDependencyProp of DependencyProperty
    
    | SetClrProp of set: (UIElement -> obj -> unit) * value: obj
    | ClearClrProp of clear: (UIElement -> unit)
    
    | SetChildProp of NodeDiff * set: (UIElement -> UIElement -> unit) * get: (UIElement -> UIElement)

    | SetChildrenProp of IVirtualNode list * set: (UIElement -> UIElement list -> unit)
    | UpdateChildrenProp of ChildrenDiff list * get: (UIElement -> IList<UIElement>)
    | ClearChildrenProp of set: (UIElement -> UIElement list -> unit)
    
    | SetCollectionProp of items: obj list * set: (UIElement -> obj seq -> unit)
    | UpdateCollectionProp of CollectionDiff list * get: (UIElement -> ObservableCollection<obj>)
    | ClearCollectionProp of set: (UIElement -> obj seq -> unit)

and NodeDiff =
    | SkipNode
    | Replace of IVirtualNode
    | Patch of NodeId * PropertyDiff list * EventDiff list
    | Render of IVirtualNode
    | Destroy
    | MemoUnchanged
    | MemoChanged of IVirtualNode

type WindowDiff =
    {
        ContentDiff: NodeDiff
        TitleDiff: string option
        SizeDiff: Windows.Graphics.SizeInt32 option
        BackdropDiff: WindowBackdrop option

        RequestedTheme: ElementTheme option
    }


let private diffEvents oldEvents newEvents =
    let oldKeys = oldEvents |> List.map (fun e -> e.Key) |> Set.ofList
    let newKeys = newEvents |> List.map (fun e -> e.Key) |> Set.ofList

    let toBeRemoved =
        oldEvents
        |> List.filter (fun e -> not (newKeys.Contains e.Key))
        |> List.map (fun e -> UnHandle e.Key)

    let toBeAdded =
        newEvents
        |> List.filter (fun e -> not (oldKeys.Contains e.Key))
        |> List.map Handle

    toBeRemoved @ toBeAdded


let private lisPositions (xs: int[]) : HashSet<int> =
    let tails = Array.zeroCreate<int> xs.Length
    let prev = Array.create xs.Length (-1)

    let rec lowerBound x lo hi =
        if lo >= hi then lo
        else
            let mid = (lo + hi) / 2
            if xs[tails[mid]] < x then lowerBound x (mid + 1) hi
            else lowerBound x lo mid

    let rec build i len =
        if i = xs.Length then len
        else
            let lo = lowerBound xs[i] 0 len
            if lo > 0 then prev[i] <- tails[lo - 1]
            tails[lo] <- i
            build (i + 1) (if lo = len then len + 1 else len)

    let rec collect k acc =
        if k < 0 then acc
        else collect prev[k] (k :: acc)

    let len = build 0 0
    let start = if len > 0 then tails[len - 1] else -1
    HashSet<int>(collect start [])

let private itemsDiffer (a: obj) (b: obj) = not (obj.Equals(a, b))

/// No keySelector: compare index by index, no moves.
let private diffByIndex (oldItems: obj[]) (newItems: obj[]) : CollectionDiff list =
    let common = min oldItems.Length newItems.Length

    let removes =
        [ oldItems.Length - 1 .. -1 .. common ]
        |> List.map RemoveItem

    let inserts =
        [ common .. newItems.Length - 1 ]
        |> List.map (fun i -> InsertItem(i, newItems[i]))

    let updates =
        [ 0 .. common - 1 ]
        |> List.filter (fun i -> itemsDiffer oldItems[i] newItems[i])
        |> List.map (fun i -> UpdateItem(i, newItems[i]))

    removes @ inserts @ updates

/// Keyed: removes -> inserts/moves (LIS) -> updates.
let private diffByKey
    (oldKeySelector: obj -> obj) (newKeySelector: obj -> obj)
    (oldItems: obj[]) (newItems: obj[]) : CollectionDiff list =

    let oldKeys = oldItems |> Array.map oldKeySelector
    let newKeys = newItems |> Array.map newKeySelector

    let oldIndexByKey = Dictionary<obj, int>()
    oldKeys |> Array.iteri (fun i k -> oldIndexByKey[k] <- i)
    let newKeySet = HashSet<obj>(newKeys)

    let removes =
        [ oldKeys.Length - 1 .. -1 .. 0 ]
        |> List.filter (fun i -> not (newKeySet.Contains oldKeys[i]))
        |> List.map RemoveItem

    let live0 = oldKeys |> Array.filter newKeySet.Contains |> List.ofArray
    let liveRank = Dictionary<obj, int>()
    live0 |> List.iteri (fun i k -> liveRank[k] <- i)

    let common =
        newKeys
        |> Array.mapi (fun j k ->
            match liveRank.TryGetValue k with
            | true, r -> Some (j, r)
            | _ -> None)
        |> Array.choose id

    let commonNewIdx = common |> Array.map fst
    let ranks = common |> Array.map snd

    let stable =
        lisPositions ranks
        |> Seq.map (fun c -> commonNewIdx[c])
        |> HashSet<int>

    let last = newKeys.Length - 1

    let indexOfKey (key: obj) live = live |> List.findIndex (fun x -> obj.Equals(x, key))

    let targetIndex live j =
        if j = last then List.length live else indexOfKey newKeys[j + 1] live

    let step (live: obj list, acc: CollectionDiff list) j =
        let key = newKeys[j]
        match liveRank.ContainsKey key with
        | true when stable.Contains j -> live, acc
        | true ->
            let oldIdx = indexOfKey key live
            let without = List.removeAt oldIdx live
            let target = targetIndex without j
            let acc' = if oldIdx <> target then MoveItem(oldIdx, target) :: acc else acc
            List.insertAt target key without, acc'
        | false ->
            let target = targetIndex live j
            List.insertAt target key live, InsertItem(target, newItems[j]) :: acc

    let _, movesRev =
        [ last .. -1 .. 0 ]
        |> List.fold step (live0, [])

    let updates =
        [ 0 .. newItems.Length - 1 ]
        |> List.choose (fun j ->
            match oldIndexByKey.TryGetValue newKeys[j] with
            | true, oi when itemsDiffer oldItems[oi] newItems[j] -> Some (UpdateItem(j, newItems[j]))
            | _ -> None)

    removes @ List.rev movesRev @ updates

let private diffCollectionItems
    (oldItems: obj list, oldKeySelector: (obj -> obj) option)
    (newItems: obj list, newKeySelector: (obj -> obj) option) : CollectionDiff list =

    let o, n = Array.ofList oldItems, Array.ofList newItems
    match oldKeySelector, newKeySelector with
    | Some oks, Some nks -> diffByKey oks nks o n
    | _ -> diffByIndex o n

    
let clearPropMap p =
    match p.Prop with
    | Dependency (dp, _) -> ClearDependencyProp dp
    | Clr (_, clear, _) -> ClearClrProp clear
    | InnerChild (_, set, clear, _) -> SetChildProp (Destroy, set, clear)
    | InnerChildren (_, set, _) -> ClearChildrenProp set
    | CollectionItems (_, _, set, _) -> ClearCollectionProp set


let setPropMap p =
    match p.Prop with
    | Dependency (dp, value) -> SetDependencyProp (dp, value)
    | Clr (set, _, value) -> SetClrProp (set, value)
    | InnerChild (node, set, clear, _) -> SetChildProp (Render node, set, clear)
    | InnerChildren (nodes, set, _) -> SetChildrenProp (nodes, set)
    | CollectionItems (items, _, set, _) -> SetCollectionProp (items, set)


let rec private diffNodes oldNode newNode : NodeDiff =
    let diffSameNodes (oldNode: IVirtualNode) (newNode: IVirtualNode) =
        let propDiffs = diffProperties oldNode.Props newNode.Props
        let eventDiffs = diffEvents oldNode.Events newNode.Events

        Patch (newNode.Id, propDiffs, eventDiffs)

    let tryDiffDifferentNodes (oldNode: IVirtualNode) (newNode: IVirtualNode) =
        if oldNode.Id <> newNode.Id then
            Some <| Replace newNode
        else
            None

    let tryDiffMemoizedNodes (oldNode: IVirtualNode) (newNode: IVirtualNode) =
        match oldNode.MemoizationKey, newNode.MemoizationKey with
        | Some key1, Some key2 when key1 = key2 -> Some MemoUnchanged
        | Some _, Some _ -> Some <| MemoChanged newNode
        | _ -> None

    let tryDiffConditionalNodes (oldNode: IVirtualNode) (newNode: IVirtualNode) =
        match oldNode.RenderCondition, newNode.RenderCondition with
        | Some false, Some false -> Some SkipNode
        | Some false, Some true -> Some <| Render newNode
        | Some true, Some false -> Some Destroy
        | _ -> None
    
    tryDiffDifferentNodes oldNode newNode
    |> Option.orElse (tryDiffMemoizedNodes oldNode newNode)
    |> Option.orElse (tryDiffConditionalNodes oldNode newNode)
    |> Option.defaultValue (diffSameNodes oldNode newNode)


and private diffProperties (oldProps: VirtualProperty list) (newProps: VirtualProperty list) =
    let oldMap = oldProps |> List.map (fun p -> p.Id, p) |> Map.ofList
    let newMap = newProps |> List.map (fun p -> p.Id, p) |> Map.ofList

    let toBeRemoved =
        oldProps
        |> List.filter (fun p -> not (newMap.ContainsKey p.Id))
        |> List.map clearPropMap
    
    let toBeAdded =
        newProps
        |> List.filter (fun p -> not (oldMap.ContainsKey p.Id))
        |> List.map setPropMap

    let toBeUpdated =
        newProps
        |> List.filter (fun p -> oldMap.ContainsKey p.Id)
        |> List.filter (fun p ->
            match oldMap[p.Id].DiffKey, p.DiffKey with
            | Some oldKey, Some newKey -> oldKey <> newKey
            | _ -> true
        )
        |> List.choose (fun newProp ->
            let oldProp = oldMap[newProp.Id]
            match oldProp.Prop, newProp.Prop with
            | Dependency (_, oldValue), Dependency (dp, newValue) ->
                if oldValue = newValue then None
                else Some <| SetDependencyProp (dp, newValue)

            | Clr (_, _, oldValue), Clr (set, _, newValue) ->
                if oldValue = newValue then None
                else Some <| SetClrProp (set, newValue)
            
            | InnerChild (oldNode, _, _, oldReplaceKey), InnerChild (newNode, set, clear, newReplaceKey) ->
                if oldReplaceKey = newReplaceKey then
                    SetChildProp (diffNodes oldNode newNode, set, clear)
                else
                    SetChildProp (Replace newNode, set, clear)
                |> Some
            
            | InnerChildren (oldNodes, _, _), InnerChildren (newNodes, _, get) ->
                Some <| UpdateChildrenProp (diffChildren oldNodes newNodes, get)
            
            | CollectionItems (oldItems, oldKeySelector, _, _), CollectionItems (newItems, newKeySelector, _, get) ->
                Some <| UpdateCollectionProp (diffCollectionItems (oldItems, oldKeySelector) (newItems, newKeySelector), get)

            | _ -> None
        )

    toBeRemoved @ toBeAdded @ toBeUpdated


and private diffChildren oldNodes newNodes : ChildrenDiff list =
    let rec moveForward oldList newList acc =
        match oldList, newList with
        | [], [] -> List.rev acc
        
        | [], n :: rest ->
            AddChild n :: acc
            |> moveForward [] rest
        
        | o :: rest, [] ->
            RemoveChild o :: acc
            |> moveForward rest []
        
        | o :: oldRest, n :: newRest ->
            UpdateChild (diffNodes o n) :: acc
            |> moveForward oldRest newRest

    moveForward oldNodes newNodes []


let diffWindow oldWindow newWindow =
    {
        ContentDiff = diffNodes oldWindow.Content newWindow.Content
        TitleDiff = if oldWindow.Title = newWindow.Title then None else Some newWindow.Title
        SizeDiff =
            match oldWindow.Size, newWindow.Size with
            | Some size1, Some size2 when size1 <> size2 -> Some size2
            | None, Some size2 -> Some size2
            | _ -> None
        BackdropDiff =
            match oldWindow.Backdrop, newWindow.Backdrop with
            | Some backdrop1, Some backdrop2 when backdrop1 <> backdrop2 -> Some backdrop2
            | None, Some backdrop -> Some backdrop
            | _ -> None

        RequestedTheme = newWindow.RequestedTheme
    }