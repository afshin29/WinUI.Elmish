namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property FirstCacheIndex: read-only
// skipped property FirstVisibleIndex: read-only
// skipped property LastCacheIndex: read-only
// skipped property LastVisibleIndex: read-only
// skipped property ScrollingDirection: read-only


[<AutoOpen>]
module ItemsStackPanelMod =

    let private itemsStackPanelNodeId = getNextId ()

    registerDefaultRenderer<ItemsStackPanel> itemsStackPanelNodeId

    let private areStickyGroupHeadersEnabledPropertyId = getNextId ()
    let private cacheLengthPropertyId = getNextId ()
    let private groupHeaderPlacementPropertyId = getNextId ()
    let private groupPaddingPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private itemsUpdatingScrollModePropertyId = getNextId ()

    [<Extension>]
    type ItemsStackPanelExtensions =

        [<Extension>]
        static member areStickyGroupHeadersEnabled(node: VirtualNode<ItemsStackPanel>, value: bool) =
            createDependencyProperty areStickyGroupHeadersEnabledPropertyId ItemsStackPanel.AreStickyGroupHeadersEnabledProperty value
            |> node.set

        [<Extension>]
        static member cacheLength(node: VirtualNode<ItemsStackPanel>, value: float) =
            createDependencyProperty cacheLengthPropertyId ItemsStackPanel.CacheLengthProperty value
            |> node.set

        [<Extension>]
        static member groupHeaderPlacement(node: VirtualNode<ItemsStackPanel>, value: GroupHeaderPlacement) =
            createDependencyProperty groupHeaderPlacementPropertyId ItemsStackPanel.GroupHeaderPlacementProperty value
            |> node.set

        [<Extension>]
        static member groupPadding(node: VirtualNode<ItemsStackPanel>, value: Thickness) =
            createDependencyProperty groupPaddingPropertyId ItemsStackPanel.GroupPaddingProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<ItemsStackPanel>, value: Orientation) =
            createDependencyProperty orientationPropertyId ItemsStackPanel.OrientationProperty value
            |> node.set

        [<Extension>]
        static member itemsUpdatingScrollMode(node: VirtualNode<ItemsStackPanel>, value: ItemsUpdatingScrollMode) =
            createClrProperty
                itemsUpdatingScrollModePropertyId
                (fun uiElement _ -> (uiElement :?> ItemsStackPanel).ItemsUpdatingScrollMode <- value)
                (fun uiElement -> (uiElement :?> ItemsStackPanel).ItemsUpdatingScrollMode <- Unchecked.defaultof<ItemsUpdatingScrollMode>)
                value
            |> node.set

    let itemsStackPanel () =
        createNode<ItemsStackPanel> itemsStackPanelNodeId
