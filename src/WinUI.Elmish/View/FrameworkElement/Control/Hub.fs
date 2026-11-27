namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property SectionHeaders: read-only


[<AutoOpen>]
module HubMod =

    let private hubNodeId = getNextId ()

    registerDefaultRenderer<Hub> hubNodeId

    let private defaultSectionIndexPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private isActiveViewPropertyId = getNextId ()
    let private isZoomedInViewPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private semanticZoomOwnerPropertyId = getNextId ()
    let private sectionsPropertyId = getNextId ()
    let private sectionsInViewPropertyId = getNextId ()

    [<Extension>]
    type HubExtensions =

        [<Extension>]
        static member defaultSectionIndex(node: VirtualNode<Hub>, value: int) =
            createDependencyProperty defaultSectionIndexPropertyId Hub.DefaultSectionIndexProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<Hub>, value: string) =
            createDependencyProperty str_headerPropertyId Hub.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Hub>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> Hub).Header <- child)
                (fun element -> (element :?> Hub).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<Hub>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId Hub.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member isActiveView(node: VirtualNode<Hub>, value: bool) =
            createDependencyProperty isActiveViewPropertyId Hub.IsActiveViewProperty value
            |> node.set

        [<Extension>]
        static member isZoomedInView(node: VirtualNode<Hub>, value: bool) =
            createDependencyProperty isZoomedInViewPropertyId Hub.IsZoomedInViewProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<Hub>, value: Orientation) =
            createDependencyProperty orientationPropertyId Hub.OrientationProperty value
            |> node.set

        [<Extension>]
        static member semanticZoomOwner(node: VirtualNode<Hub>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                semanticZoomOwnerPropertyId
                value
                (fun element child -> (element :?> Hub).SemanticZoomOwner <- (child :?> SemanticZoom))
                (fun element -> (element :?> Hub).SemanticZoomOwner :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member sections(node: VirtualNode<Hub>, value: IVirtualNode list) =
            createInnerChildrenProperty
                sectionsPropertyId
                value
                (fun element children ->
                    children
                    |> Seq.cast<HubSection>
                    |> Seq.toList
                    |> List.iter (element :?> Hub).Sections.Add)
                (fun element ->
                    (element :?> Hub).Sections
                    |> Seq.cast<UIElement>
                    |> ResizeArray
                    :> IList<UIElement>)
            |> node.set

        [<Extension>]
        static member sectionsInView(node: VirtualNode<Hub>, value: IVirtualNode list) =
            createInnerChildrenProperty
                sectionsInViewPropertyId
                value
                (fun element children ->
                    children
                    |> Seq.cast<HubSection>
                    |> Seq.toList
                    |> List.iter (element :?> Hub).SectionsInView.Add)
                (fun element ->
                    (element :?> Hub).SectionsInView
                    |> Seq.cast<UIElement>
                    |> ResizeArray
                    :> IList<UIElement>)
            |> node.set

        [<Extension>]
        static member onSectionHeaderClick(node: VirtualNode<Hub>, handler: Hub -> HubSectionHeaderClickEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SectionHeaderClick"
                Subscribe = (fun uiElement ->
                    let hub = uiElement :?> Hub
                    let h = HubSectionHeaderClickEventHandler(fun _ e -> handler hub e)
                    hub.SectionHeaderClick.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = hub.SectionHeaderClick.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onSectionsInViewChanged(node: VirtualNode<Hub>, handler: Hub -> SectionsInViewChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SectionsInViewChanged"
                Subscribe = (fun uiElement ->
                    let hub = uiElement :?> Hub
                    let h = SectionsInViewChangedEventHandler(fun _ e -> handler hub e)
                    hub.SectionsInViewChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = hub.SectionsInViewChanged.RemoveHandler h }
                )
            })

    let hub () =
        createNode<Hub> hubNodeId
