module internal WinUI.Elmish.Patcher

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System.Collections.Generic

open VirtualElements
open EventSubscriptions
open Diff
open Reconciler


let private patchEvents (uiElement: UIElement) (diffs: EventDiff list) =
    diffs
    |> List.iter (
        function
        | Handle e ->
            disposeSubscription uiElement e.Key
            
            e.Subscribe uiElement
            |> trackSubscription uiElement e.Key
        | UnHandle key ->
            disposeSubscription uiElement key
    )


let rec applyDiff (target: UIElement) (diff: NodeDiff) : UIElement =
    match diff with
    | SkipNode | MemoUnchanged -> target

    | Replace node -> renderNode node

    | Patch (nodeId, propertyDiffs, eventDiffs) ->
        patchProperties target propertyDiffs
        patchEvents target eventDiffs

        target

    | MemoChanged node ->
        node.MemoizedPropsToUnset
        |> List.map clearPropMap
        |> patchProperties target

        node.Props
        |> List.map setPropMap
        |> patchProperties target
        
        target

    | Destroy -> null

    | Render node -> renderNode node


and private patchProperties (uiElement: UIElement) (diffs: PropertyDiff list) =
    diffs
    |> List.iter (
        function
        | SetDependencyProp (dp, value) -> uiElement.SetValue(dp, value)
        | ClearDependencyProp dp -> uiElement.ClearValue(dp)

        | SetClrProp (set, value) -> set uiElement value
        | ClearClrProp clear -> clear uiElement

        | SetChildProp (Destroy, set, _) -> set uiElement null
        | SetChildProp (Render node, set, _) | SetChildProp (Replace node, set, _) -> set uiElement (renderNode node)
        | SetChildProp (childDiff, set, get) -> ignore <| applyDiff (get uiElement) childDiff

        | SetChildrenProp (nodes, set) ->
            nodes
            |> List.map renderNode
            |> set uiElement
        | UpdateChildrenProp (childrenDiff, get) -> applyChildrenDiff (get uiElement) childrenDiff
        | ClearChildrenProp get -> (get uiElement).Clear()

        | SetCollectionProp (items, set) -> set uiElement items
        | UpdateCollectionProp (itemsDiff, get) -> applyItemsDiff (get uiElement) itemsDiff
        | ClearCollectionProp set -> set uiElement []
    )


and private applyChildrenDiff (children: IList<UIElement>) (diffs: ChildrenDiff list) =
    let rec moveForward list idx =
        match list with
        | [] -> ()

        | AddChild child :: rest | UpdateChild (Render child) :: rest ->
            let childElement = renderNode child
            children.Insert(idx, childElement)
            moveForward rest (idx + 1)

        | RemoveChild _ :: rest | UpdateChild Destroy :: rest ->
            children.RemoveAt(idx)
            moveForward rest idx

        | UpdateChild (Replace node) :: rest ->
            children[idx] <- (renderNode node)
            moveForward rest (idx + 1)
        
        | UpdateChild SkipNode :: rest ->
            moveForward rest idx
        
        | UpdateChild MemoUnchanged :: rest ->
            moveForward rest (idx + 1)
        
        | UpdateChild diff :: rest ->
            applyDiff (children[idx]) diff |> ignore
            moveForward rest (idx + 1)

    moveForward diffs 0


and private applyItemsDiff observable itemsDiff =
    itemsDiff
    |> List.iter (
        function
        | InsertItem (idx, item) -> observable.Insert(idx, item)
        | RemoveItem idx -> observable.RemoveAt(idx)
        | MoveItem (oldIdx, newIdx) -> observable.Move(oldIdx, newIdx)
        | UpdateItem (idx, item) -> observable[idx] <- item
    )


let applyWindowDiff (window: Window) (windowDiff: WindowDiff) =

    windowDiff.TitleDiff
    |> Option.iter (fun title -> window.Title <- title)

    windowDiff.SizeDiff
    |> Option.iter (fun size -> window.AppWindow.Resize(size))

    windowDiff.BackdropDiff
    |> Option.iter (setBackdrop window)
    
    window.Content <- applyDiff window.Content windowDiff.ContentDiff