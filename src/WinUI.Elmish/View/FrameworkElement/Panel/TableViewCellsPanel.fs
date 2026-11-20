namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Tabular
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module TableViewCellsPanelMod =

    let private tableViewCellsPanelNodeId = getNextId ()

    registerDefaultRenderer<TableViewCellsPanel> tableViewCellsPanelNodeId

    let tableViewCellsPanel () =
        createNode<TableViewCellsPanel> tableViewCellsPanelNodeId
