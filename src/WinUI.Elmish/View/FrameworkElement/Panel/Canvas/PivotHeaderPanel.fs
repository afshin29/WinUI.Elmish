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


[<AutoOpen>]
module PivotHeaderPanelMod =

    let private pivotHeaderPanelNodeId = getNextId ()

    registerDefaultRenderer<PivotHeaderPanel> pivotHeaderPanelNodeId

    let pivotHeaderPanel () =
        createNode<PivotHeaderPanel> pivotHeaderPanelNodeId
