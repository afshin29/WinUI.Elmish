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
module RadioMenuFlyoutItemMod =

    let private radioMenuFlyoutItemNodeId = getNextId ()

    registerDefaultRenderer<RadioMenuFlyoutItem> radioMenuFlyoutItemNodeId

    let private areCheckStatesEnabledPropertyId = getNextId ()
    let private groupNamePropertyId = getNextId ()
    let private isCheckedPropertyId = getNextId ()

    [<Extension>]
    type RadioMenuFlyoutItemExtensions =

        [<Extension>]
        static member radioMenuFlyoutItem_areCheckStatesEnabled(node: VirtualNode<MenuFlyoutSubItem>, value: bool) =
            createDependencyProperty areCheckStatesEnabledPropertyId RadioMenuFlyoutItem.AreCheckStatesEnabledProperty value
            |> node.set

        [<Extension>]
        static member groupName(node: VirtualNode<RadioMenuFlyoutItem>, value: string) =
            createDependencyProperty groupNamePropertyId RadioMenuFlyoutItem.GroupNameProperty value
            |> node.set

        [<Extension>]
        static member isChecked(node: VirtualNode<RadioMenuFlyoutItem>, value: bool) =
            createDependencyProperty isCheckedPropertyId RadioMenuFlyoutItem.IsCheckedProperty value
            |> node.set

    let radioMenuFlyoutItem () =
        createNode<RadioMenuFlyoutItem> radioMenuFlyoutItemNodeId
