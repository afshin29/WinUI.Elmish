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
module OrientedVirtualizingPanelMod =

    let private canHorizontallyScrollPropertyId = getNextId ()
    let private canVerticallyScrollPropertyId = getNextId ()
    let private scrollOwnerPropertyId = getNextId ()

    [<Extension>]
    type OrientedVirtualizingPanelExtensions =

        [<Extension>]
        static member canHorizontallyScroll<'m when 'm :> OrientedVirtualizingPanel and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createClrProperty
                canHorizontallyScrollPropertyId
                (fun uiElement _ -> (uiElement :?> OrientedVirtualizingPanel).CanHorizontallyScroll <- value)
                (fun uiElement -> (uiElement :?> OrientedVirtualizingPanel).CanHorizontallyScroll <- Unchecked.defaultof<bool>)
                value
            |> node.set

        [<Extension>]
        static member canVerticallyScroll<'m when 'm :> OrientedVirtualizingPanel and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createClrProperty
                canVerticallyScrollPropertyId
                (fun uiElement _ -> (uiElement :?> OrientedVirtualizingPanel).CanVerticallyScroll <- value)
                (fun uiElement -> (uiElement :?> OrientedVirtualizingPanel).CanVerticallyScroll <- Unchecked.defaultof<bool>)
                value
            |> node.set

        [<Extension>]
        static member scrollOwner<'m when 'm :> OrientedVirtualizingPanel and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createClrProperty
                scrollOwnerPropertyId
                (fun uiElement _ -> (uiElement :?> OrientedVirtualizingPanel).ScrollOwner <- value)
                (fun uiElement -> (uiElement :?> OrientedVirtualizingPanel).ScrollOwner <- Unchecked.defaultof<obj>)
                value
            |> node.set

        [<Extension>]
        static member onHorizontalSnapPointsChanged<'m when 'm :> OrientedVirtualizingPanel and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "HorizontalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let orientedVirtualizingPanel = uiElement :?> OrientedVirtualizingPanel
                    let sender = uiElement :?> 'm
                    let h = EventHandler<obj>(fun _ e -> handler sender e)
                    orientedVirtualizingPanel.HorizontalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = orientedVirtualizingPanel.HorizontalSnapPointsChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onVerticalSnapPointsChanged<'m when 'm :> OrientedVirtualizingPanel and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "VerticalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let orientedVirtualizingPanel = uiElement :?> OrientedVirtualizingPanel
                    let sender = uiElement :?> 'm
                    let h = EventHandler<obj>(fun _ e -> handler sender e)
                    orientedVirtualizingPanel.VerticalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = orientedVirtualizingPanel.VerticalSnapPointsChanged.RemoveHandler h }
                )
            })
