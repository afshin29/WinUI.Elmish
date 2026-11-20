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
module VariableSizedWrapGridMod =

    let private variableSizedWrapGridNodeId = getNextId ()

    registerDefaultRenderer<VariableSizedWrapGrid> variableSizedWrapGridNodeId

    let private columnSpanPropertyId = getNextId ()
    let private horizontalChildrenAlignmentPropertyId = getNextId ()
    let private itemHeightPropertyId = getNextId ()
    let private itemWidthPropertyId = getNextId ()
    let private maximumRowsOrColumnsPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private rowSpanPropertyId = getNextId ()
    let private verticalChildrenAlignmentPropertyId = getNextId ()

    [<Extension>]
    type VariableSizedWrapGridExtensions =

        [<Extension>]
        static member variableSizedWrapGrid_columnSpan<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty columnSpanPropertyId VariableSizedWrapGrid.ColumnSpanProperty value
            |> node.set

        [<Extension>]
        static member horizontalChildrenAlignment(node: VirtualNode<VariableSizedWrapGrid>, value: HorizontalAlignment) =
            createDependencyProperty horizontalChildrenAlignmentPropertyId VariableSizedWrapGrid.HorizontalChildrenAlignmentProperty value
            |> node.set

        [<Extension>]
        static member itemHeight(node: VirtualNode<VariableSizedWrapGrid>, value: float) =
            createDependencyProperty itemHeightPropertyId VariableSizedWrapGrid.ItemHeightProperty value
            |> node.set

        [<Extension>]
        static member itemWidth(node: VirtualNode<VariableSizedWrapGrid>, value: float) =
            createDependencyProperty itemWidthPropertyId VariableSizedWrapGrid.ItemWidthProperty value
            |> node.set

        [<Extension>]
        static member maximumRowsOrColumns(node: VirtualNode<VariableSizedWrapGrid>, value: int) =
            createDependencyProperty maximumRowsOrColumnsPropertyId VariableSizedWrapGrid.MaximumRowsOrColumnsProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<VariableSizedWrapGrid>, value: Orientation) =
            createDependencyProperty orientationPropertyId VariableSizedWrapGrid.OrientationProperty value
            |> node.set

        [<Extension>]
        static member variableSizedWrapGrid_rowSpan<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty rowSpanPropertyId VariableSizedWrapGrid.RowSpanProperty value
            |> node.set

        [<Extension>]
        static member verticalChildrenAlignment(node: VirtualNode<VariableSizedWrapGrid>, value: VerticalAlignment) =
            createDependencyProperty verticalChildrenAlignmentPropertyId VariableSizedWrapGrid.VerticalChildrenAlignmentProperty value
            |> node.set

    let variableSizedWrapGrid () =
        createNode<VariableSizedWrapGrid> variableSizedWrapGridNodeId
