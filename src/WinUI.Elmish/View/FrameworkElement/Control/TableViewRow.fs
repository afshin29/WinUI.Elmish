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

// skipped dependency property IsExpandableProperty: read-only
// skipped dependency property IsExpandedProperty: read-only
// skipped dependency property IsSelectedProperty: read-only
// skipped dependency property LevelProperty: read-only


[<AutoOpen>]
module TableViewRowMod =

    let private tableViewRowNodeId = getNextId ()

    registerDefaultRenderer<TableViewRow> tableViewRowNodeId

    let tableViewRow () =
        createNode<TableViewRow> tableViewRowNodeId
