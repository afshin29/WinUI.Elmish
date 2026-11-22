namespace WinUI.Elmish.View

open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System.Runtime.CompilerServices


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
    
    let internal getDataTemplate (template: 'source -> #IVirtualNode) =
        DataTemplate(fun () ->
            let host = ContentControl(
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            )

            host.add_DataContextChanged(fun _ args ->
                host.Content <-
                    match args.NewValue with
                    | :? 'source as item -> item |> template |> renderNode
                    | _ -> null
            )
                
            host :> UIElement
        )

    let internal createTemplateSelector templates selector =
        let templates =
            templates
            |> Seq.map getDataTemplate
            |> Seq.toArray

        InternalTemplateSelector(templates, selector)
