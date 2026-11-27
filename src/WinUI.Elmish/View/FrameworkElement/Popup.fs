namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Media
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property ActualPlacement: read-only
// skipped property IsConstrainedToRootBounds: read-only


[<AutoOpen>]
module PopupMod =

    let private popupNodeId = getNextId ()

    registerDefaultRenderer<Popup> popupNodeId

    let private childPropertyId = getNextId ()
    let private childTransitionsPropertyId = getNextId ()
    let private desiredPlacementPropertyId = getNextId ()
    let private horizontalOffsetPropertyId = getNextId ()
    let private isLightDismissEnabledPropertyId = getNextId ()
    let private isOpenPropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()
    let private placementTargetPropertyId = getNextId ()
    let private shouldConstrainToRootBoundsPropertyId = getNextId ()
    let private systemBackdropPropertyId = getNextId ()
    let private verticalOffsetPropertyId = getNextId ()

    [<Extension>]
    type PopupExtensions =

        [<Extension>]
        static member child(node: VirtualNode<Popup>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                childPropertyId
                value
                (fun element child -> (element :?> Popup).Child <- child)
                (fun element -> (element :?> Popup).Child)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member childTransitions(node: VirtualNode<Popup>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty childTransitionsPropertyId Popup.ChildTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member desiredPlacement(node: VirtualNode<Popup>, value: PopupPlacementMode) =
            createDependencyProperty desiredPlacementPropertyId Popup.DesiredPlacementProperty value
            |> node.set

        [<Extension>]
        static member horizontalOffset(node: VirtualNode<Popup>, value: float) =
            createDependencyProperty horizontalOffsetPropertyId Popup.HorizontalOffsetProperty value
            |> node.set

        [<Extension>]
        static member isLightDismissEnabled(node: VirtualNode<Popup>, value: bool) =
            createDependencyProperty isLightDismissEnabledPropertyId Popup.IsLightDismissEnabledProperty value
            |> node.set

        [<Extension>]
        static member isOpen(node: VirtualNode<Popup>, value: bool) =
            createDependencyProperty isOpenPropertyId Popup.IsOpenProperty value
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode(node: VirtualNode<Popup>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId Popup.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member placementTarget(node: VirtualNode<Popup>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                placementTargetPropertyId
                value
                (fun element child -> (element :?> Popup).PlacementTarget <- (child :?> FrameworkElement))
                (fun element -> (element :?> Popup).PlacementTarget :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member shouldConstrainToRootBounds(node: VirtualNode<Popup>, value: bool) =
            createDependencyProperty shouldConstrainToRootBoundsPropertyId Popup.ShouldConstrainToRootBoundsProperty value
            |> node.set

        [<Extension>]
        static member systemBackdrop(node: VirtualNode<Popup>, value: SystemBackdrop) =
            createDependencyProperty systemBackdropPropertyId Popup.SystemBackdropProperty value
            |> node.set

        [<Extension>]
        static member verticalOffset(node: VirtualNode<Popup>, value: float) =
            createDependencyProperty verticalOffsetPropertyId Popup.VerticalOffsetProperty value
            |> node.set

        [<Extension>]
        static member onActualPlacementChanged(node: VirtualNode<Popup>, handler: Popup -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ActualPlacementChanged"
                Subscribe = (fun uiElement ->
                    let popup = uiElement :?> Popup
                    let h = EventHandler<obj>(fun _ e -> handler popup e)
                    popup.ActualPlacementChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = popup.ActualPlacementChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onClosed(node: VirtualNode<Popup>, handler: Popup -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closed"
                Subscribe = (fun uiElement ->
                    let popup = uiElement :?> Popup
                    let h = EventHandler<obj>(fun _ e -> handler popup e)
                    popup.Closed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = popup.Closed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onOpened(node: VirtualNode<Popup>, handler: Popup -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opened"
                Subscribe = (fun uiElement ->
                    let popup = uiElement :?> Popup
                    let h = EventHandler<obj>(fun _ e -> handler popup e)
                    popup.Opened.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = popup.Opened.RemoveHandler h }
                )
            })

    let popup () =
        createNode<Popup> popupNodeId
