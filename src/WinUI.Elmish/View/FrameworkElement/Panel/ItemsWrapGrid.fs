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
module ItemsWrapGridMod =

    let private itemsWrapGridNodeId = getNextId ()

    registerDefaultRenderer<ItemsWrapGrid> itemsWrapGridNodeId

    let private areStickyGroupHeadersEnabledPropertyId = getNextId ()
    let private cacheLengthPropertyId = getNextId ()
    let private groupHeaderPlacementPropertyId = getNextId ()
    let private groupPaddingPropertyId = getNextId ()
    let private itemHeightPropertyId = getNextId ()
    let private itemWidthPropertyId = getNextId ()
    let private maximumRowsOrColumnsPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()

    [<Extension>]
    type ItemsWrapGridExtensions =

        [<Extension>]
        static member areStickyGroupHeadersEnabled(node: VirtualNode<ItemsWrapGrid>, value: bool) =
            createDependencyProperty areStickyGroupHeadersEnabledPropertyId ItemsWrapGrid.AreStickyGroupHeadersEnabledProperty value
            |> node.set

        [<Extension>]
        static member cacheLength(node: VirtualNode<ItemsWrapGrid>, value: float) =
            createDependencyProperty cacheLengthPropertyId ItemsWrapGrid.CacheLengthProperty value
            |> node.set

        [<Extension>]
        static member groupHeaderPlacement(node: VirtualNode<ItemsWrapGrid>, value: GroupHeaderPlacement) =
            createDependencyProperty groupHeaderPlacementPropertyId ItemsWrapGrid.GroupHeaderPlacementProperty value
            |> node.set

        [<Extension>]
        static member groupPadding(node: VirtualNode<ItemsWrapGrid>, value: Thickness) =
            createDependencyProperty groupPaddingPropertyId ItemsWrapGrid.GroupPaddingProperty value
            |> node.set

        [<Extension>]
        static member itemHeight(node: VirtualNode<ItemsWrapGrid>, value: float) =
            createDependencyProperty itemHeightPropertyId ItemsWrapGrid.ItemHeightProperty value
            |> node.set

        [<Extension>]
        static member itemWidth(node: VirtualNode<ItemsWrapGrid>, value: float) =
            createDependencyProperty itemWidthPropertyId ItemsWrapGrid.ItemWidthProperty value
            |> node.set

        [<Extension>]
        static member maximumRowsOrColumns(node: VirtualNode<ItemsWrapGrid>, value: int) =
            createDependencyProperty maximumRowsOrColumnsPropertyId ItemsWrapGrid.MaximumRowsOrColumnsProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<ItemsWrapGrid>, value: Orientation) =
            createDependencyProperty orientationPropertyId ItemsWrapGrid.OrientationProperty value
            |> node.set

    let itemsWrapGrid () =
        createNode<ItemsWrapGrid> itemsWrapGridNodeId
