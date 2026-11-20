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

// skipped dependency property ItemsProperty: read-only


[<AutoOpen>]
module MenuBarItemMod =

    let private menuBarItemNodeId = getNextId ()

    registerDefaultRenderer<MenuBarItem> menuBarItemNodeId

    let private titlePropertyId = getNextId ()

    [<Extension>]
    type MenuBarItemExtensions =

        [<Extension>]
        static member title(node: VirtualNode<MenuBarItem>, value: string) =
            createDependencyProperty titlePropertyId MenuBarItem.TitleProperty value
            |> node.set

    let menuBarItem () =
        createNode<MenuBarItem> menuBarItemNodeId
