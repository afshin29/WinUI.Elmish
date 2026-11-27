namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property ComputedHorizontalScrollBarVisibilityProperty: read-only
// skipped dependency property ComputedHorizontalScrollModeProperty: read-only
// skipped dependency property ComputedVerticalScrollBarVisibilityProperty: read-only
// skipped dependency property ComputedVerticalScrollModeProperty: read-only
// skipped dependency property ScrollPresenterProperty: read-only
// skipped property CurrentAnchor: read-only
// skipped property ExpressionAnimationSources: read-only
// skipped property ExtentHeight: read-only
// skipped property ExtentWidth: read-only
// skipped property HorizontalOffset: read-only
// skipped property ScrollableHeight: read-only
// skipped property ScrollableWidth: read-only
// skipped property State: read-only
// skipped property VerticalOffset: read-only
// skipped property ViewportHeight: read-only
// skipped property ViewportWidth: read-only
// skipped property ZoomFactor: read-only


[<AutoOpen>]
module ScrollViewMod =

    let private scrollViewNodeId = getNextId ()

    registerDefaultRenderer<ScrollView> scrollViewNodeId

    let private canContentRenderOutsideBoundsPropertyId = getNextId ()
    let private contentOrientationPropertyId = getNextId ()
    let private contentPropertyId = getNextId ()
    let private horizontalAnchorRatioPropertyId = getNextId ()
    let private horizontalScrollBarVisibilityPropertyId = getNextId ()
    let private horizontalScrollChainModePropertyId = getNextId ()
    let private horizontalScrollModePropertyId = getNextId ()
    let private horizontalScrollRailModePropertyId = getNextId ()
    let private ignoredInputKindsPropertyId = getNextId ()
    let private maxZoomFactorPropertyId = getNextId ()
    let private minZoomFactorPropertyId = getNextId ()
    let private verticalAnchorRatioPropertyId = getNextId ()
    let private verticalScrollBarVisibilityPropertyId = getNextId ()
    let private verticalScrollChainModePropertyId = getNextId ()
    let private verticalScrollModePropertyId = getNextId ()
    let private verticalScrollRailModePropertyId = getNextId ()
    let private zoomChainModePropertyId = getNextId ()
    let private zoomModePropertyId = getNextId ()

    [<Extension>]
    type ScrollViewExtensions =

        [<Extension>]
        static member canContentRenderOutsideBounds(node: VirtualNode<ScrollView>, value: bool) =
            createDependencyProperty canContentRenderOutsideBoundsPropertyId ScrollView.CanContentRenderOutsideBoundsProperty value
            |> node.set

        [<Extension>]
        static member contentOrientation(node: VirtualNode<ScrollView>, value: ScrollingContentOrientation) =
            createDependencyProperty contentOrientationPropertyId ScrollView.ContentOrientationProperty value
            |> node.set

        [<Extension>]
        static member content(node: VirtualNode<ScrollView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                contentPropertyId
                value
                (fun element child -> (element :?> ScrollView).Content <- child)
                (fun element -> (element :?> ScrollView).Content)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member horizontalAnchorRatio(node: VirtualNode<ScrollView>, value: float) =
            createDependencyProperty horizontalAnchorRatioPropertyId ScrollView.HorizontalAnchorRatioProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollBarVisibility(node: VirtualNode<ScrollView>, value: ScrollingScrollBarVisibility) =
            createDependencyProperty horizontalScrollBarVisibilityPropertyId ScrollView.HorizontalScrollBarVisibilityProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollChainMode(node: VirtualNode<ScrollView>, value: ScrollingChainMode) =
            createDependencyProperty horizontalScrollChainModePropertyId ScrollView.HorizontalScrollChainModeProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollMode(node: VirtualNode<ScrollView>, value: ScrollingScrollMode) =
            createDependencyProperty horizontalScrollModePropertyId ScrollView.HorizontalScrollModeProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollRailMode(node: VirtualNode<ScrollView>, value: ScrollingRailMode) =
            createDependencyProperty horizontalScrollRailModePropertyId ScrollView.HorizontalScrollRailModeProperty value
            |> node.set

        [<Extension>]
        static member ignoredInputKinds(node: VirtualNode<ScrollView>, value: ScrollingInputKinds) =
            createDependencyProperty ignoredInputKindsPropertyId ScrollView.IgnoredInputKindsProperty value
            |> node.set

        [<Extension>]
        static member maxZoomFactor(node: VirtualNode<ScrollView>, value: float) =
            createDependencyProperty maxZoomFactorPropertyId ScrollView.MaxZoomFactorProperty value
            |> node.set

        [<Extension>]
        static member minZoomFactor(node: VirtualNode<ScrollView>, value: float) =
            createDependencyProperty minZoomFactorPropertyId ScrollView.MinZoomFactorProperty value
            |> node.set

        [<Extension>]
        static member verticalAnchorRatio(node: VirtualNode<ScrollView>, value: float) =
            createDependencyProperty verticalAnchorRatioPropertyId ScrollView.VerticalAnchorRatioProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollBarVisibility(node: VirtualNode<ScrollView>, value: ScrollingScrollBarVisibility) =
            createDependencyProperty verticalScrollBarVisibilityPropertyId ScrollView.VerticalScrollBarVisibilityProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollChainMode(node: VirtualNode<ScrollView>, value: ScrollingChainMode) =
            createDependencyProperty verticalScrollChainModePropertyId ScrollView.VerticalScrollChainModeProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollMode(node: VirtualNode<ScrollView>, value: ScrollingScrollMode) =
            createDependencyProperty verticalScrollModePropertyId ScrollView.VerticalScrollModeProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollRailMode(node: VirtualNode<ScrollView>, value: ScrollingRailMode) =
            createDependencyProperty verticalScrollRailModePropertyId ScrollView.VerticalScrollRailModeProperty value
            |> node.set

        [<Extension>]
        static member zoomChainMode(node: VirtualNode<ScrollView>, value: ScrollingChainMode) =
            createDependencyProperty zoomChainModePropertyId ScrollView.ZoomChainModeProperty value
            |> node.set

        [<Extension>]
        static member zoomMode(node: VirtualNode<ScrollView>, value: ScrollingZoomMode) =
            createDependencyProperty zoomModePropertyId ScrollView.ZoomModeProperty value
            |> node.set

        [<Extension>]
        static member onAnchorRequested(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingAnchorRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "AnchorRequested"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingAnchorRequestedEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_AnchorRequested h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_AnchorRequested h }
                )
            })

        [<Extension>]
        static member onBringingIntoView(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingBringingIntoViewEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "BringingIntoView"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingBringingIntoViewEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_BringingIntoView h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_BringingIntoView h }
                )
            })

        [<Extension>]
        static member onExtentChanged(node: VirtualNode<ScrollView>, handler: ScrollView -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ExtentChanged"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, obj>(fun _ e -> handler scrollView e)
                    scrollView.add_ExtentChanged h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ExtentChanged h }
                )
            })

        [<Extension>]
        static member onScrollAnimationStarting(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingScrollAnimationStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ScrollAnimationStarting"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingScrollAnimationStartingEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_ScrollAnimationStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ScrollAnimationStarting h }
                )
            })

        [<Extension>]
        static member onScrollCompleted(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingScrollCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ScrollCompleted"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingScrollCompletedEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_ScrollCompleted h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ScrollCompleted h }
                )
            })

        [<Extension>]
        static member onScrollStarting(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingScrollStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ScrollStarting"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingScrollStartingEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_ScrollStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ScrollStarting h }
                )
            })

        [<Extension>]
        static member onStateChanged(node: VirtualNode<ScrollView>, handler: ScrollView -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "StateChanged"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, obj>(fun _ e -> handler scrollView e)
                    scrollView.add_StateChanged h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_StateChanged h }
                )
            })

        [<Extension>]
        static member onViewChanged(node: VirtualNode<ScrollView>, handler: ScrollView -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ViewChanged"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, obj>(fun _ e -> handler scrollView e)
                    scrollView.add_ViewChanged h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ViewChanged h }
                )
            })

        [<Extension>]
        static member onZoomAnimationStarting(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingZoomAnimationStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ZoomAnimationStarting"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingZoomAnimationStartingEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_ZoomAnimationStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ZoomAnimationStarting h }
                )
            })

        [<Extension>]
        static member onZoomCompleted(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingZoomCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ZoomCompleted"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingZoomCompletedEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_ZoomCompleted h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ZoomCompleted h }
                )
            })

        [<Extension>]
        static member onZoomStarting(node: VirtualNode<ScrollView>, handler: ScrollView -> ScrollingZoomStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ZoomStarting"
                Subscribe = (fun uiElement ->
                    let scrollView = uiElement :?> ScrollView
                    let h = TypedEventHandler<ScrollView, ScrollingZoomStartingEventArgs>(fun _ e -> handler scrollView e)
                    scrollView.add_ZoomStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollView.remove_ZoomStarting h }
                )
            })

    let scrollView () =
        createNode<ScrollView> scrollViewNodeId
