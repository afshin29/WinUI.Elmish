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


[<AutoOpen>]
module ListViewHeaderItemMod =

    let private listViewHeaderItemNodeId = getNextId ()

    registerDefaultRenderer<ListViewHeaderItem> listViewHeaderItemNodeId

    let listViewHeaderItem () =
        createNode<ListViewHeaderItem> listViewHeaderItemNodeId
