namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property AreHorizontalSnapPointsRegular: read-only
// skipped property AreVerticalSnapPointsRegular: read-only


[<AutoOpen>]
module ItemsPresenterMod =

    let private itemsPresenterNodeId = getNextId ()

    registerDefaultRenderer<ItemsPresenter> itemsPresenterNodeId

    let private str_footerPropertyId = getNextId ()
    let private vnode_footerPropertyId = getNextId ()
    let private footerTemplatePropertyId = getNextId ()
    let private footerTransitionsPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private headerTransitionsPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()

    [<Extension>]
    type ItemsPresenterExtensions =

        [<Extension>]
        static member footer(node: VirtualNode<ItemsPresenter>, value: string) =
            createDependencyProperty str_footerPropertyId ItemsPresenter.FooterProperty value
            |> node.set

        [<Extension>]
        static member footer<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<ItemsPresenter>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_footerPropertyId
                value
                (fun element child -> (element :?> ItemsPresenter).Footer <- child)
                (fun element -> (element :?> ItemsPresenter).Footer :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member footerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<ItemsPresenter>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty footerTemplatePropertyId ItemsPresenter.FooterTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member footerTransitions(node: VirtualNode<ItemsPresenter>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty footerTransitionsPropertyId ItemsPresenter.FooterTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<ItemsPresenter>, value: string) =
            createDependencyProperty str_headerPropertyId ItemsPresenter.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<ItemsPresenter>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> ItemsPresenter).Header <- child)
                (fun element -> (element :?> ItemsPresenter).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<ItemsPresenter>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId ItemsPresenter.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member headerTransitions(node: VirtualNode<ItemsPresenter>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty headerTransitionsPropertyId ItemsPresenter.HeaderTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<ItemsPresenter>, value: Thickness) =
            createDependencyProperty paddingPropertyId ItemsPresenter.PaddingProperty value
            |> node.set

        [<Extension>]
        static member onHorizontalSnapPointsChanged(node: VirtualNode<ItemsPresenter>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "HorizontalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let itemsPresenter = uiElement :?> ItemsPresenter
                    let h = EventHandler<obj>(fun _ e -> handler e)
                    itemsPresenter.HorizontalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = itemsPresenter.HorizontalSnapPointsChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onVerticalSnapPointsChanged(node: VirtualNode<ItemsPresenter>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "VerticalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let itemsPresenter = uiElement :?> ItemsPresenter
                    let h = EventHandler<obj>(fun _ e -> handler e)
                    itemsPresenter.VerticalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = itemsPresenter.VerticalSnapPointsChanged.RemoveHandler h }
                )
            })

    let itemsPresenter () =
        createNode<ItemsPresenter> itemsPresenterNodeId
