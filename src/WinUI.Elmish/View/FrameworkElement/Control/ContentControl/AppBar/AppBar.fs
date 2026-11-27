namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module AppBarMod =

    let private appBarNodeId = getNextId ()

    registerDefaultRenderer<AppBar> appBarNodeId

    let private closedDisplayModePropertyId = getNextId ()
    let private isOpenPropertyId = getNextId ()
    let private isStickyPropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()

    [<Extension>]
    type AppBarExtensions =

        [<Extension>]
        static member closedDisplayMode<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, value: AppBarClosedDisplayMode) =
            createDependencyProperty closedDisplayModePropertyId AppBar.ClosedDisplayModeProperty value
            |> node.set

        [<Extension>]
        static member isOpen<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isOpenPropertyId AppBar.IsOpenProperty value
            |> node.set

        [<Extension>]
        static member isSticky<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isStickyPropertyId AppBar.IsStickyProperty value
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId AppBar.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member onClosed<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closed"
                Subscribe = (fun uiElement ->
                    let appBar = uiElement :?> AppBar
                    let sender = uiElement :?> 'm
                    let h = EventHandler<obj>(fun _ e -> handler sender e)
                    appBar.Closed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = appBar.Closed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onClosing<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closing"
                Subscribe = (fun uiElement ->
                    let appBar = uiElement :?> AppBar
                    let sender = uiElement :?> 'm
                    let h = EventHandler<obj>(fun _ e -> handler sender e)
                    appBar.Closing.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = appBar.Closing.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onOpened<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opened"
                Subscribe = (fun uiElement ->
                    let appBar = uiElement :?> AppBar
                    let sender = uiElement :?> 'm
                    let h = EventHandler<obj>(fun _ e -> handler sender e)
                    appBar.Opened.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = appBar.Opened.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onOpening<'m when 'm :> AppBar and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opening"
                Subscribe = (fun uiElement ->
                    let appBar = uiElement :?> AppBar
                    let sender = uiElement :?> 'm
                    let h = EventHandler<obj>(fun _ e -> handler sender e)
                    appBar.Opening.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = appBar.Opening.RemoveHandler h }
                )
            })

    let appBar () =
        createNode<AppBar> appBarNodeId
