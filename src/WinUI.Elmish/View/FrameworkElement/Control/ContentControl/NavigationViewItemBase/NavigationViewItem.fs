namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property CompactPaneLengthProperty: read-only
// skipped dependency property MenuItemsProperty: read-only


[<AutoOpen>]
module NavigationViewItemMod =

    let private navigationViewItemNodeId = getNextId ()

    registerDefaultRenderer<NavigationViewItem> navigationViewItemNodeId

    let private hasUnrealizedChildrenPropertyId = getNextId ()
    let private iconPropertyId = getNextId ()
    let private infoBadgePropertyId = getNextId ()
    let private isChildSelectedPropertyId = getNextId ()
    let private isExpandedPropertyId = getNextId ()
    let private menuItemsSourcePropertyId = getNextId ()
    let private menuItemsPropertyId = getNextId ()
    let private selectsOnInvokedPropertyId = getNextId ()

    [<Extension>]
    type NavigationViewItemExtensions =

        [<Extension>]
        static member hasUnrealizedChildren(node: VirtualNode<NavigationViewItem>, value: bool) =
            createDependencyProperty hasUnrealizedChildrenPropertyId NavigationViewItem.HasUnrealizedChildrenProperty value
            |> node.set

        [<Extension>]
        static member icon(node: VirtualNode<NavigationViewItem>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                iconPropertyId
                value
                (fun element child -> (element :?> NavigationViewItem).Icon <- (child :?> IconElement))
                (fun element -> (element :?> NavigationViewItem).Icon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member infoBadge(node: VirtualNode<NavigationViewItem>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                infoBadgePropertyId
                value
                (fun element child -> (element :?> NavigationViewItem).InfoBadge <- (child :?> InfoBadge))
                (fun element -> (element :?> NavigationViewItem).InfoBadge :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member isChildSelected(node: VirtualNode<NavigationViewItem>, value: bool) =
            createDependencyProperty isChildSelectedPropertyId NavigationViewItem.IsChildSelectedProperty value
            |> node.set

        [<Extension>]
        static member isExpanded(node: VirtualNode<NavigationViewItem>, value: bool) =
            createDependencyProperty isExpandedPropertyId NavigationViewItem.IsExpandedProperty value
            |> node.set

        [<Extension>]
        static member menuItemsSource<'source, 'key>
            (node: VirtualNode<NavigationViewItem>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                menuItemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> NavigationViewItem).MenuItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> NavigationViewItem).MenuItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member menuItems'(node: VirtualNode<NavigationViewItem>, value: IVirtualNode list) =
            createInnerChildrenProperty
                menuItemsPropertyId
                value
                (fun element children -> children |> List.iter (element :?> NavigationViewItem).MenuItems.Add)
                (fun element ->
                    (element :?> NavigationViewItem).MenuItems
                    |> Seq.cast<UIElement>
                    |> ResizeArray
                    :> IList<_>)
            |> node.set

        [<Extension>]
        static member selectsOnInvoked(node: VirtualNode<NavigationViewItem>, value: bool) =
            createDependencyProperty selectsOnInvokedPropertyId NavigationViewItem.SelectsOnInvokedProperty value
            |> node.set

    let navigationViewItem () =
        createNode<NavigationViewItem> navigationViewItemNodeId
