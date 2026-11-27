namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property ComputedHorizontalScrollBarVisibilityProperty: read-only
// skipped dependency property ComputedVerticalScrollBarVisibilityProperty: read-only
// skipped dependency property ExtentHeightProperty: read-only
// skipped dependency property ExtentWidthProperty: read-only
// skipped dependency property HorizontalOffsetProperty: read-only
// skipped dependency property ScrollableHeightProperty: read-only
// skipped dependency property ScrollableWidthProperty: read-only
// skipped dependency property VerticalOffsetProperty: read-only
// skipped dependency property ViewportHeightProperty: read-only
// skipped dependency property ViewportWidthProperty: read-only
// skipped dependency property ZoomFactorProperty: read-only
// skipped dependency property ZoomSnapPointsProperty: read-only
// skipped property CurrentAnchor: read-only


[<AutoOpen>]
module ScrollViewerMod =

    let private scrollViewerNodeId = getNextId ()

    registerDefaultRenderer<ScrollViewer> scrollViewerNodeId

    let private bringIntoViewOnFocusChangePropertyId = getNextId ()
    let private canContentRenderOutsideBoundsPropertyId = getNextId ()
    let private horizontalAnchorRatioPropertyId = getNextId ()
    let private horizontalScrollBarVisibilityPropertyId = getNextId ()
    let private horizontalScrollModePropertyId = getNextId ()
    let private horizontalSnapPointsAlignmentPropertyId = getNextId ()
    let private horizontalSnapPointsTypePropertyId = getNextId ()
    let private isDeferredScrollingEnabledPropertyId = getNextId ()
    let private isHorizontalRailEnabledPropertyId = getNextId ()
    let private isHorizontalScrollChainingEnabledPropertyId = getNextId ()
    let private isScrollInertiaEnabledPropertyId = getNextId ()
    let private isVerticalRailEnabledPropertyId = getNextId ()
    let private isVerticalScrollChainingEnabledPropertyId = getNextId ()
    let private isZoomChainingEnabledPropertyId = getNextId ()
    let private isZoomInertiaEnabledPropertyId = getNextId ()
    let private leftHeaderPropertyId = getNextId ()
    let private maxZoomFactorPropertyId = getNextId ()
    let private minZoomFactorPropertyId = getNextId ()
    let private reduceViewportForCoreInputViewOcclusionsPropertyId = getNextId ()
    let private topHeaderPropertyId = getNextId ()
    let private topLeftHeaderPropertyId = getNextId ()
    let private verticalAnchorRatioPropertyId = getNextId ()
    let private verticalScrollBarVisibilityPropertyId = getNextId ()
    let private verticalScrollModePropertyId = getNextId ()
    let private verticalSnapPointsAlignmentPropertyId = getNextId ()
    let private verticalSnapPointsTypePropertyId = getNextId ()
    let private zoomModePropertyId = getNextId ()
    let private zoomSnapPointsTypePropertyId = getNextId ()

    [<Extension>]
    type ScrollViewerExtensions =

        [<Extension>]
        static member bringIntoViewOnFocusChange(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty bringIntoViewOnFocusChangePropertyId ScrollViewer.BringIntoViewOnFocusChangeProperty value
            |> node.set

        [<Extension>]
        static member canContentRenderOutsideBounds(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty canContentRenderOutsideBoundsPropertyId ScrollViewer.CanContentRenderOutsideBoundsProperty value
            |> node.set

        [<Extension>]
        static member horizontalAnchorRatio(node: VirtualNode<ScrollViewer>, value: float) =
            createDependencyProperty horizontalAnchorRatioPropertyId ScrollViewer.HorizontalAnchorRatioProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollBarVisibility(node: VirtualNode<ScrollViewer>, value: ScrollBarVisibility) =
            createDependencyProperty horizontalScrollBarVisibilityPropertyId ScrollViewer.HorizontalScrollBarVisibilityProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollMode(node: VirtualNode<ScrollViewer>, value: ScrollMode) =
            createDependencyProperty horizontalScrollModePropertyId ScrollViewer.HorizontalScrollModeProperty value
            |> node.set

        [<Extension>]
        static member horizontalSnapPointsAlignment(node: VirtualNode<ScrollViewer>, value: SnapPointsAlignment) =
            createDependencyProperty horizontalSnapPointsAlignmentPropertyId ScrollViewer.HorizontalSnapPointsAlignmentProperty value
            |> node.set

        [<Extension>]
        static member horizontalSnapPointsType(node: VirtualNode<ScrollViewer>, value: SnapPointsType) =
            createDependencyProperty horizontalSnapPointsTypePropertyId ScrollViewer.HorizontalSnapPointsTypeProperty value
            |> node.set

        [<Extension>]
        static member isDeferredScrollingEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isDeferredScrollingEnabledPropertyId ScrollViewer.IsDeferredScrollingEnabledProperty value
            |> node.set

        [<Extension>]
        static member isHorizontalRailEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isHorizontalRailEnabledPropertyId ScrollViewer.IsHorizontalRailEnabledProperty value
            |> node.set

        [<Extension>]
        static member isHorizontalScrollChainingEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isHorizontalScrollChainingEnabledPropertyId ScrollViewer.IsHorizontalScrollChainingEnabledProperty value
            |> node.set

        [<Extension>]
        static member isScrollInertiaEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isScrollInertiaEnabledPropertyId ScrollViewer.IsScrollInertiaEnabledProperty value
            |> node.set

        [<Extension>]
        static member isVerticalRailEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isVerticalRailEnabledPropertyId ScrollViewer.IsVerticalRailEnabledProperty value
            |> node.set

        [<Extension>]
        static member isVerticalScrollChainingEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isVerticalScrollChainingEnabledPropertyId ScrollViewer.IsVerticalScrollChainingEnabledProperty value
            |> node.set

        [<Extension>]
        static member isZoomChainingEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isZoomChainingEnabledPropertyId ScrollViewer.IsZoomChainingEnabledProperty value
            |> node.set

        [<Extension>]
        static member isZoomInertiaEnabled(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty isZoomInertiaEnabledPropertyId ScrollViewer.IsZoomInertiaEnabledProperty value
            |> node.set

        [<Extension>]
        static member leftHeader(node: VirtualNode<ScrollViewer>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                leftHeaderPropertyId
                value
                (fun element child -> (element :?> ScrollViewer).LeftHeader <- child)
                (fun element -> (element :?> ScrollViewer).LeftHeader)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member maxZoomFactor(node: VirtualNode<ScrollViewer>, value: float32) =
            createDependencyProperty maxZoomFactorPropertyId ScrollViewer.MaxZoomFactorProperty value
            |> node.set

        [<Extension>]
        static member minZoomFactor(node: VirtualNode<ScrollViewer>, value: float32) =
            createDependencyProperty minZoomFactorPropertyId ScrollViewer.MinZoomFactorProperty value
            |> node.set

        [<Extension>]
        static member reduceViewportForCoreInputViewOcclusions(node: VirtualNode<ScrollViewer>, value: bool) =
            createDependencyProperty reduceViewportForCoreInputViewOcclusionsPropertyId ScrollViewer.ReduceViewportForCoreInputViewOcclusionsProperty value
            |> node.set

        [<Extension>]
        static member topHeader(node: VirtualNode<ScrollViewer>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                topHeaderPropertyId
                value
                (fun element child -> (element :?> ScrollViewer).TopHeader <- child)
                (fun element -> (element :?> ScrollViewer).TopHeader)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member topLeftHeader(node: VirtualNode<ScrollViewer>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                topLeftHeaderPropertyId
                value
                (fun element child -> (element :?> ScrollViewer).TopLeftHeader <- child)
                (fun element -> (element :?> ScrollViewer).TopLeftHeader)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member verticalAnchorRatio(node: VirtualNode<ScrollViewer>, value: float) =
            createDependencyProperty verticalAnchorRatioPropertyId ScrollViewer.VerticalAnchorRatioProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollBarVisibility(node: VirtualNode<ScrollViewer>, value: ScrollBarVisibility) =
            createDependencyProperty verticalScrollBarVisibilityPropertyId ScrollViewer.VerticalScrollBarVisibilityProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollMode(node: VirtualNode<ScrollViewer>, value: ScrollMode) =
            createDependencyProperty verticalScrollModePropertyId ScrollViewer.VerticalScrollModeProperty value
            |> node.set

        [<Extension>]
        static member verticalSnapPointsAlignment(node: VirtualNode<ScrollViewer>, value: SnapPointsAlignment) =
            createDependencyProperty verticalSnapPointsAlignmentPropertyId ScrollViewer.VerticalSnapPointsAlignmentProperty value
            |> node.set

        [<Extension>]
        static member verticalSnapPointsType(node: VirtualNode<ScrollViewer>, value: SnapPointsType) =
            createDependencyProperty verticalSnapPointsTypePropertyId ScrollViewer.VerticalSnapPointsTypeProperty value
            |> node.set

        [<Extension>]
        static member zoomMode(node: VirtualNode<ScrollViewer>, value: ZoomMode) =
            createDependencyProperty zoomModePropertyId ScrollViewer.ZoomModeProperty value
            |> node.set

        [<Extension>]
        static member zoomSnapPointsType(node: VirtualNode<ScrollViewer>, value: SnapPointsType) =
            createDependencyProperty zoomSnapPointsTypePropertyId ScrollViewer.ZoomSnapPointsTypeProperty value
            |> node.set

        [<Extension>]
        static member onAnchorRequested(node: VirtualNode<ScrollViewer>, handler: ScrollViewer -> AnchorRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "AnchorRequested"
                Subscribe = (fun uiElement ->
                    let scrollViewer = uiElement :?> ScrollViewer
                    let h = TypedEventHandler<ScrollViewer, AnchorRequestedEventArgs>(fun _ e -> handler scrollViewer e)
                    scrollViewer.add_AnchorRequested h
                    { new IDisposable with
                        member _.Dispose() = scrollViewer.remove_AnchorRequested h }
                )
            })

        [<Extension>]
        static member onDirectManipulationCompleted(node: VirtualNode<ScrollViewer>, handler: ScrollViewer -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DirectManipulationCompleted"
                Subscribe = (fun uiElement ->
                    let scrollViewer = uiElement :?> ScrollViewer
                    let h = EventHandler<obj>(fun _ e -> handler scrollViewer e)
                    scrollViewer.DirectManipulationCompleted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = scrollViewer.DirectManipulationCompleted.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDirectManipulationStarted(node: VirtualNode<ScrollViewer>, handler: ScrollViewer -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DirectManipulationStarted"
                Subscribe = (fun uiElement ->
                    let scrollViewer = uiElement :?> ScrollViewer
                    let h = EventHandler<obj>(fun _ e -> handler scrollViewer e)
                    scrollViewer.DirectManipulationStarted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = scrollViewer.DirectManipulationStarted.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onViewChanged(node: VirtualNode<ScrollViewer>, handler: ScrollViewer -> ScrollViewerViewChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ViewChanged"
                Subscribe = (fun uiElement ->
                    let scrollViewer = uiElement :?> ScrollViewer
                    let h = EventHandler<ScrollViewerViewChangedEventArgs>(fun _ e -> handler scrollViewer e)
                    scrollViewer.ViewChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = scrollViewer.ViewChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onViewChanging(node: VirtualNode<ScrollViewer>, handler: ScrollViewer -> ScrollViewerViewChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ViewChanging"
                Subscribe = (fun uiElement ->
                    let scrollViewer = uiElement :?> ScrollViewer
                    let h = EventHandler<ScrollViewerViewChangingEventArgs>(fun _ e -> handler scrollViewer e)
                    scrollViewer.ViewChanging.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = scrollViewer.ViewChanging.RemoveHandler h }
                )
            })

    let scrollViewer () =
        createNode<ScrollViewer> scrollViewerNodeId
