namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property AreHorizontalSnapPointsRegular: read-only
// skipped property AreVerticalSnapPointsRegular: read-only
// skipped property ExtentHeight: read-only
// skipped property ExtentWidth: read-only
// skipped property HorizontalOffset: read-only
// skipped property VerticalOffset: read-only
// skipped property ViewportHeight: read-only
// skipped property ViewportWidth: read-only


[<AutoOpen>]
module CarouselPanelMod =

    let private carouselPanelNodeId = getNextId ()

    registerDefaultRenderer<CarouselPanel> carouselPanelNodeId

    let private canHorizontallyScrollPropertyId = getNextId ()
    let private canVerticallyScrollPropertyId = getNextId ()
    let private scrollOwnerPropertyId = getNextId ()

    [<Extension>]
    type CarouselPanelExtensions =

        [<Extension>]
        static member canHorizontallyScroll(node: VirtualNode<CarouselPanel>, value: bool) =
            createClrProperty
                canHorizontallyScrollPropertyId
                (fun uiElement _ -> (uiElement :?> CarouselPanel).CanHorizontallyScroll <- value)
                (fun uiElement -> (uiElement :?> CarouselPanel).CanHorizontallyScroll <- Unchecked.defaultof<bool>)
                value
            |> node.set

        [<Extension>]
        static member canVerticallyScroll(node: VirtualNode<CarouselPanel>, value: bool) =
            createClrProperty
                canVerticallyScrollPropertyId
                (fun uiElement _ -> (uiElement :?> CarouselPanel).CanVerticallyScroll <- value)
                (fun uiElement -> (uiElement :?> CarouselPanel).CanVerticallyScroll <- Unchecked.defaultof<bool>)
                value
            |> node.set

        [<Extension>]
        static member scrollOwner(node: VirtualNode<CarouselPanel>, value: obj) =
            createClrProperty
                scrollOwnerPropertyId
                (fun uiElement _ -> (uiElement :?> CarouselPanel).ScrollOwner <- value)
                (fun uiElement -> (uiElement :?> CarouselPanel).ScrollOwner <- Unchecked.defaultof<obj>)
                value
            |> node.set

        [<Extension>]
        static member onHorizontalSnapPointsChanged(node: VirtualNode<CarouselPanel>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "HorizontalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let carouselPanel = uiElement :?> CarouselPanel
                    let h = EventHandler<obj>(fun _ e -> handler e)
                    carouselPanel.HorizontalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = carouselPanel.HorizontalSnapPointsChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onVerticalSnapPointsChanged(node: VirtualNode<CarouselPanel>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "VerticalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let carouselPanel = uiElement :?> CarouselPanel
                    let h = EventHandler<obj>(fun _ e -> handler e)
                    carouselPanel.VerticalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = carouselPanel.VerticalSnapPointsChanged.RemoveHandler h }
                )
            })

    let carouselPanel () =
        createNode<CarouselPanel> carouselPanelNodeId
