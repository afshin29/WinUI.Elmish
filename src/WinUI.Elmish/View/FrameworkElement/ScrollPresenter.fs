namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property ComputedHorizontalScrollModeProperty: read-only
// skipped dependency property ComputedVerticalScrollModeProperty: read-only
// skipped property CurrentAnchor: read-only
// skipped property ExpressionAnimationSources: read-only
// skipped property ExtentHeight: read-only
// skipped property ExtentWidth: read-only
// skipped property HorizontalOffset: read-only
// skipped property HorizontalSnapPoints: read-only
// skipped property ScrollableHeight: read-only
// skipped property ScrollableWidth: read-only
// skipped property State: read-only
// skipped property VerticalOffset: read-only
// skipped property VerticalSnapPoints: read-only
// skipped property ViewportHeight: read-only
// skipped property ViewportWidth: read-only
// skipped property ZoomFactor: read-only
// skipped property ZoomSnapPoints: read-only


[<AutoOpen>]
module ScrollPresenterMod =

    let private scrollPresenterNodeId = getNextId ()

    registerDefaultRenderer<ScrollPresenter> scrollPresenterNodeId

    let private backgroundPropertyId = getNextId ()
    let private canContentRenderOutsideBoundsPropertyId = getNextId ()
    let private contentOrientationPropertyId = getNextId ()
    let private contentPropertyId = getNextId ()
    let private horizontalAnchorRatioPropertyId = getNextId ()
    let private horizontalScrollChainModePropertyId = getNextId ()
    let private horizontalScrollModePropertyId = getNextId ()
    let private horizontalScrollRailModePropertyId = getNextId ()
    let private ignoredInputKindsPropertyId = getNextId ()
    let private maxZoomFactorPropertyId = getNextId ()
    let private minZoomFactorPropertyId = getNextId ()
    let private verticalAnchorRatioPropertyId = getNextId ()
    let private verticalScrollChainModePropertyId = getNextId ()
    let private verticalScrollModePropertyId = getNextId ()
    let private verticalScrollRailModePropertyId = getNextId ()
    let private zoomChainModePropertyId = getNextId ()
    let private zoomModePropertyId = getNextId ()
    let private horizontalScrollControllerPropertyId = getNextId ()
    let private verticalScrollControllerPropertyId = getNextId ()

    [<Extension>]
    type ScrollPresenterExtensions =

        [<Extension>]
        static member background(node: VirtualNode<ScrollPresenter>, value: Brush) =
            createDependencyProperty backgroundPropertyId ScrollPresenter.BackgroundProperty value
            |> node.set

        [<Extension>]
        static member canContentRenderOutsideBounds(node: VirtualNode<ScrollPresenter>, value: bool) =
            createDependencyProperty canContentRenderOutsideBoundsPropertyId ScrollPresenter.CanContentRenderOutsideBoundsProperty value
            |> node.set

        [<Extension>]
        static member contentOrientation(node: VirtualNode<ScrollPresenter>, value: ScrollingContentOrientation) =
            createDependencyProperty contentOrientationPropertyId ScrollPresenter.ContentOrientationProperty value
            |> node.set

        [<Extension>]
        static member content(node: VirtualNode<ScrollPresenter>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                contentPropertyId
                value
                (fun element child -> (element :?> ScrollPresenter).Content <- child)
                (fun element -> (element :?> ScrollPresenter).Content)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member horizontalAnchorRatio(node: VirtualNode<ScrollPresenter>, value: float) =
            createDependencyProperty horizontalAnchorRatioPropertyId ScrollPresenter.HorizontalAnchorRatioProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollChainMode(node: VirtualNode<ScrollPresenter>, value: ScrollingChainMode) =
            createDependencyProperty horizontalScrollChainModePropertyId ScrollPresenter.HorizontalScrollChainModeProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollMode(node: VirtualNode<ScrollPresenter>, value: ScrollingScrollMode) =
            createDependencyProperty horizontalScrollModePropertyId ScrollPresenter.HorizontalScrollModeProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollRailMode(node: VirtualNode<ScrollPresenter>, value: ScrollingRailMode) =
            createDependencyProperty horizontalScrollRailModePropertyId ScrollPresenter.HorizontalScrollRailModeProperty value
            |> node.set

        [<Extension>]
        static member ignoredInputKinds(node: VirtualNode<ScrollPresenter>, value: ScrollingInputKinds) =
            createDependencyProperty ignoredInputKindsPropertyId ScrollPresenter.IgnoredInputKindsProperty value
            |> node.set

        [<Extension>]
        static member maxZoomFactor(node: VirtualNode<ScrollPresenter>, value: float) =
            createDependencyProperty maxZoomFactorPropertyId ScrollPresenter.MaxZoomFactorProperty value
            |> node.set

        [<Extension>]
        static member minZoomFactor(node: VirtualNode<ScrollPresenter>, value: float) =
            createDependencyProperty minZoomFactorPropertyId ScrollPresenter.MinZoomFactorProperty value
            |> node.set

        [<Extension>]
        static member verticalAnchorRatio(node: VirtualNode<ScrollPresenter>, value: float) =
            createDependencyProperty verticalAnchorRatioPropertyId ScrollPresenter.VerticalAnchorRatioProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollChainMode(node: VirtualNode<ScrollPresenter>, value: ScrollingChainMode) =
            createDependencyProperty verticalScrollChainModePropertyId ScrollPresenter.VerticalScrollChainModeProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollMode(node: VirtualNode<ScrollPresenter>, value: ScrollingScrollMode) =
            createDependencyProperty verticalScrollModePropertyId ScrollPresenter.VerticalScrollModeProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollRailMode(node: VirtualNode<ScrollPresenter>, value: ScrollingRailMode) =
            createDependencyProperty verticalScrollRailModePropertyId ScrollPresenter.VerticalScrollRailModeProperty value
            |> node.set

        [<Extension>]
        static member zoomChainMode(node: VirtualNode<ScrollPresenter>, value: ScrollingChainMode) =
            createDependencyProperty zoomChainModePropertyId ScrollPresenter.ZoomChainModeProperty value
            |> node.set

        [<Extension>]
        static member zoomMode(node: VirtualNode<ScrollPresenter>, value: ScrollingZoomMode) =
            createDependencyProperty zoomModePropertyId ScrollPresenter.ZoomModeProperty value
            |> node.set

        [<Extension>]
        static member horizontalScrollController(node: VirtualNode<ScrollPresenter>, value: IScrollController) =
            createClrProperty
                horizontalScrollControllerPropertyId
                (fun uiElement _ -> (uiElement :?> ScrollPresenter).HorizontalScrollController <- value)
                (fun uiElement -> (uiElement :?> ScrollPresenter).HorizontalScrollController <- Unchecked.defaultof<IScrollController>)
                value
            |> node.set

        [<Extension>]
        static member verticalScrollController(node: VirtualNode<ScrollPresenter>, value: IScrollController) =
            createClrProperty
                verticalScrollControllerPropertyId
                (fun uiElement _ -> (uiElement :?> ScrollPresenter).VerticalScrollController <- value)
                (fun uiElement -> (uiElement :?> ScrollPresenter).VerticalScrollController <- Unchecked.defaultof<IScrollController>)
                value
            |> node.set

        [<Extension>]
        static member onAnchorRequested(node: VirtualNode<ScrollPresenter>, handler: ScrollingAnchorRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "AnchorRequested"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingAnchorRequestedEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_AnchorRequested h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_AnchorRequested h }
                )
            })

        [<Extension>]
        static member onBringingIntoView(node: VirtualNode<ScrollPresenter>, handler: ScrollingBringingIntoViewEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "BringingIntoView"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingBringingIntoViewEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_BringingIntoView h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_BringingIntoView h }
                )
            })

        [<Extension>]
        static member onExtentChanged(node: VirtualNode<ScrollPresenter>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ExtentChanged"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, obj>(fun _ e -> handler e)
                    scrollPresenter.add_ExtentChanged h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ExtentChanged h }
                )
            })

        [<Extension>]
        static member onScrollAnimationStarting(node: VirtualNode<ScrollPresenter>, handler: ScrollingScrollAnimationStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ScrollAnimationStarting"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingScrollAnimationStartingEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_ScrollAnimationStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ScrollAnimationStarting h }
                )
            })

        [<Extension>]
        static member onScrollCompleted(node: VirtualNode<ScrollPresenter>, handler: ScrollingScrollCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ScrollCompleted"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingScrollCompletedEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_ScrollCompleted h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ScrollCompleted h }
                )
            })

        [<Extension>]
        static member onScrollStarting(node: VirtualNode<ScrollPresenter>, handler: ScrollingScrollStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ScrollStarting"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingScrollStartingEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_ScrollStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ScrollStarting h }
                )
            })

        [<Extension>]
        static member onStateChanged(node: VirtualNode<ScrollPresenter>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "StateChanged"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, obj>(fun _ e -> handler e)
                    scrollPresenter.add_StateChanged h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_StateChanged h }
                )
            })

        [<Extension>]
        static member onViewChanged(node: VirtualNode<ScrollPresenter>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ViewChanged"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, obj>(fun _ e -> handler e)
                    scrollPresenter.add_ViewChanged h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ViewChanged h }
                )
            })

        [<Extension>]
        static member onZoomAnimationStarting(node: VirtualNode<ScrollPresenter>, handler: ScrollingZoomAnimationStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ZoomAnimationStarting"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingZoomAnimationStartingEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_ZoomAnimationStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ZoomAnimationStarting h }
                )
            })

        [<Extension>]
        static member onZoomCompleted(node: VirtualNode<ScrollPresenter>, handler: ScrollingZoomCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ZoomCompleted"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingZoomCompletedEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_ZoomCompleted h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ZoomCompleted h }
                )
            })

        [<Extension>]
        static member onZoomStarting(node: VirtualNode<ScrollPresenter>, handler: ScrollingZoomStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ZoomStarting"
                Subscribe = (fun uiElement ->
                    let scrollPresenter = uiElement :?> ScrollPresenter
                    let h = TypedEventHandler<ScrollPresenter, ScrollingZoomStartingEventArgs>(fun _ e -> handler e)
                    scrollPresenter.add_ZoomStarting h
                    { new IDisposable with
                        member _.Dispose() = scrollPresenter.remove_ZoomStarting h }
                )
            })

    let scrollPresenter () =
        createNode<ScrollPresenter> scrollPresenterNodeId
