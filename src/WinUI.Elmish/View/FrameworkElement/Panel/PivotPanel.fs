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
module PivotPanelMod =

    let private pivotPanelNodeId = getNextId ()

    registerDefaultRenderer<PivotPanel> pivotPanelNodeId

    [<Extension>]
    type PivotPanelExtensions =

        [<Extension>]
        static member onHorizontalSnapPointsChanged(node: VirtualNode<PivotPanel>, handler: PivotPanel -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "HorizontalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let pivotPanel = uiElement :?> PivotPanel
                    let h = EventHandler<obj>(fun _ e -> handler pivotPanel e)
                    pivotPanel.HorizontalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = pivotPanel.HorizontalSnapPointsChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onVerticalSnapPointsChanged(node: VirtualNode<PivotPanel>, handler: PivotPanel -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "VerticalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let pivotPanel = uiElement :?> PivotPanel
                    let h = EventHandler<obj>(fun _ e -> handler pivotPanel e)
                    pivotPanel.VerticalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = pivotPanel.VerticalSnapPointsChanged.RemoveHandler h }
                )
            })

    let pivotPanel () =
        createNode<PivotPanel> pivotPanelNodeId
