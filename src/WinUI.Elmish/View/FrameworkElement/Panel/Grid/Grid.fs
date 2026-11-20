namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property ColumnDefinitions: read-only
// skipped property RowDefinitions: read-only


[<AutoOpen>]
module GridMod =

    let private gridNodeId = getNextId ()

    registerDefaultRenderer<Grid> gridNodeId

    let private backgroundSizingPropertyId = getNextId ()
    let private borderBrushPropertyId = getNextId ()
    let private borderThicknessPropertyId = getNextId ()
    let private columnPropertyId = getNextId ()
    let private columnSpacingPropertyId = getNextId ()
    let private columnSpanPropertyId = getNextId ()
    let private cornerRadiusPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private rowPropertyId = getNextId ()
    let private rowSpacingPropertyId = getNextId ()
    let private rowSpanPropertyId = getNextId ()

    [<Extension>]
    type GridExtensions =

        [<Extension>]
        static member backgroundSizing<'m when 'm :> Grid and 'm: not struct>
            (node: VirtualNode<'m>, value: BackgroundSizing) =
            createDependencyProperty backgroundSizingPropertyId Grid.BackgroundSizingProperty value
            |> node.set

        [<Extension>]
        static member borderBrush<'m when 'm :> Grid and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty borderBrushPropertyId Grid.BorderBrushProperty value
            |> node.set

        [<Extension>]
        static member borderThickness<'m when 'm :> Grid and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty borderThicknessPropertyId Grid.BorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member grid_column<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty columnPropertyId Grid.ColumnProperty value
            |> node.set

        [<Extension>]
        static member columnSpacing<'m when 'm :> Grid and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty columnSpacingPropertyId Grid.ColumnSpacingProperty value
            |> node.set

        [<Extension>]
        static member grid_columnSpan<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty columnSpanPropertyId Grid.ColumnSpanProperty value
            |> node.set

        [<Extension>]
        static member cornerRadius<'m when 'm :> Grid and 'm: not struct>
            (node: VirtualNode<'m>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId Grid.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member padding<'m when 'm :> Grid and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty paddingPropertyId Grid.PaddingProperty value
            |> node.set

        [<Extension>]
        static member grid_row<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty rowPropertyId Grid.RowProperty value
            |> node.set

        [<Extension>]
        static member grid<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, row: int, column: int) =
            node
                .grid_row(row)
                .grid_column(column)

        [<Extension>]
        static member rowSpacing<'m when 'm :> Grid and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty rowSpacingPropertyId Grid.RowSpacingProperty value
            |> node.set

        [<Extension>]
        static member grid_rowSpan<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty rowSpanPropertyId Grid.RowSpanProperty value
            |> node.set

    let grid (rows: GridLength list, columns: GridLength list) =
        (createNode<Grid> gridNodeId)
            .config(fun grid ->
                rows
                |> List.map (fun row -> RowDefinition(Height = row))
                |> List.iter grid.RowDefinitions.Add)
            .config(fun grid ->
                columns
                |> List.map (fun col -> ColumnDefinition(Width = col))
                |> List.iter grid.ColumnDefinitions.Add)
