namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Input
open Microsoft.UI.Xaml.Media
open Microsoft.UI.Xaml.Media.Animation
open Microsoft.UI.Xaml.Media.Media3D
open System
open System.Numerics
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property FocusStateProperty: read-only
// skipped dependency property LightsProperty: read-only
// skipped dependency property PointerCapturesProperty: read-only
// skipped property BringIntoViewRequestedEvent: static
// skipped property CharacterReceivedEvent: static
// skipped property ContextRequestedEvent: static
// skipped property DoubleTappedEvent: static
// skipped property DragEnterEvent: static
// skipped property DragLeaveEvent: static
// skipped property DragOverEvent: static
// skipped property DropEvent: static
// skipped property GettingFocusEvent: static
// skipped property HoldingEvent: static
// skipped property KeyDownEvent: static
// skipped property KeyUpEvent: static
// skipped property LosingFocusEvent: static
// skipped property ManipulationCompletedEvent: static
// skipped property ManipulationDeltaEvent: static
// skipped property ManipulationInertiaStartingEvent: static
// skipped property ManipulationStartedEvent: static
// skipped property ManipulationStartingEvent: static
// skipped property NoFocusCandidateFoundEvent: static
// skipped property PointerCanceledEvent: static
// skipped property PointerCaptureLostEvent: static
// skipped property PointerEnteredEvent: static
// skipped property PointerExitedEvent: static
// skipped property PointerMovedEvent: static
// skipped property PointerPressedEvent: static
// skipped property PointerReleasedEvent: static
// skipped property PointerWheelChangedEvent: static
// skipped property PreviewKeyDownEvent: static
// skipped property PreviewKeyUpEvent: static
// skipped property RightTappedEvent: static
// skipped property TappedEvent: static
// skipped property ActualOffset: read-only
// skipped property ActualSize: read-only
// skipped property DesiredSize: read-only
// skipped property KeyboardAccelerators: read-only
// skipped property RenderSize: read-only


[<AutoOpen>]
module UIElementMod =

    let private accessKeyPropertyId = getNextId ()
    let private accessKeyScopeOwnerPropertyId = getNextId ()
    let private allowDropPropertyId = getNextId ()
    let private cacheModePropertyId = getNextId ()
    let private canBeScrollAnchorPropertyId = getNextId ()
    let private canDragPropertyId = getNextId ()
    let private clipPropertyId = getNextId ()
    let private compositeModePropertyId = getNextId ()
    let private contextFlyoutPropertyId = getNextId ()
    let private exitDisplayModeOnAccessKeyInvokedPropertyId = getNextId ()
    let private highContrastAdjustmentPropertyId = getNextId ()
    let private isAccessKeyScopePropertyId = getNextId ()
    let private isDoubleTapEnabledPropertyId = getNextId ()
    let private isHitTestVisiblePropertyId = getNextId ()
    let private isHoldingEnabledPropertyId = getNextId ()
    let private isRightTapEnabledPropertyId = getNextId ()
    let private isTabStopPropertyId = getNextId ()
    let private isTapEnabledPropertyId = getNextId ()
    let private keyTipHorizontalOffsetPropertyId = getNextId ()
    let private keyTipPlacementModePropertyId = getNextId ()
    let private keyTipTargetPropertyId = getNextId ()
    let private keyTipVerticalOffsetPropertyId = getNextId ()
    let private keyboardAcceleratorPlacementModePropertyId = getNextId ()
    let private keyboardAcceleratorPlacementTargetPropertyId = getNextId ()
    let private manipulationModePropertyId = getNextId ()
    let private opacityPropertyId = getNextId ()
    let private projectionPropertyId = getNextId ()
    let private renderTransformOriginPropertyId = getNextId ()
    let private renderTransformPropertyId = getNextId ()
    let private shadowPropertyId = getNextId ()
    let private tabFocusNavigationPropertyId = getNextId ()
    let private tabIndexPropertyId = getNextId ()
    let private transform3DPropertyId = getNextId ()
    let private transitionsPropertyId = getNextId ()
    let private useLayoutRoundingPropertyId = getNextId ()
    let private useSystemFocusVisualsPropertyId = getNextId ()
    let private visibilityPropertyId = getNextId ()
    let private xyFocusDownNavigationStrategyPropertyId = getNextId ()
    let private xyFocusDownPropertyId = getNextId ()
    let private xyFocusKeyboardNavigationPropertyId = getNextId ()
    let private xyFocusLeftNavigationStrategyPropertyId = getNextId ()
    let private xyFocusLeftPropertyId = getNextId ()
    let private xyFocusRightNavigationStrategyPropertyId = getNextId ()
    let private xyFocusRightPropertyId = getNextId ()
    let private xyFocusUpNavigationStrategyPropertyId = getNextId ()
    let private xyFocusUpPropertyId = getNextId ()
    let private centerPointPropertyId = getNextId ()
    let private opacityTransitionPropertyId = getNextId ()
    let private rasterizationScalePropertyId = getNextId ()
    let private rotationPropertyId = getNextId ()
    let private rotationAxisPropertyId = getNextId ()
    let private rotationTransitionPropertyId = getNextId ()
    let private scalePropertyId = getNextId ()
    let private scaleTransitionPropertyId = getNextId ()
    let private transformMatrixPropertyId = getNextId ()
    let private translationPropertyId = getNextId ()
    let private translationTransitionPropertyId = getNextId ()
    let private xamlRootPropertyId = getNextId ()

    [<Extension>]
    type UIElementExtensions =

        [<Extension>]
        static member accessKey<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty accessKeyPropertyId UIElement.AccessKeyProperty value
            |> node.set

        [<Extension>]
        static member accessKeyScopeOwner<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: DependencyObject) =
            createDependencyProperty accessKeyScopeOwnerPropertyId UIElement.AccessKeyScopeOwnerProperty value
            |> node.set

        [<Extension>]
        static member allowDrop<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty allowDropPropertyId UIElement.AllowDropProperty value
            |> node.set

        [<Extension>]
        static member cacheMode<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: CacheMode) =
            createDependencyProperty cacheModePropertyId UIElement.CacheModeProperty value
            |> node.set

        [<Extension>]
        static member canBeScrollAnchor<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty canBeScrollAnchorPropertyId UIElement.CanBeScrollAnchorProperty value
            |> node.set

        [<Extension>]
        static member canDrag<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty canDragPropertyId UIElement.CanDragProperty value
            |> node.set

        [<Extension>]
        static member clip<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: RectangleGeometry) =
            createDependencyProperty clipPropertyId UIElement.ClipProperty value
            |> node.set

        [<Extension>]
        static member compositeMode<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: ElementCompositeMode) =
            createDependencyProperty compositeModePropertyId UIElement.CompositeModeProperty value
            |> node.set

        [<Extension>]
        static member contextFlyout<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: FlyoutBase) =
            createDependencyProperty contextFlyoutPropertyId UIElement.ContextFlyoutProperty value
            |> node.set

        [<Extension>]
        static member exitDisplayModeOnAccessKeyInvoked<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty exitDisplayModeOnAccessKeyInvokedPropertyId UIElement.ExitDisplayModeOnAccessKeyInvokedProperty value
            |> node.set

        [<Extension>]
        static member highContrastAdjustment<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: ElementHighContrastAdjustment) =
            createDependencyProperty highContrastAdjustmentPropertyId UIElement.HighContrastAdjustmentProperty value
            |> node.set

        [<Extension>]
        static member isAccessKeyScope<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isAccessKeyScopePropertyId UIElement.IsAccessKeyScopeProperty value
            |> node.set

        [<Extension>]
        static member isDoubleTapEnabled<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isDoubleTapEnabledPropertyId UIElement.IsDoubleTapEnabledProperty value
            |> node.set

        [<Extension>]
        static member isHitTestVisible<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isHitTestVisiblePropertyId UIElement.IsHitTestVisibleProperty value
            |> node.set

        [<Extension>]
        static member isHoldingEnabled<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isHoldingEnabledPropertyId UIElement.IsHoldingEnabledProperty value
            |> node.set

        [<Extension>]
        static member isRightTapEnabled<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isRightTapEnabledPropertyId UIElement.IsRightTapEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTabStop<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isTabStopPropertyId UIElement.IsTabStopProperty value
            |> node.set

        [<Extension>]
        static member isTapEnabled<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isTapEnabledPropertyId UIElement.IsTapEnabledProperty value
            |> node.set

        [<Extension>]
        static member keyTipHorizontalOffset<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty keyTipHorizontalOffsetPropertyId UIElement.KeyTipHorizontalOffsetProperty value
            |> node.set

        [<Extension>]
        static member keyTipPlacementMode<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: KeyTipPlacementMode) =
            createDependencyProperty keyTipPlacementModePropertyId UIElement.KeyTipPlacementModeProperty value
            |> node.set

        [<Extension>]
        static member keyTipTarget<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: DependencyObject) =
            createDependencyProperty keyTipTargetPropertyId UIElement.KeyTipTargetProperty value
            |> node.set

        [<Extension>]
        static member keyTipVerticalOffset<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty keyTipVerticalOffsetPropertyId UIElement.KeyTipVerticalOffsetProperty value
            |> node.set

        [<Extension>]
        static member keyboardAcceleratorPlacementMode<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: KeyboardAcceleratorPlacementMode) =
            createDependencyProperty keyboardAcceleratorPlacementModePropertyId UIElement.KeyboardAcceleratorPlacementModeProperty value
            |> node.set

        [<Extension>]
        static member keyboardAcceleratorPlacementTarget<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: DependencyObject) =
            createDependencyProperty keyboardAcceleratorPlacementTargetPropertyId UIElement.KeyboardAcceleratorPlacementTargetProperty value
            |> node.set

        [<Extension>]
        static member manipulationMode<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: ManipulationModes) =
            createDependencyProperty manipulationModePropertyId UIElement.ManipulationModeProperty value
            |> node.set

        [<Extension>]
        static member opacity<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty opacityPropertyId UIElement.OpacityProperty value
            |> node.set

        [<Extension>]
        static member projection<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Projection) =
            createDependencyProperty projectionPropertyId UIElement.ProjectionProperty value
            |> node.set

        [<Extension>]
        static member renderTransformOrigin<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Point) =
            createDependencyProperty renderTransformOriginPropertyId UIElement.RenderTransformOriginProperty value
            |> node.set

        [<Extension>]
        static member renderTransform<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Transform) =
            createDependencyProperty renderTransformPropertyId UIElement.RenderTransformProperty value
            |> node.set

        [<Extension>]
        static member shadow<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Shadow) =
            createDependencyProperty shadowPropertyId UIElement.ShadowProperty value
            |> node.set

        [<Extension>]
        static member tabFocusNavigation<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: KeyboardNavigationMode) =
            createDependencyProperty tabFocusNavigationPropertyId UIElement.TabFocusNavigationProperty value
            |> node.set

        [<Extension>]
        static member tabIndex<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty tabIndexPropertyId UIElement.TabIndexProperty value
            |> node.set

        [<Extension>]
        static member transform3D<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Transform3D) =
            createDependencyProperty transform3DPropertyId UIElement.Transform3DProperty value
            |> node.set

        [<Extension>]
        static member transitions<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty transitionsPropertyId UIElement.TransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member useLayoutRounding<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty useLayoutRoundingPropertyId UIElement.UseLayoutRoundingProperty value
            |> node.set

        [<Extension>]
        static member useSystemFocusVisuals<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty useSystemFocusVisualsPropertyId UIElement.UseSystemFocusVisualsProperty value
            |> node.set

        //[<Extension>]
        //static member visibility<'m when 'm :> UIElement and 'm: not struct>
        //    (node: VirtualNode<'m>, value: Visibility) =
        //    createDependencyProperty visibilityPropertyId UIElement.VisibilityProperty value
        //    |> node.set

        [<Extension>]
        static member visibleIf<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, condition: bool) =
            if condition then Visibility.Visible else Visibility.Collapsed
            |> createDependencyProperty visibilityPropertyId UIElement.VisibilityProperty
            |> node.set

        [<Extension>]
        static member xyFocusDownNavigationStrategy<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: XYFocusNavigationStrategy) =
            createDependencyProperty xyFocusDownNavigationStrategyPropertyId UIElement.XYFocusDownNavigationStrategyProperty value
            |> node.set

        [<Extension>]
        static member xyFocusDown<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: DependencyObject) =
            createDependencyProperty xyFocusDownPropertyId UIElement.XYFocusDownProperty value
            |> node.set

        [<Extension>]
        static member xyFocusKeyboardNavigation<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: XYFocusKeyboardNavigationMode) =
            createDependencyProperty xyFocusKeyboardNavigationPropertyId UIElement.XYFocusKeyboardNavigationProperty value
            |> node.set

        [<Extension>]
        static member xyFocusLeftNavigationStrategy<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: XYFocusNavigationStrategy) =
            createDependencyProperty xyFocusLeftNavigationStrategyPropertyId UIElement.XYFocusLeftNavigationStrategyProperty value
            |> node.set

        [<Extension>]
        static member xyFocusLeft<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: DependencyObject) =
            createDependencyProperty xyFocusLeftPropertyId UIElement.XYFocusLeftProperty value
            |> node.set

        [<Extension>]
        static member xyFocusRightNavigationStrategy<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: XYFocusNavigationStrategy) =
            createDependencyProperty xyFocusRightNavigationStrategyPropertyId UIElement.XYFocusRightNavigationStrategyProperty value
            |> node.set

        [<Extension>]
        static member xyFocusRight<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: DependencyObject) =
            createDependencyProperty xyFocusRightPropertyId UIElement.XYFocusRightProperty value
            |> node.set

        [<Extension>]
        static member xyFocusUpNavigationStrategy<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: XYFocusNavigationStrategy) =
            createDependencyProperty xyFocusUpNavigationStrategyPropertyId UIElement.XYFocusUpNavigationStrategyProperty value
            |> node.set

        [<Extension>]
        static member xyFocusUp<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: DependencyObject) =
            createDependencyProperty xyFocusUpPropertyId UIElement.XYFocusUpProperty value
            |> node.set

        [<Extension>]
        static member centerPoint<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Vector3) =
            createClrProperty
                centerPointPropertyId
                (fun uiElement _ -> uiElement.CenterPoint <- value)
                (fun uiElement -> uiElement.CenterPoint <- Unchecked.defaultof<Vector3>)
                value
            |> node.set

        [<Extension>]
        static member opacityTransition<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: ScalarTransition) =
            createClrProperty
                opacityTransitionPropertyId
                (fun uiElement _ -> uiElement.OpacityTransition <- value)
                (fun uiElement -> uiElement.OpacityTransition <- Unchecked.defaultof<ScalarTransition>)
                value
            |> node.set

        [<Extension>]
        static member rasterizationScale<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createClrProperty
                rasterizationScalePropertyId
                (fun uiElement _ -> uiElement.RasterizationScale <- value)
                (fun uiElement -> uiElement.RasterizationScale <- Unchecked.defaultof<float>)
                value
            |> node.set

        [<Extension>]
        static member rotation<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float32) =
            createClrProperty
                rotationPropertyId
                (fun uiElement _ -> uiElement.Rotation <- value)
                (fun uiElement -> uiElement.Rotation <- Unchecked.defaultof<float32>)
                value
            |> node.set

        [<Extension>]
        static member rotationAxis<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Vector3) =
            createClrProperty
                rotationAxisPropertyId
                (fun uiElement _ -> uiElement.RotationAxis <- value)
                (fun uiElement -> uiElement.RotationAxis <- Unchecked.defaultof<Vector3>)
                value
            |> node.set

        [<Extension>]
        static member rotationTransition<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: ScalarTransition) =
            createClrProperty
                rotationTransitionPropertyId
                (fun uiElement _ -> uiElement.RotationTransition <- value)
                (fun uiElement -> uiElement.RotationTransition <- Unchecked.defaultof<ScalarTransition>)
                value
            |> node.set

        [<Extension>]
        static member scale<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Vector3) =
            createClrProperty
                scalePropertyId
                (fun uiElement _ -> uiElement.Scale <- value)
                (fun uiElement -> uiElement.Scale <- Unchecked.defaultof<Vector3>)
                value
            |> node.set

        [<Extension>]
        static member scaleTransition<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Vector3Transition) =
            createClrProperty
                scaleTransitionPropertyId
                (fun uiElement _ -> uiElement.ScaleTransition <- value)
                (fun uiElement -> uiElement.ScaleTransition <- Unchecked.defaultof<Vector3Transition>)
                value
            |> node.set

        [<Extension>]
        static member transformMatrix<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Matrix4x4) =
            createClrProperty
                transformMatrixPropertyId
                (fun uiElement _ -> uiElement.TransformMatrix <- value)
                (fun uiElement -> uiElement.TransformMatrix <- Unchecked.defaultof<Matrix4x4>)
                value
            |> node.set

        [<Extension>]
        static member translation<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Vector3) =
            createClrProperty
                translationPropertyId
                (fun uiElement _ -> uiElement.Translation <- value)
                (fun uiElement -> uiElement.Translation <- Unchecked.defaultof<Vector3>)
                value
            |> node.set

        [<Extension>]
        static member translationTransition<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Vector3Transition) =
            createClrProperty
                translationTransitionPropertyId
                (fun uiElement _ -> uiElement.TranslationTransition <- value)
                (fun uiElement -> uiElement.TranslationTransition <- Unchecked.defaultof<Vector3Transition>)
                value
            |> node.set

        [<Extension>]
        static member xamlRoot<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: XamlRoot) =
            createClrProperty
                xamlRootPropertyId
                (fun uiElement _ -> uiElement.XamlRoot <- value)
                (fun uiElement -> uiElement.XamlRoot <- Unchecked.defaultof<XamlRoot>)
                value
            |> node.set

        [<Extension>]
        static member onAccessKeyDisplayDismissed<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: AccessKeyDisplayDismissedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "AccessKeyDisplayDismissed"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, AccessKeyDisplayDismissedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_AccessKeyDisplayDismissed h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_AccessKeyDisplayDismissed h }
                )
            })

        [<Extension>]
        static member onAccessKeyDisplayRequested<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: AccessKeyDisplayRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "AccessKeyDisplayRequested"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, AccessKeyDisplayRequestedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_AccessKeyDisplayRequested h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_AccessKeyDisplayRequested h }
                )
            })

        [<Extension>]
        static member onAccessKeyInvoked<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: AccessKeyInvokedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "AccessKeyInvoked"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, AccessKeyInvokedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_AccessKeyInvoked h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_AccessKeyInvoked h }
                )
            })

        [<Extension>]
        static member onBringIntoViewRequested<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: BringIntoViewRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "BringIntoViewRequested"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, BringIntoViewRequestedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_BringIntoViewRequested h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_BringIntoViewRequested h }
                )
            })

        [<Extension>]
        static member onCharacterReceived<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: CharacterReceivedRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CharacterReceived"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, CharacterReceivedRoutedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_CharacterReceived h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_CharacterReceived h }
                )
            })

        [<Extension>]
        static member onContextCanceled<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContextCanceled"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, RoutedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_ContextCanceled h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_ContextCanceled h }
                )
            })

        [<Extension>]
        static member onContextRequested<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: ContextRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContextRequested"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, ContextRequestedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_ContextRequested h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_ContextRequested h }
                )
            })

        [<Extension>]
        static member onDoubleTapped<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: DoubleTappedRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DoubleTapped"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = DoubleTappedEventHandler(fun _ e -> handler e)
                    uiElementElement.DoubleTapped.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.DoubleTapped.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDragEnter<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: DragEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragEnter"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = DragEventHandler(fun _ e -> handler e)
                    uiElementElement.DragEnter.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.DragEnter.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDragLeave<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: DragEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragLeave"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = DragEventHandler(fun _ e -> handler e)
                    uiElementElement.DragLeave.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.DragLeave.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDragOver<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: DragEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragOver"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = DragEventHandler(fun _ e -> handler e)
                    uiElementElement.DragOver.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.DragOver.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDragStarting<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: DragStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragStarting"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, DragStartingEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_DragStarting h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_DragStarting h }
                )
            })

        [<Extension>]
        static member onDrop<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: DragEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Drop"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = DragEventHandler(fun _ e -> handler e)
                    uiElementElement.Drop.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.Drop.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDropCompleted<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: DropCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DropCompleted"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, DropCompletedEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_DropCompleted h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_DropCompleted h }
                )
            })

        [<Extension>]
        static member onGettingFocus<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: GettingFocusEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "GettingFocus"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, GettingFocusEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_GettingFocus h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_GettingFocus h }
                )
            })

        [<Extension>]
        static member onGotFocus<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "GotFocus"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    uiElementElement.GotFocus.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.GotFocus.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onHolding<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: HoldingRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Holding"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = HoldingEventHandler(fun _ e -> handler e)
                    uiElementElement.Holding.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.Holding.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onKeyDown<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: KeyRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "KeyDown"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = KeyEventHandler(fun _ e -> handler e)
                    uiElementElement.KeyDown.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.KeyDown.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onKeyUp<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: KeyRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "KeyUp"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = KeyEventHandler(fun _ e -> handler e)
                    uiElementElement.KeyUp.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.KeyUp.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onLosingFocus<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: LosingFocusEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "LosingFocus"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, LosingFocusEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_LosingFocus h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_LosingFocus h }
                )
            })

        [<Extension>]
        static member onLostFocus<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "LostFocus"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    uiElementElement.LostFocus.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.LostFocus.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onManipulationCompleted<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: ManipulationCompletedRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ManipulationCompleted"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = ManipulationCompletedEventHandler(fun _ e -> handler e)
                    uiElementElement.ManipulationCompleted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.ManipulationCompleted.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onManipulationDelta<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: ManipulationDeltaRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ManipulationDelta"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = ManipulationDeltaEventHandler(fun _ e -> handler e)
                    uiElementElement.ManipulationDelta.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.ManipulationDelta.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onManipulationInertiaStarting<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: ManipulationInertiaStartingRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ManipulationInertiaStarting"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = ManipulationInertiaStartingEventHandler(fun _ e -> handler e)
                    uiElementElement.ManipulationInertiaStarting.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.ManipulationInertiaStarting.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onManipulationStarted<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: ManipulationStartedRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ManipulationStarted"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = ManipulationStartedEventHandler(fun _ e -> handler e)
                    uiElementElement.ManipulationStarted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.ManipulationStarted.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onManipulationStarting<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: ManipulationStartingRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ManipulationStarting"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = ManipulationStartingEventHandler(fun _ e -> handler e)
                    uiElementElement.ManipulationStarting.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.ManipulationStarting.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onNoFocusCandidateFound<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: NoFocusCandidateFoundEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "NoFocusCandidateFound"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, NoFocusCandidateFoundEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_NoFocusCandidateFound h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_NoFocusCandidateFound h }
                )
            })

        [<Extension>]
        static member onPointerCanceled<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerCanceled"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerCanceled.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerCanceled.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPointerCaptureLost<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerCaptureLost"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerCaptureLost.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerCaptureLost.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPointerEntered<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerEntered"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerEntered.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerEntered.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPointerExited<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerExited"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerExited.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerExited.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPointerMoved<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerMoved"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerMoved.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerMoved.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPointerPressed<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerPressed"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerPressed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerPressed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPointerReleased<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerReleased"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerReleased.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerReleased.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPointerWheelChanged<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: PointerRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PointerWheelChanged"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = PointerEventHandler(fun _ e -> handler e)
                    uiElementElement.PointerWheelChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PointerWheelChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPreviewKeyDown<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: KeyRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PreviewKeyDown"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = KeyEventHandler(fun _ e -> handler e)
                    uiElementElement.PreviewKeyDown.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PreviewKeyDown.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPreviewKeyUp<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: KeyRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PreviewKeyUp"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = KeyEventHandler(fun _ e -> handler e)
                    uiElementElement.PreviewKeyUp.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.PreviewKeyUp.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onProcessKeyboardAccelerators<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: ProcessKeyboardAcceleratorEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ProcessKeyboardAccelerators"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TypedEventHandler<UIElement, ProcessKeyboardAcceleratorEventArgs>(fun _ e -> handler e)
                    uiElementElement.add_ProcessKeyboardAccelerators h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.remove_ProcessKeyboardAccelerators h }
                )
            })

        [<Extension>]
        static member onRightTapped<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: RightTappedRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "RightTapped"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = RightTappedEventHandler(fun _ e -> handler e)
                    uiElementElement.RightTapped.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.RightTapped.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onTapped<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: TappedRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Tapped"
                Subscribe = (fun uiElement ->
                    let uiElementElement = uiElement :?> UIElement
                    let h = TappedEventHandler(fun _ e -> handler e)
                    uiElementElement.Tapped.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = uiElementElement.Tapped.RemoveHandler h }
                )
            })
