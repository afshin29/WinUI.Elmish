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
module ListViewItemMod =

    let private listViewItemNodeId = getNextId ()

    registerDefaultRenderer<ListViewItem> listViewItemNodeId

    let listViewItem () =
        createNode<ListViewItem> listViewItemNodeId
