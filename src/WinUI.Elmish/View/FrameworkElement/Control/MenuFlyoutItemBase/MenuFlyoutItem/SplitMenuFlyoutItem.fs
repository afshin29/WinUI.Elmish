namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module SplitMenuFlyoutItemMod =

    let private splitMenuFlyoutItemNodeId = getNextId ()

    registerDefaultRenderer<SplitMenuFlyoutItem> splitMenuFlyoutItemNodeId

    let private subMenuItemStylePropertyId = getNextId ()
    let private subMenuPresenterStylePropertyId = getNextId ()
    let private itemsPropertyId = getNextId ()

    [<Extension>]
    type SplitMenuFlyoutItemExtensions =

        [<Extension>]
        static member subMenuItemStyle(node: VirtualNode<SplitMenuFlyoutItem>, value: Style) =
            createDependencyProperty subMenuItemStylePropertyId SplitMenuFlyoutItem.SubMenuItemStyleProperty value
            |> node.set

        [<Extension>]
        static member subMenuPresenterStyle(node: VirtualNode<SplitMenuFlyoutItem>, value: Style) =
            createDependencyProperty subMenuPresenterStylePropertyId SplitMenuFlyoutItem.SubMenuPresenterStyleProperty value
            |> node.set

        [<Extension>]
        static member items(node: VirtualNode<SplitMenuFlyoutItem>, value: IVirtualNode list) =
            createInnerChildrenProperty
                itemsPropertyId
                value
                (fun element children ->
                    children
                    |> Seq.cast<MenuFlyoutItemBase>
                    |> Seq.toList
                    |> List.iter (element :?> SplitMenuFlyoutItem).Items.Add)
                (fun element ->
                    (element :?> SplitMenuFlyoutItem).Items
                    |> Seq.cast<UIElement>
                    |> ResizeArray
                    :> IList<_>)
            |> node.set

    let splitMenuFlyoutItem () =
        createNode<SplitMenuFlyoutItem> splitMenuFlyoutItemNodeId
