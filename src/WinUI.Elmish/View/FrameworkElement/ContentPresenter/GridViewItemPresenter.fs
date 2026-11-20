namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module GridViewItemPresenterMod =

    let private gridViewItemPresenterNodeId = getNextId ()

    registerDefaultRenderer<GridViewItemPresenter> gridViewItemPresenterNodeId

    let private checkBrushPropertyId = getNextId ()
    let private checkHintBrushPropertyId = getNextId ()
    let private checkSelectingBrushPropertyId = getNextId ()
    let private contentMarginPropertyId = getNextId ()
    let private disabledOpacityPropertyId = getNextId ()
    let private dragBackgroundPropertyId = getNextId ()
    let private dragForegroundPropertyId = getNextId ()
    let private dragOpacityPropertyId = getNextId ()
    let private focusBorderBrushPropertyId = getNextId ()
    let private gridViewItemPresenterHorizontalContentAlignmentPropertyId = getNextId ()
    let private gridViewItemPresenterPaddingPropertyId = getNextId ()
    let private gridViewItemPresenterVerticalContentAlignmentPropertyId = getNextId ()
    let private placeholderBackgroundPropertyId = getNextId ()
    let private pointerOverBackgroundMarginPropertyId = getNextId ()
    let private pointerOverBackgroundPropertyId = getNextId ()
    let private reorderHintOffsetPropertyId = getNextId ()
    let private selectedBackgroundPropertyId = getNextId ()
    let private selectedBorderThicknessPropertyId = getNextId ()
    let private selectedForegroundPropertyId = getNextId ()
    let private selectedPointerOverBackgroundPropertyId = getNextId ()
    let private selectedPointerOverBorderBrushPropertyId = getNextId ()
    let private selectionCheckMarkVisualEnabledPropertyId = getNextId ()

    [<Extension>]
    type GridViewItemPresenterExtensions =

        [<Extension>]
        static member checkBrush(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty checkBrushPropertyId GridViewItemPresenter.CheckBrushProperty value
            |> node.set

        [<Extension>]
        static member checkHintBrush(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty checkHintBrushPropertyId GridViewItemPresenter.CheckHintBrushProperty value
            |> node.set

        [<Extension>]
        static member checkSelectingBrush(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty checkSelectingBrushPropertyId GridViewItemPresenter.CheckSelectingBrushProperty value
            |> node.set

        [<Extension>]
        static member contentMargin(node: VirtualNode<GridViewItemPresenter>, value: Thickness) =
            createDependencyProperty contentMarginPropertyId GridViewItemPresenter.ContentMarginProperty value
            |> node.set

        [<Extension>]
        static member disabledOpacity(node: VirtualNode<GridViewItemPresenter>, value: float) =
            createDependencyProperty disabledOpacityPropertyId GridViewItemPresenter.DisabledOpacityProperty value
            |> node.set

        [<Extension>]
        static member dragBackground(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty dragBackgroundPropertyId GridViewItemPresenter.DragBackgroundProperty value
            |> node.set

        [<Extension>]
        static member dragForeground(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty dragForegroundPropertyId GridViewItemPresenter.DragForegroundProperty value
            |> node.set

        [<Extension>]
        static member dragOpacity(node: VirtualNode<GridViewItemPresenter>, value: float) =
            createDependencyProperty dragOpacityPropertyId GridViewItemPresenter.DragOpacityProperty value
            |> node.set

        [<Extension>]
        static member focusBorderBrush(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty focusBorderBrushPropertyId GridViewItemPresenter.FocusBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member gridViewItemPresenterHorizontalContentAlignment(node: VirtualNode<GridViewItemPresenter>, value: HorizontalAlignment) =
            createDependencyProperty gridViewItemPresenterHorizontalContentAlignmentPropertyId GridViewItemPresenter.GridViewItemPresenterHorizontalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member gridViewItemPresenterPadding(node: VirtualNode<GridViewItemPresenter>, value: Thickness) =
            createDependencyProperty gridViewItemPresenterPaddingPropertyId GridViewItemPresenter.GridViewItemPresenterPaddingProperty value
            |> node.set

        [<Extension>]
        static member gridViewItemPresenterVerticalContentAlignment(node: VirtualNode<GridViewItemPresenter>, value: VerticalAlignment) =
            createDependencyProperty gridViewItemPresenterVerticalContentAlignmentPropertyId GridViewItemPresenter.GridViewItemPresenterVerticalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member placeholderBackground(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty placeholderBackgroundPropertyId GridViewItemPresenter.PlaceholderBackgroundProperty value
            |> node.set

        [<Extension>]
        static member pointerOverBackgroundMargin(node: VirtualNode<GridViewItemPresenter>, value: Thickness) =
            createDependencyProperty pointerOverBackgroundMarginPropertyId GridViewItemPresenter.PointerOverBackgroundMarginProperty value
            |> node.set

        [<Extension>]
        static member pointerOverBackground(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty pointerOverBackgroundPropertyId GridViewItemPresenter.PointerOverBackgroundProperty value
            |> node.set

        [<Extension>]
        static member reorderHintOffset(node: VirtualNode<GridViewItemPresenter>, value: float) =
            createDependencyProperty reorderHintOffsetPropertyId GridViewItemPresenter.ReorderHintOffsetProperty value
            |> node.set

        [<Extension>]
        static member selectedBackground(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty selectedBackgroundPropertyId GridViewItemPresenter.SelectedBackgroundProperty value
            |> node.set

        [<Extension>]
        static member selectedBorderThickness(node: VirtualNode<GridViewItemPresenter>, value: Thickness) =
            createDependencyProperty selectedBorderThicknessPropertyId GridViewItemPresenter.SelectedBorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member selectedForeground(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty selectedForegroundPropertyId GridViewItemPresenter.SelectedForegroundProperty value
            |> node.set

        [<Extension>]
        static member selectedPointerOverBackground(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty selectedPointerOverBackgroundPropertyId GridViewItemPresenter.SelectedPointerOverBackgroundProperty value
            |> node.set

        [<Extension>]
        static member selectedPointerOverBorderBrush(node: VirtualNode<GridViewItemPresenter>, value: Brush) =
            createDependencyProperty selectedPointerOverBorderBrushPropertyId GridViewItemPresenter.SelectedPointerOverBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectionCheckMarkVisualEnabled(node: VirtualNode<GridViewItemPresenter>, value: bool) =
            createDependencyProperty selectionCheckMarkVisualEnabledPropertyId GridViewItemPresenter.SelectionCheckMarkVisualEnabledProperty value
            |> node.set

    let gridViewItemPresenter () =
        createNode<GridViewItemPresenter> gridViewItemPresenterNodeId
