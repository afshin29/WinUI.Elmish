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
module MenuFlyoutSubItemMod =

    let private menuFlyoutSubItemNodeId = getNextId ()

    registerDefaultRenderer<MenuFlyoutSubItem> menuFlyoutSubItemNodeId

    let private iconPropertyId = getNextId ()
    let private textPropertyId = getNextId ()
    let private itemsPropertyId = getNextId ()

    [<Extension>]
    type MenuFlyoutSubItemExtensions =

        [<Extension>]
        static member icon(node: VirtualNode<MenuFlyoutSubItem>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                iconPropertyId
                value
                (fun element child -> (element :?> MenuFlyoutSubItem).Icon <- (child :?> IconElement))
                (fun element -> (element :?> MenuFlyoutSubItem).Icon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member text(node: VirtualNode<MenuFlyoutSubItem>, value: string) =
            createDependencyProperty textPropertyId MenuFlyoutSubItem.TextProperty value
            |> node.set

        [<Extension>]
        static member items(node: VirtualNode<MenuFlyoutSubItem>, value: IVirtualNode list) =
            createInnerChildrenProperty
                itemsPropertyId
                value
                (fun element children ->
                    children
                    |> Seq.cast<MenuFlyoutItemBase>
                    |> Seq.toList
                    |> List.iter (element :?> MenuFlyoutSubItem).Items.Add)
                (fun element ->
                    (element :?> MenuFlyoutSubItem).Items
                    |> Seq.cast<UIElement>
                    |> ResizeArray
                    :> IList<_>)
            |> node.set

    let menuFlyoutSubItem () =
        createNode<MenuFlyoutSubItem> menuFlyoutSubItemNodeId
