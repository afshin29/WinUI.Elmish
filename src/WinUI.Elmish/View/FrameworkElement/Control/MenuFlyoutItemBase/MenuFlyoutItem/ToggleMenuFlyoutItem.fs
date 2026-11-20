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
module ToggleMenuFlyoutItemMod =

    let private toggleMenuFlyoutItemNodeId = getNextId ()

    registerDefaultRenderer<ToggleMenuFlyoutItem> toggleMenuFlyoutItemNodeId

    let private isCheckedPropertyId = getNextId ()

    [<Extension>]
    type ToggleMenuFlyoutItemExtensions =

        [<Extension>]
        static member isChecked(node: VirtualNode<ToggleMenuFlyoutItem>, value: bool) =
            createDependencyProperty isCheckedPropertyId ToggleMenuFlyoutItem.IsCheckedProperty value
            |> node.set

    let toggleMenuFlyoutItem () =
        createNode<ToggleMenuFlyoutItem> toggleMenuFlyoutItemNodeId
