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
module PivotHeaderItemMod =

    let private pivotHeaderItemNodeId = getNextId ()

    registerDefaultRenderer<PivotHeaderItem> pivotHeaderItemNodeId

    let pivotHeaderItem () =
        createNode<PivotHeaderItem> pivotHeaderItemNodeId
