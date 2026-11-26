namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property SelectedItems: read-only
// skipped property SelectedRanges: read-only


[<AutoOpen>]
module ListViewBaseMod =

    let private canDragItemsPropertyId = getNextId ()
    let private canReorderItemsPropertyId = getNextId ()
    let private dataFetchSizePropertyId = getNextId ()
    let private str_footerPropertyId = getNextId ()
    let private vnode_footerPropertyId = getNextId ()
    let private footerTemplatePropertyId = getNextId ()
    let private footerTransitionsPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private headerTransitionsPropertyId = getNextId ()
    let private incrementalLoadingThresholdPropertyId = getNextId ()
    let private incrementalLoadingTriggerPropertyId = getNextId ()
    let private isActiveViewPropertyId = getNextId ()
    let private isItemClickEnabledPropertyId = getNextId ()
    let private isMultiSelectCheckBoxEnabledPropertyId = getNextId ()
    let private isSwipeEnabledPropertyId = getNextId ()
    let private isZoomedInViewPropertyId = getNextId ()
    let private reorderModePropertyId = getNextId ()
    let private selectionModePropertyId = getNextId ()
    let private semanticZoomOwnerPropertyId = getNextId ()
    let private showsScrollingPlaceholdersPropertyId = getNextId ()
    let private singleSelectionFollowsFocusPropertyId = getNextId ()

    [<Extension>]
    type ListViewBaseExtensions =

        [<Extension>]
        static member canDragItems<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty canDragItemsPropertyId ListViewBase.CanDragItemsProperty value
            |> node.set

        [<Extension>]
        static member canReorderItems<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty canReorderItemsPropertyId ListViewBase.CanReorderItemsProperty value
            |> node.set

        [<Extension>]
        static member dataFetchSize<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty dataFetchSizePropertyId ListViewBase.DataFetchSizeProperty value
            |> node.set

        [<Extension>]
        static member footer<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty str_footerPropertyId ListViewBase.FooterProperty value
            |> node.set

        [<Extension>]
        static member footer<'m, 'vnode when 'm :> ListViewBase and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_footerPropertyId
                value
                (fun element child -> (element :?> ListViewBase).Footer <- child)
                (fun element -> (element :?> ListViewBase).Footer :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member footerTemplate<'m, 'source, 'el when 'm :> ListViewBase and 'm: not struct and 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<'m>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty footerTemplatePropertyId ListViewBase.FooterTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member footerTransitions<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty footerTransitionsPropertyId ListViewBase.FooterTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member header<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty str_headerPropertyId ListViewBase.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'m, 'vnode when 'm :> ListViewBase and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> ListViewBase).Header <- child)
                (fun element -> (element :?> ListViewBase).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'m, 'source, 'el when 'm :> ListViewBase and 'm: not struct and 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<'m>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId ListViewBase.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member headerTransitions<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty headerTransitionsPropertyId ListViewBase.HeaderTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member incrementalLoadingThreshold<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty incrementalLoadingThresholdPropertyId ListViewBase.IncrementalLoadingThresholdProperty value
            |> node.set

        [<Extension>]
        static member incrementalLoadingTrigger<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: IncrementalLoadingTrigger) =
            createDependencyProperty incrementalLoadingTriggerPropertyId ListViewBase.IncrementalLoadingTriggerProperty value
            |> node.set

        [<Extension>]
        static member isActiveView<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isActiveViewPropertyId ListViewBase.IsActiveViewProperty value
            |> node.set

        [<Extension>]
        static member isItemClickEnabled<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isItemClickEnabledPropertyId ListViewBase.IsItemClickEnabledProperty value
            |> node.set

        [<Extension>]
        static member isMultiSelectCheckBoxEnabled<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isMultiSelectCheckBoxEnabledPropertyId ListViewBase.IsMultiSelectCheckBoxEnabledProperty value
            |> node.set

        [<Extension>]
        static member isSwipeEnabled<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isSwipeEnabledPropertyId ListViewBase.IsSwipeEnabledProperty value
            |> node.set

        [<Extension>]
        static member isZoomedInView<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isZoomedInViewPropertyId ListViewBase.IsZoomedInViewProperty value
            |> node.set

        [<Extension>]
        static member reorderMode<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: ListViewReorderMode) =
            createDependencyProperty reorderModePropertyId ListViewBase.ReorderModeProperty value
            |> node.set

        [<Extension>]
        static member selectionMode<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: ListViewSelectionMode) =
            createDependencyProperty selectionModePropertyId ListViewBase.SelectionModeProperty value
            |> node.set

        [<Extension>]
        static member semanticZoomOwner<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                semanticZoomOwnerPropertyId
                value
                (fun element child -> (element :?> ListViewBase).SemanticZoomOwner <- (child :?> SemanticZoom))
                (fun element -> (element :?> ListViewBase).SemanticZoomOwner :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member showsScrollingPlaceholders<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty showsScrollingPlaceholdersPropertyId ListViewBase.ShowsScrollingPlaceholdersProperty value
            |> node.set

        [<Extension>]
        static member singleSelectionFollowsFocus<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty singleSelectionFollowsFocusPropertyId ListViewBase.SingleSelectionFollowsFocusProperty value
            |> node.set

        [<Extension>]
        static member onChoosingGroupHeaderContainer<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: ChoosingGroupHeaderContainerEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ChoosingGroupHeaderContainer"
                Subscribe = (fun uiElement ->
                    let listViewBase = uiElement :?> ListViewBase
                    let h = TypedEventHandler<ListViewBase, ChoosingGroupHeaderContainerEventArgs>(fun _ e -> handler e)
                    listViewBase.add_ChoosingGroupHeaderContainer h
                    { new IDisposable with
                        member _.Dispose() = listViewBase.remove_ChoosingGroupHeaderContainer h }
                )
            })

        [<Extension>]
        static member onChoosingItemContainer<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: ChoosingItemContainerEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ChoosingItemContainer"
                Subscribe = (fun uiElement ->
                    let listViewBase = uiElement :?> ListViewBase
                    let h = TypedEventHandler<ListViewBase, ChoosingItemContainerEventArgs>(fun _ e -> handler e)
                    listViewBase.add_ChoosingItemContainer h
                    { new IDisposable with
                        member _.Dispose() = listViewBase.remove_ChoosingItemContainer h }
                )
            })

        [<Extension>]
        static member onContainerContentChanging<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: ContainerContentChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContainerContentChanging"
                Subscribe = (fun uiElement ->
                    let listViewBase = uiElement :?> ListViewBase
                    let h = TypedEventHandler<ListViewBase, ContainerContentChangingEventArgs>(fun _ e -> handler e)
                    listViewBase.add_ContainerContentChanging h
                    { new IDisposable with
                        member _.Dispose() = listViewBase.remove_ContainerContentChanging h }
                )
            })

        [<Extension>]
        static member onDragItemsCompleted<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: DragItemsCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragItemsCompleted"
                Subscribe = (fun uiElement ->
                    let listViewBase = uiElement :?> ListViewBase
                    let h = TypedEventHandler<ListViewBase, DragItemsCompletedEventArgs>(fun _ e -> handler e)
                    listViewBase.add_DragItemsCompleted h
                    { new IDisposable with
                        member _.Dispose() = listViewBase.remove_DragItemsCompleted h }
                )
            })

        [<Extension>]
        static member onDragItemsStarting<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: DragItemsStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragItemsStarting"
                Subscribe = (fun uiElement ->
                    let listViewBase = uiElement :?> ListViewBase
                    let h = DragItemsStartingEventHandler(fun _ e -> handler e)
                    listViewBase.DragItemsStarting.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = listViewBase.DragItemsStarting.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onItemClick<'m when 'm :> ListViewBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: ItemClickEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ItemClick"
                Subscribe = (fun uiElement ->
                    let listViewBase = uiElement :?> ListViewBase
                    let h = ItemClickEventHandler(fun _ e -> handler e)
                    listViewBase.ItemClick.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = listViewBase.ItemClick.RemoveHandler h }
                )
            })
