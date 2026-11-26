namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System.Runtime.CompilerServices

open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Diff
open WinUI.Elmish.Reconciler
open WinUI.Elmish.Patcher


[<AutoOpen>]
module NodeExtensions =

    let getStaticResource<'t> (name: string) = Application.Current.Resources[name] :?> 't

    let private isPropValueNull p =
        match p.Prop with
        | Dependency (_, value) -> value
        | Clr (_, _, value) -> value
        | InnerChild (node, _, _, _) -> node
        | InnerChildren (nodes, _, _) -> nodes
        | CollectionItems (items, _, _, _) -> items
        |> isNull

    [<Extension>]
    type NodeExtensions =

        [<Extension>]
        static member internal set(node: VirtualNode<_>, prop) =
            if isPropValueNull prop then
                node
            else
                { node with
                    Props = prop :: node.Props }

        [<Extension>]
        static member setIf(node: VirtualNode<_>, condition: bool, setter: (VirtualNode<_> -> VirtualNode<_>)) =
            if condition then
                setter node
            else
                let existingIds =
                    node.Props
                    |> List.map (fun p -> p.Id)
                    |> Set.ofList

                let toUnset =
                    (setter node).Props
                    |> List.filter (fun p -> not (isPropValueNull p) && not (existingIds.Contains p.Id))

                { node with
                    MemoizedPropsToUnset = toUnset @ node.MemoizedPropsToUnset }

        [<Extension>]
        static member internal useDiffKey(prop: VirtualProperty, value: obj option) =
            { prop with
                DiffKey = value }

        [<Extension>]
        static member config(node: VirtualNode<_>, config: NodeConfig<_>) =
            { node with
                Configs = config :: node.Configs }

        [<Extension>]
        static member configIf(node: VirtualNode<_>, condition: bool, config: NodeConfig<_>) =
            if condition then node.config(config) else node

        [<Extension>]
        static member bindThemeResource<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, property: DependencyProperty, resourceName: string) =
            node.config(fun node -> node.SetThemeResourceBinding(property, resourceName))
                
        [<Extension>]
        static member handle(node: VirtualNode<_>, event) =
            { node with
                Events = event :: node.Events }
                
        [<Extension>]
        static member handleIf(node: VirtualNode<_>, condition: bool, event) =
            if condition then node.handle(event) else node

        [<Extension>]
        static member renderIf(node: VirtualNode<_>, condition: bool) =
            { node with
                RenderCondition = Some condition }

        [<Extension>]
        static member memoizeOn(node: VirtualNode<_>, key: obj) =
            { node with
                MemoizationKey = Some key }

        [<Extension>]
        static member memoize(node: VirtualNode<_>) =
            node.memoizeOn(0)

        [<Extension>]
        static member ref(node: VirtualNode<_>, weakRef: System.WeakReference<_>) =
            { node with
                Ref = Some weakRef }


module Common =

    let private applyProperties (uiElement: UIElement) (props: VirtualProperty seq) =        
        props
        |> Seq.iter (fun p ->
            match p.Prop with
            | Dependency (dp, value) -> uiElement.SetValue(dp, value)
            | Clr (set, _, value) -> set uiElement value
            | InnerChild (node, set, _, _) -> set uiElement (renderNode node)
            | InnerChildren (nodes, set, _) ->
                nodes
                |> List.map renderNode
                |> set uiElement
            | CollectionItems (items, _, set, _) -> set uiElement items
        )

    let private subscribeEvents (uiElement: UIElement) (events: VirtualEvent seq) =
        events
        |> Seq.iter (fun e ->
            e.Subscribe uiElement
            |> trackSubscription uiElement e.Key
        )

    let private applyConfigs (uiElement: 'element) (configs: NodeConfig<'element> seq) =
        configs
        |> Seq.iter (fun config -> config uiElement)


    let registerDefaultRenderer<'element
        when 'element : (new : unit -> 'element)
        and 'element :> UIElement
        and 'element: not struct>
        nodeId =
        
        renderers.Add(
            nodeId,
            fun node ->
                let element = new 'element()
            
                applyProperties element node.Props
                subscribeEvents element node.Events
                applyConfigs element (node :?> VirtualNode<'element>).Configs

                (node :?> VirtualNode<'element>).Ref
                |> Option.iter (fun ref -> ref.SetTarget(element))
                
                element :> UIElement
            )


    type internal InternalStyleSelector<'source>(styles: Style seq, selector: 'source -> int) =
        inherit StyleSelector()

        let styles = styles |> Seq.toArray

        override _.SelectStyleCore(item: obj, _) =
            item
            |> unbox<'source>
            |> selector
            |> min (styles.Length - 1)
            |> max 0
            |> Array.get styles

            
    type internal InternalTemplateSelector<'source>(templates: DataTemplate array, selector: 'source -> int) =
        inherit DataTemplateSelector()

        let select item =
            item
            |> unbox<'source>
            |> selector
            |> min (templates.Length - 1)
            |> max 0
            |> Array.get templates

        override _.SelectTemplateCore(item: obj) = select item
        override _.SelectTemplateCore(item: obj, _) = select item

    let internal getDataTemplate'<'el when 'el :> UIElement and 'el: not struct>
        (node: VirtualNode<'el>)
        (setBindings: ('el -> unit)) =
        
        DataTemplate(fun () ->
            let uiElement = renderNode node
            setBindings (uiElement :?> 'el)

            uiElement
        )
    
    let private refreshDiff (previous: IVirtualNode option) (node: IVirtualNode) : NodeDiff =
        let newPropIds = node.Props |> List.map (fun p -> p.Id) |> Set.ofList
        let newEventKeys = node.Events |> List.map (fun e -> e.Key) |> Set.ofList

        let staleProps =
            match previous with
            | Some old ->
                old.Props
                |> List.filter (fun p ->
                    match p.Prop with
                    | InnerChildren _ -> true
                    | _ -> not (newPropIds.Contains p.Id))
                |> List.map clearPropMap
            | None -> []

        let staleEvents =
            match previous with
            | Some old ->
                old.Events
                |> List.filter (fun e -> not (newEventKeys.Contains e.Key))
                |> List.map (fun e -> UnHandle e.Key)
            | None -> []

        Patch (
            node.Id,
            staleProps @ (node.Props |> List.map setPropMap),
            staleEvents @ (node.Events |> List.map Handle)
        )

    let internal getDataTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
        (template: 'source -> VirtualNode<'el>) =
        DataTemplate(fun () ->
            System.Diagnostics.Debug.WriteLine("---")
            System.Diagnostics.Debug.WriteLine("inside dt")
            let element = new 'el()
            let frameworkElement = element :> FrameworkElement
            let uiElement = frameworkElement :> UIElement
            let previous: IVirtualNode option ref = ref None

            frameworkElement.add_DataContextChanged(fun _ args ->
                System.Diagnostics.Debug.WriteLine($"inside handler: {args.NewValue}")
                args.Handled <- true

                match args.NewValue with
                | :? 'source as item ->
                    let node = template item

                    if previous.Value.IsNone then
                        applyConfigs element node.Configs

                    node.Ref
                    |> Option.iter (fun weakRef -> weakRef.SetTarget(element))

                    refreshDiff previous.Value node
                    |> applyDiff uiElement
                    |> ignore

                    previous.Value <- Some (node :> IVirtualNode)

                | _ -> ()
            )

            uiElement
        )

    let internal createTemplateSelector<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
        (templates: ('source -> VirtualNode<'el>) seq)
        (selector: 'source -> int) =
        let templates =
            templates
            |> Seq.map getDataTemplate
            |> Seq.toArray

        InternalTemplateSelector(templates, selector)
