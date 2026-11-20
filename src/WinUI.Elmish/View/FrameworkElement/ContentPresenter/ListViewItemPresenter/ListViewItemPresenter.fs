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
module ListViewItemPresenterMod =

    let private listViewItemPresenterNodeId = getNextId ()

    registerDefaultRenderer<ListViewItemPresenter> listViewItemPresenterNodeId

    let private checkBoxBorderBrushPropertyId = getNextId ()
    let private checkBoxBrushPropertyId = getNextId ()
    let private checkBoxCornerRadiusPropertyId = getNextId ()
    let private checkBoxDisabledBorderBrushPropertyId = getNextId ()
    let private checkBoxDisabledBrushPropertyId = getNextId ()
    let private checkBoxPointerOverBorderBrushPropertyId = getNextId ()
    let private checkBoxPointerOverBrushPropertyId = getNextId ()
    let private checkBoxPressedBorderBrushPropertyId = getNextId ()
    let private checkBoxPressedBrushPropertyId = getNextId ()
    let private checkBoxSelectedBrushPropertyId = getNextId ()
    let private checkBoxSelectedDisabledBrushPropertyId = getNextId ()
    let private checkBoxSelectedPointerOverBrushPropertyId = getNextId ()
    let private checkBoxSelectedPressedBrushPropertyId = getNextId ()
    let private checkBrushPropertyId = getNextId ()
    let private checkDisabledBrushPropertyId = getNextId ()
    let private checkHintBrushPropertyId = getNextId ()
    let private checkModePropertyId = getNextId ()
    let private checkPressedBrushPropertyId = getNextId ()
    let private checkSelectingBrushPropertyId = getNextId ()
    let private contentMarginPropertyId = getNextId ()
    let private disabledOpacityPropertyId = getNextId ()
    let private dragBackgroundPropertyId = getNextId ()
    let private dragForegroundPropertyId = getNextId ()
    let private dragOpacityPropertyId = getNextId ()
    let private focusBorderBrushPropertyId = getNextId ()
    let private focusSecondaryBorderBrushPropertyId = getNextId ()
    let private listViewItemPresenterHorizontalContentAlignmentPropertyId = getNextId ()
    let private listViewItemPresenterPaddingPropertyId = getNextId ()
    let private listViewItemPresenterVerticalContentAlignmentPropertyId = getNextId ()
    let private placeholderBackgroundPropertyId = getNextId ()
    let private pointerOverBackgroundMarginPropertyId = getNextId ()
    let private pointerOverBackgroundPropertyId = getNextId ()
    let private pointerOverBorderBrushPropertyId = getNextId ()
    let private pointerOverForegroundPropertyId = getNextId ()
    let private pressedBackgroundPropertyId = getNextId ()
    let private reorderHintOffsetPropertyId = getNextId ()
    let private revealBackgroundPropertyId = getNextId ()
    let private revealBackgroundShowsAboveContentPropertyId = getNextId ()
    let private revealBorderBrushPropertyId = getNextId ()
    let private revealBorderThicknessPropertyId = getNextId ()
    let private selectedBackgroundPropertyId = getNextId ()
    let private selectedBorderBrushPropertyId = getNextId ()
    let private selectedBorderThicknessPropertyId = getNextId ()
    let private selectedDisabledBackgroundPropertyId = getNextId ()
    let private selectedDisabledBorderBrushPropertyId = getNextId ()
    let private selectedForegroundPropertyId = getNextId ()
    let private selectedInnerBorderBrushPropertyId = getNextId ()
    let private selectedPointerOverBackgroundPropertyId = getNextId ()
    let private selectedPointerOverBorderBrushPropertyId = getNextId ()
    let private selectedPressedBackgroundPropertyId = getNextId ()
    let private selectedPressedBorderBrushPropertyId = getNextId ()
    let private selectionCheckMarkVisualEnabledPropertyId = getNextId ()
    let private selectionIndicatorBrushPropertyId = getNextId ()
    let private selectionIndicatorCornerRadiusPropertyId = getNextId ()
    let private selectionIndicatorDisabledBrushPropertyId = getNextId ()
    let private selectionIndicatorModePropertyId = getNextId ()
    let private selectionIndicatorPointerOverBrushPropertyId = getNextId ()
    let private selectionIndicatorPressedBrushPropertyId = getNextId ()
    let private selectionIndicatorVisualEnabledPropertyId = getNextId ()

    [<Extension>]
    type ListViewItemPresenterExtensions =

        [<Extension>]
        static member checkBoxBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxBorderBrushPropertyId ListViewItemPresenter.CheckBoxBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxBrushPropertyId ListViewItemPresenter.CheckBoxBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxCornerRadius<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: CornerRadius) =
            createDependencyProperty checkBoxCornerRadiusPropertyId ListViewItemPresenter.CheckBoxCornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member checkBoxDisabledBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxDisabledBorderBrushPropertyId ListViewItemPresenter.CheckBoxDisabledBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxDisabledBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxDisabledBrushPropertyId ListViewItemPresenter.CheckBoxDisabledBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxPointerOverBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxPointerOverBorderBrushPropertyId ListViewItemPresenter.CheckBoxPointerOverBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxPointerOverBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxPointerOverBrushPropertyId ListViewItemPresenter.CheckBoxPointerOverBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxPressedBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxPressedBorderBrushPropertyId ListViewItemPresenter.CheckBoxPressedBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxPressedBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxPressedBrushPropertyId ListViewItemPresenter.CheckBoxPressedBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxSelectedBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxSelectedBrushPropertyId ListViewItemPresenter.CheckBoxSelectedBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxSelectedDisabledBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxSelectedDisabledBrushPropertyId ListViewItemPresenter.CheckBoxSelectedDisabledBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxSelectedPointerOverBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxSelectedPointerOverBrushPropertyId ListViewItemPresenter.CheckBoxSelectedPointerOverBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBoxSelectedPressedBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBoxSelectedPressedBrushPropertyId ListViewItemPresenter.CheckBoxSelectedPressedBrushProperty value
            |> node.set

        [<Extension>]
        static member checkBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkBrushPropertyId ListViewItemPresenter.CheckBrushProperty value
            |> node.set

        [<Extension>]
        static member checkDisabledBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkDisabledBrushPropertyId ListViewItemPresenter.CheckDisabledBrushProperty value
            |> node.set

        [<Extension>]
        static member checkHintBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkHintBrushPropertyId ListViewItemPresenter.CheckHintBrushProperty value
            |> node.set

        [<Extension>]
        static member checkMode<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: ListViewItemPresenterCheckMode) =
            createDependencyProperty checkModePropertyId ListViewItemPresenter.CheckModeProperty value
            |> node.set

        [<Extension>]
        static member checkPressedBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkPressedBrushPropertyId ListViewItemPresenter.CheckPressedBrushProperty value
            |> node.set

        [<Extension>]
        static member checkSelectingBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty checkSelectingBrushPropertyId ListViewItemPresenter.CheckSelectingBrushProperty value
            |> node.set

        [<Extension>]
        static member contentMargin<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty contentMarginPropertyId ListViewItemPresenter.ContentMarginProperty value
            |> node.set

        [<Extension>]
        static member disabledOpacity<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty disabledOpacityPropertyId ListViewItemPresenter.DisabledOpacityProperty value
            |> node.set

        [<Extension>]
        static member dragBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty dragBackgroundPropertyId ListViewItemPresenter.DragBackgroundProperty value
            |> node.set

        [<Extension>]
        static member dragForeground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty dragForegroundPropertyId ListViewItemPresenter.DragForegroundProperty value
            |> node.set

        [<Extension>]
        static member dragOpacity<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty dragOpacityPropertyId ListViewItemPresenter.DragOpacityProperty value
            |> node.set

        [<Extension>]
        static member focusBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty focusBorderBrushPropertyId ListViewItemPresenter.FocusBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member focusSecondaryBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty focusSecondaryBorderBrushPropertyId ListViewItemPresenter.FocusSecondaryBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member listViewItemPresenterHorizontalContentAlignment<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: HorizontalAlignment) =
            createDependencyProperty listViewItemPresenterHorizontalContentAlignmentPropertyId ListViewItemPresenter.ListViewItemPresenterHorizontalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member listViewItemPresenterPadding<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty listViewItemPresenterPaddingPropertyId ListViewItemPresenter.ListViewItemPresenterPaddingProperty value
            |> node.set

        [<Extension>]
        static member listViewItemPresenterVerticalContentAlignment<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: VerticalAlignment) =
            createDependencyProperty listViewItemPresenterVerticalContentAlignmentPropertyId ListViewItemPresenter.ListViewItemPresenterVerticalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member placeholderBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty placeholderBackgroundPropertyId ListViewItemPresenter.PlaceholderBackgroundProperty value
            |> node.set

        [<Extension>]
        static member pointerOverBackgroundMargin<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty pointerOverBackgroundMarginPropertyId ListViewItemPresenter.PointerOverBackgroundMarginProperty value
            |> node.set

        [<Extension>]
        static member pointerOverBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty pointerOverBackgroundPropertyId ListViewItemPresenter.PointerOverBackgroundProperty value
            |> node.set

        [<Extension>]
        static member pointerOverBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty pointerOverBorderBrushPropertyId ListViewItemPresenter.PointerOverBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member pointerOverForeground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty pointerOverForegroundPropertyId ListViewItemPresenter.PointerOverForegroundProperty value
            |> node.set

        [<Extension>]
        static member pressedBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty pressedBackgroundPropertyId ListViewItemPresenter.PressedBackgroundProperty value
            |> node.set

        [<Extension>]
        static member reorderHintOffset<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty reorderHintOffsetPropertyId ListViewItemPresenter.ReorderHintOffsetProperty value
            |> node.set

        [<Extension>]
        static member revealBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty revealBackgroundPropertyId ListViewItemPresenter.RevealBackgroundProperty value
            |> node.set

        [<Extension>]
        static member revealBackgroundShowsAboveContent<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty revealBackgroundShowsAboveContentPropertyId ListViewItemPresenter.RevealBackgroundShowsAboveContentProperty value
            |> node.set

        [<Extension>]
        static member revealBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty revealBorderBrushPropertyId ListViewItemPresenter.RevealBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member revealBorderThickness<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty revealBorderThicknessPropertyId ListViewItemPresenter.RevealBorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member selectedBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedBackgroundPropertyId ListViewItemPresenter.SelectedBackgroundProperty value
            |> node.set

        [<Extension>]
        static member selectedBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedBorderBrushPropertyId ListViewItemPresenter.SelectedBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedBorderThickness<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty selectedBorderThicknessPropertyId ListViewItemPresenter.SelectedBorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member selectedDisabledBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedDisabledBackgroundPropertyId ListViewItemPresenter.SelectedDisabledBackgroundProperty value
            |> node.set

        [<Extension>]
        static member selectedDisabledBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedDisabledBorderBrushPropertyId ListViewItemPresenter.SelectedDisabledBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedForeground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedForegroundPropertyId ListViewItemPresenter.SelectedForegroundProperty value
            |> node.set

        [<Extension>]
        static member selectedInnerBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedInnerBorderBrushPropertyId ListViewItemPresenter.SelectedInnerBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedPointerOverBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedPointerOverBackgroundPropertyId ListViewItemPresenter.SelectedPointerOverBackgroundProperty value
            |> node.set

        [<Extension>]
        static member selectedPointerOverBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedPointerOverBorderBrushPropertyId ListViewItemPresenter.SelectedPointerOverBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedPressedBackground<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedPressedBackgroundPropertyId ListViewItemPresenter.SelectedPressedBackgroundProperty value
            |> node.set

        [<Extension>]
        static member selectedPressedBorderBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectedPressedBorderBrushPropertyId ListViewItemPresenter.SelectedPressedBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectionCheckMarkVisualEnabled<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty selectionCheckMarkVisualEnabledPropertyId ListViewItemPresenter.SelectionCheckMarkVisualEnabledProperty value
            |> node.set

        [<Extension>]
        static member selectionIndicatorBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectionIndicatorBrushPropertyId ListViewItemPresenter.SelectionIndicatorBrushProperty value
            |> node.set

        [<Extension>]
        static member selectionIndicatorCornerRadius<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: CornerRadius) =
            createDependencyProperty selectionIndicatorCornerRadiusPropertyId ListViewItemPresenter.SelectionIndicatorCornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member selectionIndicatorDisabledBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectionIndicatorDisabledBrushPropertyId ListViewItemPresenter.SelectionIndicatorDisabledBrushProperty value
            |> node.set

        [<Extension>]
        static member selectionIndicatorMode<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: ListViewItemPresenterSelectionIndicatorMode) =
            createDependencyProperty selectionIndicatorModePropertyId ListViewItemPresenter.SelectionIndicatorModeProperty value
            |> node.set

        [<Extension>]
        static member selectionIndicatorPointerOverBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectionIndicatorPointerOverBrushPropertyId ListViewItemPresenter.SelectionIndicatorPointerOverBrushProperty value
            |> node.set

        [<Extension>]
        static member selectionIndicatorPressedBrush<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty selectionIndicatorPressedBrushPropertyId ListViewItemPresenter.SelectionIndicatorPressedBrushProperty value
            |> node.set

        [<Extension>]
        static member selectionIndicatorVisualEnabled<'m when 'm :> ListViewItemPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty selectionIndicatorVisualEnabledPropertyId ListViewItemPresenter.SelectionIndicatorVisualEnabledProperty value
            |> node.set

    let listViewItemPresenter () =
        createNode<ListViewItemPresenter> listViewItemPresenterNodeId
