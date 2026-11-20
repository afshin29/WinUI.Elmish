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


[<AutoOpen>]
module LoopingSelectorPanelMod =

    [<Extension>]
    type LoopingSelectorPanelExtensions =

        [<Extension>]
        static member onHorizontalSnapPointsChanged(node: VirtualNode<LoopingSelectorPanel>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "HorizontalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let loopingSelectorPanel = uiElement :?> LoopingSelectorPanel
                    let h = EventHandler<obj>(fun _ e -> handler e)
                    loopingSelectorPanel.HorizontalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = loopingSelectorPanel.HorizontalSnapPointsChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onVerticalSnapPointsChanged(node: VirtualNode<LoopingSelectorPanel>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "VerticalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let loopingSelectorPanel = uiElement :?> LoopingSelectorPanel
                    let h = EventHandler<obj>(fun _ e -> handler e)
                    loopingSelectorPanel.VerticalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = loopingSelectorPanel.VerticalSnapPointsChanged.RemoveHandler h }
                )
            })
