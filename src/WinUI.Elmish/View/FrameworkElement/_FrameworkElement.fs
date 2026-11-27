namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property ActualHeightProperty: read-only
// skipped dependency property ActualThemeProperty: read-only
// skipped dependency property ActualWidthProperty: read-only
// skipped property BaseUri: read-only
// skipped property IsLoaded: read-only
// skipped property Parent: read-only
// skipped property Triggers: read-only


[<AutoOpen>]
module FrameworkElementMod =

    let private allowFocusOnInteractionPropertyId = getNextId ()
    let private allowFocusWhenDisabledPropertyId = getNextId ()
    let private dataContextPropertyId = getNextId ()
    let private flowDirectionPropertyId = getNextId ()
    let private focusVisualMarginPropertyId = getNextId ()
    let private focusVisualPrimaryBrushPropertyId = getNextId ()
    let private focusVisualPrimaryThicknessPropertyId = getNextId ()
    let private focusVisualSecondaryBrushPropertyId = getNextId ()
    let private focusVisualSecondaryThicknessPropertyId = getNextId ()
    let private heightPropertyId = getNextId ()
    let private horizontalAlignmentPropertyId = getNextId ()
    let private languagePropertyId = getNextId ()
    let private marginPropertyId = getNextId ()
    let private maxHeightPropertyId = getNextId ()
    let private maxWidthPropertyId = getNextId ()
    let private minHeightPropertyId = getNextId ()
    let private minWidthPropertyId = getNextId ()
    let private namePropertyId = getNextId ()
    let private requestedThemePropertyId = getNextId ()
    let private stylePropertyId = getNextId ()
    let private tagPropertyId = getNextId ()
    let private verticalAlignmentPropertyId = getNextId ()
    let private widthPropertyId = getNextId ()
    let private resourcesPropertyId = getNextId ()

    [<Extension>]
    type FrameworkElementExtensions =

        [<Extension>]
        static member allowFocusOnInteraction<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty allowFocusOnInteractionPropertyId FrameworkElement.AllowFocusOnInteractionProperty value
            |> node.set

        [<Extension>]
        static member allowFocusWhenDisabled<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty allowFocusWhenDisabledPropertyId FrameworkElement.AllowFocusWhenDisabledProperty value
            |> node.set

        [<Extension>]
        static member dataContext<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty dataContextPropertyId FrameworkElement.DataContextProperty value
            |> node.set

        [<Extension>]
        static member flowDirection<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: FlowDirection) =
            createDependencyProperty flowDirectionPropertyId FrameworkElement.FlowDirectionProperty value
            |> node.set

        [<Extension>]
        static member focusVisualMargin<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty focusVisualMarginPropertyId FrameworkElement.FocusVisualMarginProperty value
            |> node.set

        [<Extension>]
        static member focusVisualPrimaryBrush<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty focusVisualPrimaryBrushPropertyId FrameworkElement.FocusVisualPrimaryBrushProperty value
            |> node.set

        [<Extension>]
        static member focusVisualPrimaryThickness<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty focusVisualPrimaryThicknessPropertyId FrameworkElement.FocusVisualPrimaryThicknessProperty value
            |> node.set

        [<Extension>]
        static member focusVisualSecondaryBrush<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty focusVisualSecondaryBrushPropertyId FrameworkElement.FocusVisualSecondaryBrushProperty value
            |> node.set

        [<Extension>]
        static member focusVisualSecondaryThickness<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty focusVisualSecondaryThicknessPropertyId FrameworkElement.FocusVisualSecondaryThicknessProperty value
            |> node.set

        [<Extension>]
        static member height<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty heightPropertyId FrameworkElement.HeightProperty value
            |> node.set

        [<Extension>]
        static member horizontalAlignment<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: HorizontalAlignment) =
            createDependencyProperty horizontalAlignmentPropertyId FrameworkElement.HorizontalAlignmentProperty value
            |> node.set

        [<Extension>]
        static member language<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty languagePropertyId FrameworkElement.LanguageProperty value
            |> node.set

        [<Extension>]
        static member margin<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty marginPropertyId FrameworkElement.MarginProperty value
            |> node.set

        [<Extension>]
        static member maxHeight<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty maxHeightPropertyId FrameworkElement.MaxHeightProperty value
            |> node.set

        [<Extension>]
        static member maxWidth<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty maxWidthPropertyId FrameworkElement.MaxWidthProperty value
            |> node.set

        [<Extension>]
        static member minHeight<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty minHeightPropertyId FrameworkElement.MinHeightProperty value
            |> node.set

        [<Extension>]
        static member minWidth<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty minWidthPropertyId FrameworkElement.MinWidthProperty value
            |> node.set

        [<Extension>]
        static member name<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty namePropertyId FrameworkElement.NameProperty value
            |> node.set

        [<Extension>]
        static member requestedTheme<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: ElementTheme) =
            createDependencyProperty requestedThemePropertyId FrameworkElement.RequestedThemeProperty value
            |> node.set

        [<Extension>]
        static member style<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Style) =
            createDependencyProperty stylePropertyId FrameworkElement.StyleProperty value
            |> node.set

        [<Extension>]
        static member tag<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty tagPropertyId FrameworkElement.TagProperty value
            |> node.set

        [<Extension>]
        static member verticalAlignment<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: VerticalAlignment) =
            createDependencyProperty verticalAlignmentPropertyId FrameworkElement.VerticalAlignmentProperty value
            |> node.set

        [<Extension>]
        static member center(node: VirtualNode<_>) =
            node.horizontalAlignment(HorizontalAlignment.Center)
                .verticalAlignment(VerticalAlignment.Center)

        [<Extension>]
        static member width<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty widthPropertyId FrameworkElement.WidthProperty value
            |> node.set

        [<Extension>]
        static member resources<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: ResourceDictionary) =
            createClrProperty
                resourcesPropertyId
                (fun uiElement _ -> (uiElement :?> FrameworkElement).Resources <- value)
                (fun uiElement -> (uiElement :?> FrameworkElement).Resources <- Unchecked.defaultof<ResourceDictionary>)
                value
            |> node.set

        [<Extension>]
        static member onActualThemeChanged<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ActualThemeChanged"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = TypedEventHandler<FrameworkElement, obj>(fun _ e -> handler sender e)
                    frameworkElement.add_ActualThemeChanged h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.remove_ActualThemeChanged h }
                )
            })

        [<Extension>]
        static member onDataContextChanged<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> DataContextChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DataContextChanged"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = TypedEventHandler<FrameworkElement, DataContextChangedEventArgs>(fun _ e -> handler sender e)
                    frameworkElement.add_DataContextChanged h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.remove_DataContextChanged h }
                )
            })

        [<Extension>]
        static member onEffectiveViewportChanged<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> EffectiveViewportChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "EffectiveViewportChanged"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = TypedEventHandler<FrameworkElement, EffectiveViewportChangedEventArgs>(fun _ e -> handler sender e)
                    frameworkElement.add_EffectiveViewportChanged h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.remove_EffectiveViewportChanged h }
                )
            })

        [<Extension>]
        static member onLayoutUpdated<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "LayoutUpdated"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = EventHandler<obj>(fun _ e -> handler sender e)
                    frameworkElement.LayoutUpdated.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.LayoutUpdated.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onLoaded<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Loaded"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = RoutedEventHandler(fun _ e -> handler sender e)
                    frameworkElement.Loaded.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.Loaded.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onLoading<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Loading"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = TypedEventHandler<FrameworkElement, obj>(fun _ e -> handler sender e)
                    frameworkElement.add_Loading h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.remove_Loading h }
                )
            })

        [<Extension>]
        static member onSizeChanged<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> SizeChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SizeChanged"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = SizeChangedEventHandler(fun _ e -> handler sender e)
                    frameworkElement.SizeChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.SizeChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onUnloaded<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Unloaded"
                Subscribe = (fun uiElement ->
                    let frameworkElement = uiElement :?> FrameworkElement
                    let sender = uiElement :?> 'm
                    let h = RoutedEventHandler(fun _ e -> handler sender e)
                    frameworkElement.Unloaded.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frameworkElement.Unloaded.RemoveHandler h }
                )
            })
