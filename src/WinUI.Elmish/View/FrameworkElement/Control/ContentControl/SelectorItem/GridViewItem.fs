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
module GridViewItemMod =

    let private gridViewItemNodeId = getNextId ()

    registerDefaultRenderer<GridViewItem> gridViewItemNodeId

    let gridViewItem () =
        createNode<GridViewItem> gridViewItemNodeId
