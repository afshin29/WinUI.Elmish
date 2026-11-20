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
module WrapGridMod =

    let private wrapGridNodeId = getNextId ()

    registerDefaultRenderer<WrapGrid> wrapGridNodeId

    let private horizontalChildrenAlignmentPropertyId = getNextId ()
    let private itemHeightPropertyId = getNextId ()
    let private itemWidthPropertyId = getNextId ()
    let private maximumRowsOrColumnsPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private verticalChildrenAlignmentPropertyId = getNextId ()

    [<Extension>]
    type WrapGridExtensions =

        [<Extension>]
        static member horizontalChildrenAlignment(node: VirtualNode<WrapGrid>, value: HorizontalAlignment) =
            createDependencyProperty horizontalChildrenAlignmentPropertyId WrapGrid.HorizontalChildrenAlignmentProperty value
            |> node.set

        [<Extension>]
        static member itemHeight(node: VirtualNode<WrapGrid>, value: float) =
            createDependencyProperty itemHeightPropertyId WrapGrid.ItemHeightProperty value
            |> node.set

        [<Extension>]
        static member itemWidth(node: VirtualNode<WrapGrid>, value: float) =
            createDependencyProperty itemWidthPropertyId WrapGrid.ItemWidthProperty value
            |> node.set

        [<Extension>]
        static member maximumRowsOrColumns(node: VirtualNode<WrapGrid>, value: int) =
            createDependencyProperty maximumRowsOrColumnsPropertyId WrapGrid.MaximumRowsOrColumnsProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<WrapGrid>, value: Orientation) =
            createDependencyProperty orientationPropertyId WrapGrid.OrientationProperty value
            |> node.set

        [<Extension>]
        static member verticalChildrenAlignment(node: VirtualNode<WrapGrid>, value: VerticalAlignment) =
            createDependencyProperty verticalChildrenAlignmentPropertyId WrapGrid.VerticalChildrenAlignmentProperty value
            |> node.set

    let wrapGrid () =
        createNode<WrapGrid> wrapGridNodeId
