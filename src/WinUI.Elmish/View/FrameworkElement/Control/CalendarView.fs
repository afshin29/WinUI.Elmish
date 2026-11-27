namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation
open Windows.Globalization
open Windows.UI.Text

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property SelectedDatesProperty: read-only
// skipped dependency property TemplateSettingsProperty: read-only


[<AutoOpen>]
module CalendarViewMod =

    let private calendarViewNodeId = getNextId ()

    registerDefaultRenderer<CalendarView> calendarViewNodeId

    let private blackoutBackgroundPropertyId = getNextId ()
    let private blackoutForegroundPropertyId = getNextId ()
    let private blackoutStrikethroughBrushPropertyId = getNextId ()
    let private calendarIdentifierPropertyId = getNextId ()
    let private calendarItemBackgroundPropertyId = getNextId ()
    let private calendarItemBorderBrushPropertyId = getNextId ()
    let private calendarItemBorderThicknessPropertyId = getNextId ()
    let private calendarItemCornerRadiusPropertyId = getNextId ()
    let private calendarItemDisabledBackgroundPropertyId = getNextId ()
    let private calendarItemForegroundPropertyId = getNextId ()
    let private calendarItemHoverBackgroundPropertyId = getNextId ()
    let private calendarItemPressedBackgroundPropertyId = getNextId ()
    let private calendarViewDayItemStylePropertyId = getNextId ()
    let private dayItemFontFamilyPropertyId = getNextId ()
    let private dayItemFontSizePropertyId = getNextId ()
    let private dayItemFontStylePropertyId = getNextId ()
    let private dayItemFontWeightPropertyId = getNextId ()
    let private dayItemMarginPropertyId = getNextId ()
    let private dayOfWeekFormatPropertyId = getNextId ()
    let private disabledForegroundPropertyId = getNextId ()
    let private displayModePropertyId = getNextId ()
    let private firstDayOfWeekPropertyId = getNextId ()
    let private firstOfMonthLabelFontFamilyPropertyId = getNextId ()
    let private firstOfMonthLabelFontSizePropertyId = getNextId ()
    let private firstOfMonthLabelFontStylePropertyId = getNextId ()
    let private firstOfMonthLabelFontWeightPropertyId = getNextId ()
    let private firstOfMonthLabelMarginPropertyId = getNextId ()
    let private firstOfYearDecadeLabelFontFamilyPropertyId = getNextId ()
    let private firstOfYearDecadeLabelFontSizePropertyId = getNextId ()
    let private firstOfYearDecadeLabelFontStylePropertyId = getNextId ()
    let private firstOfYearDecadeLabelFontWeightPropertyId = getNextId ()
    let private firstOfYearDecadeLabelMarginPropertyId = getNextId ()
    let private focusBorderBrushPropertyId = getNextId ()
    let private horizontalDayItemAlignmentPropertyId = getNextId ()
    let private horizontalFirstOfMonthLabelAlignmentPropertyId = getNextId ()
    let private hoverBorderBrushPropertyId = getNextId ()
    let private isGroupLabelVisiblePropertyId = getNextId ()
    let private isOutOfScopeEnabledPropertyId = getNextId ()
    let private isTodayHighlightedPropertyId = getNextId ()
    let private maxDatePropertyId = getNextId ()
    let private minDatePropertyId = getNextId ()
    let private monthYearItemFontFamilyPropertyId = getNextId ()
    let private monthYearItemFontSizePropertyId = getNextId ()
    let private monthYearItemFontStylePropertyId = getNextId ()
    let private monthYearItemFontWeightPropertyId = getNextId ()
    let private monthYearItemMarginPropertyId = getNextId ()
    let private numberOfWeeksInViewPropertyId = getNextId ()
    let private outOfScopeBackgroundPropertyId = getNextId ()
    let private outOfScopeForegroundPropertyId = getNextId ()
    let private outOfScopeHoverForegroundPropertyId = getNextId ()
    let private outOfScopePressedForegroundPropertyId = getNextId ()
    let private pressedBorderBrushPropertyId = getNextId ()
    let private pressedForegroundPropertyId = getNextId ()
    let private selectedBorderBrushPropertyId = getNextId ()
    let private selectedDisabledBorderBrushPropertyId = getNextId ()
    let private selectedDisabledForegroundPropertyId = getNextId ()
    let private selectedForegroundPropertyId = getNextId ()
    let private selectedHoverBorderBrushPropertyId = getNextId ()
    let private selectedHoverForegroundPropertyId = getNextId ()
    let private selectedPressedBorderBrushPropertyId = getNextId ()
    let private selectedPressedForegroundPropertyId = getNextId ()
    let private selectionModePropertyId = getNextId ()
    let private todayBackgroundPropertyId = getNextId ()
    let private todayBlackoutBackgroundPropertyId = getNextId ()
    let private todayBlackoutForegroundPropertyId = getNextId ()
    let private todayDisabledBackgroundPropertyId = getNextId ()
    let private todayFontWeightPropertyId = getNextId ()
    let private todayForegroundPropertyId = getNextId ()
    let private todayHoverBackgroundPropertyId = getNextId ()
    let private todayPressedBackgroundPropertyId = getNextId ()
    let private todaySelectedInnerBorderBrushPropertyId = getNextId ()
    let private verticalDayItemAlignmentPropertyId = getNextId ()
    let private verticalFirstOfMonthLabelAlignmentPropertyId = getNextId ()

    [<Extension>]
    type CalendarViewExtensions =

        [<Extension>]
        static member blackoutBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty blackoutBackgroundPropertyId CalendarView.BlackoutBackgroundProperty value
            |> node.set

        [<Extension>]
        static member blackoutForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty blackoutForegroundPropertyId CalendarView.BlackoutForegroundProperty value
            |> node.set

        [<Extension>]
        static member blackoutStrikethroughBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty blackoutStrikethroughBrushPropertyId CalendarView.BlackoutStrikethroughBrushProperty value
            |> node.set

        [<Extension>]
        static member calendarIdentifier(node: VirtualNode<CalendarView>, value: string) =
            createDependencyProperty calendarIdentifierPropertyId CalendarView.CalendarIdentifierProperty value
            |> node.set

        [<Extension>]
        static member calendarItemBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty calendarItemBackgroundPropertyId CalendarView.CalendarItemBackgroundProperty value
            |> node.set

        [<Extension>]
        static member calendarItemBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty calendarItemBorderBrushPropertyId CalendarView.CalendarItemBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member calendarItemBorderThickness(node: VirtualNode<CalendarView>, value: Thickness) =
            createDependencyProperty calendarItemBorderThicknessPropertyId CalendarView.CalendarItemBorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member calendarItemCornerRadius(node: VirtualNode<CalendarView>, value: CornerRadius) =
            createDependencyProperty calendarItemCornerRadiusPropertyId CalendarView.CalendarItemCornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member calendarItemDisabledBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty calendarItemDisabledBackgroundPropertyId CalendarView.CalendarItemDisabledBackgroundProperty value
            |> node.set

        [<Extension>]
        static member calendarItemForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty calendarItemForegroundPropertyId CalendarView.CalendarItemForegroundProperty value
            |> node.set

        [<Extension>]
        static member calendarItemHoverBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty calendarItemHoverBackgroundPropertyId CalendarView.CalendarItemHoverBackgroundProperty value
            |> node.set

        [<Extension>]
        static member calendarItemPressedBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty calendarItemPressedBackgroundPropertyId CalendarView.CalendarItemPressedBackgroundProperty value
            |> node.set

        [<Extension>]
        static member calendarViewDayItemStyle(node: VirtualNode<CalendarView>, value: Style) =
            createDependencyProperty calendarViewDayItemStylePropertyId CalendarView.CalendarViewDayItemStyleProperty value
            |> node.set

        [<Extension>]
        static member dayItemFontFamily(node: VirtualNode<CalendarView>, value: FontFamily) =
            createDependencyProperty dayItemFontFamilyPropertyId CalendarView.DayItemFontFamilyProperty value
            |> node.set

        [<Extension>]
        static member dayItemFontSize(node: VirtualNode<CalendarView>, value: float) =
            createDependencyProperty dayItemFontSizePropertyId CalendarView.DayItemFontSizeProperty value
            |> node.set

        [<Extension>]
        static member dayItemFontStyle(node: VirtualNode<CalendarView>, value: FontStyle) =
            createDependencyProperty dayItemFontStylePropertyId CalendarView.DayItemFontStyleProperty value
            |> node.set

        [<Extension>]
        static member dayItemFontWeight(node: VirtualNode<CalendarView>, value: FontWeight) =
            createDependencyProperty dayItemFontWeightPropertyId CalendarView.DayItemFontWeightProperty value
            |> node.set

        [<Extension>]
        static member dayItemMargin(node: VirtualNode<CalendarView>, value: Thickness) =
            createDependencyProperty dayItemMarginPropertyId CalendarView.DayItemMarginProperty value
            |> node.set

        [<Extension>]
        static member dayOfWeekFormat(node: VirtualNode<CalendarView>, value: string) =
            createDependencyProperty dayOfWeekFormatPropertyId CalendarView.DayOfWeekFormatProperty value
            |> node.set

        [<Extension>]
        static member disabledForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty disabledForegroundPropertyId CalendarView.DisabledForegroundProperty value
            |> node.set

        [<Extension>]
        static member displayMode(node: VirtualNode<CalendarView>, value: CalendarViewDisplayMode) =
            createDependencyProperty displayModePropertyId CalendarView.DisplayModeProperty value
            |> node.set

        [<Extension>]
        static member firstDayOfWeek(node: VirtualNode<CalendarView>, value: DayOfWeek) =
            createDependencyProperty firstDayOfWeekPropertyId CalendarView.FirstDayOfWeekProperty value
            |> node.set

        [<Extension>]
        static member firstOfMonthLabelFontFamily(node: VirtualNode<CalendarView>, value: FontFamily) =
            createDependencyProperty firstOfMonthLabelFontFamilyPropertyId CalendarView.FirstOfMonthLabelFontFamilyProperty value
            |> node.set

        [<Extension>]
        static member firstOfMonthLabelFontSize(node: VirtualNode<CalendarView>, value: float) =
            createDependencyProperty firstOfMonthLabelFontSizePropertyId CalendarView.FirstOfMonthLabelFontSizeProperty value
            |> node.set

        [<Extension>]
        static member firstOfMonthLabelFontStyle(node: VirtualNode<CalendarView>, value: FontStyle) =
            createDependencyProperty firstOfMonthLabelFontStylePropertyId CalendarView.FirstOfMonthLabelFontStyleProperty value
            |> node.set

        [<Extension>]
        static member firstOfMonthLabelFontWeight(node: VirtualNode<CalendarView>, value: FontWeight) =
            createDependencyProperty firstOfMonthLabelFontWeightPropertyId CalendarView.FirstOfMonthLabelFontWeightProperty value
            |> node.set

        [<Extension>]
        static member firstOfMonthLabelMargin(node: VirtualNode<CalendarView>, value: Thickness) =
            createDependencyProperty firstOfMonthLabelMarginPropertyId CalendarView.FirstOfMonthLabelMarginProperty value
            |> node.set

        [<Extension>]
        static member firstOfYearDecadeLabelFontFamily(node: VirtualNode<CalendarView>, value: FontFamily) =
            createDependencyProperty firstOfYearDecadeLabelFontFamilyPropertyId CalendarView.FirstOfYearDecadeLabelFontFamilyProperty value
            |> node.set

        [<Extension>]
        static member firstOfYearDecadeLabelFontSize(node: VirtualNode<CalendarView>, value: float) =
            createDependencyProperty firstOfYearDecadeLabelFontSizePropertyId CalendarView.FirstOfYearDecadeLabelFontSizeProperty value
            |> node.set

        [<Extension>]
        static member firstOfYearDecadeLabelFontStyle(node: VirtualNode<CalendarView>, value: FontStyle) =
            createDependencyProperty firstOfYearDecadeLabelFontStylePropertyId CalendarView.FirstOfYearDecadeLabelFontStyleProperty value
            |> node.set

        [<Extension>]
        static member firstOfYearDecadeLabelFontWeight(node: VirtualNode<CalendarView>, value: FontWeight) =
            createDependencyProperty firstOfYearDecadeLabelFontWeightPropertyId CalendarView.FirstOfYearDecadeLabelFontWeightProperty value
            |> node.set

        [<Extension>]
        static member firstOfYearDecadeLabelMargin(node: VirtualNode<CalendarView>, value: Thickness) =
            createDependencyProperty firstOfYearDecadeLabelMarginPropertyId CalendarView.FirstOfYearDecadeLabelMarginProperty value
            |> node.set

        [<Extension>]
        static member focusBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty focusBorderBrushPropertyId CalendarView.FocusBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member horizontalDayItemAlignment(node: VirtualNode<CalendarView>, value: HorizontalAlignment) =
            createDependencyProperty horizontalDayItemAlignmentPropertyId CalendarView.HorizontalDayItemAlignmentProperty value
            |> node.set

        [<Extension>]
        static member horizontalFirstOfMonthLabelAlignment(node: VirtualNode<CalendarView>, value: HorizontalAlignment) =
            createDependencyProperty horizontalFirstOfMonthLabelAlignmentPropertyId CalendarView.HorizontalFirstOfMonthLabelAlignmentProperty value
            |> node.set

        [<Extension>]
        static member hoverBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty hoverBorderBrushPropertyId CalendarView.HoverBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member isGroupLabelVisible(node: VirtualNode<CalendarView>, value: bool) =
            createDependencyProperty isGroupLabelVisiblePropertyId CalendarView.IsGroupLabelVisibleProperty value
            |> node.set

        [<Extension>]
        static member isOutOfScopeEnabled(node: VirtualNode<CalendarView>, value: bool) =
            createDependencyProperty isOutOfScopeEnabledPropertyId CalendarView.IsOutOfScopeEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTodayHighlighted(node: VirtualNode<CalendarView>, value: bool) =
            createDependencyProperty isTodayHighlightedPropertyId CalendarView.IsTodayHighlightedProperty value
            |> node.set

        [<Extension>]
        static member maxDate(node: VirtualNode<CalendarView>, value: DateTimeOffset) =
            createDependencyProperty maxDatePropertyId CalendarView.MaxDateProperty value
            |> node.set

        [<Extension>]
        static member minDate(node: VirtualNode<CalendarView>, value: DateTimeOffset) =
            createDependencyProperty minDatePropertyId CalendarView.MinDateProperty value
            |> node.set

        [<Extension>]
        static member monthYearItemFontFamily(node: VirtualNode<CalendarView>, value: FontFamily) =
            createDependencyProperty monthYearItemFontFamilyPropertyId CalendarView.MonthYearItemFontFamilyProperty value
            |> node.set

        [<Extension>]
        static member monthYearItemFontSize(node: VirtualNode<CalendarView>, value: float) =
            createDependencyProperty monthYearItemFontSizePropertyId CalendarView.MonthYearItemFontSizeProperty value
            |> node.set

        [<Extension>]
        static member monthYearItemFontStyle(node: VirtualNode<CalendarView>, value: FontStyle) =
            createDependencyProperty monthYearItemFontStylePropertyId CalendarView.MonthYearItemFontStyleProperty value
            |> node.set

        [<Extension>]
        static member monthYearItemFontWeight(node: VirtualNode<CalendarView>, value: FontWeight) =
            createDependencyProperty monthYearItemFontWeightPropertyId CalendarView.MonthYearItemFontWeightProperty value
            |> node.set

        [<Extension>]
        static member monthYearItemMargin(node: VirtualNode<CalendarView>, value: Thickness) =
            createDependencyProperty monthYearItemMarginPropertyId CalendarView.MonthYearItemMarginProperty value
            |> node.set

        [<Extension>]
        static member numberOfWeeksInView(node: VirtualNode<CalendarView>, value: int) =
            createDependencyProperty numberOfWeeksInViewPropertyId CalendarView.NumberOfWeeksInViewProperty value
            |> node.set

        [<Extension>]
        static member outOfScopeBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty outOfScopeBackgroundPropertyId CalendarView.OutOfScopeBackgroundProperty value
            |> node.set

        [<Extension>]
        static member outOfScopeForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty outOfScopeForegroundPropertyId CalendarView.OutOfScopeForegroundProperty value
            |> node.set

        [<Extension>]
        static member outOfScopeHoverForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty outOfScopeHoverForegroundPropertyId CalendarView.OutOfScopeHoverForegroundProperty value
            |> node.set

        [<Extension>]
        static member outOfScopePressedForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty outOfScopePressedForegroundPropertyId CalendarView.OutOfScopePressedForegroundProperty value
            |> node.set

        [<Extension>]
        static member pressedBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty pressedBorderBrushPropertyId CalendarView.PressedBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member pressedForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty pressedForegroundPropertyId CalendarView.PressedForegroundProperty value
            |> node.set

        [<Extension>]
        static member selectedBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedBorderBrushPropertyId CalendarView.SelectedBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedDisabledBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedDisabledBorderBrushPropertyId CalendarView.SelectedDisabledBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedDisabledForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedDisabledForegroundPropertyId CalendarView.SelectedDisabledForegroundProperty value
            |> node.set

        [<Extension>]
        static member selectedForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedForegroundPropertyId CalendarView.SelectedForegroundProperty value
            |> node.set

        [<Extension>]
        static member selectedHoverBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedHoverBorderBrushPropertyId CalendarView.SelectedHoverBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedHoverForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedHoverForegroundPropertyId CalendarView.SelectedHoverForegroundProperty value
            |> node.set

        [<Extension>]
        static member selectedPressedBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedPressedBorderBrushPropertyId CalendarView.SelectedPressedBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member selectedPressedForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty selectedPressedForegroundPropertyId CalendarView.SelectedPressedForegroundProperty value
            |> node.set

        [<Extension>]
        static member selectionMode(node: VirtualNode<CalendarView>, value: CalendarViewSelectionMode) =
            createDependencyProperty selectionModePropertyId CalendarView.SelectionModeProperty value
            |> node.set

        [<Extension>]
        static member todayBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todayBackgroundPropertyId CalendarView.TodayBackgroundProperty value
            |> node.set

        [<Extension>]
        static member todayBlackoutBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todayBlackoutBackgroundPropertyId CalendarView.TodayBlackoutBackgroundProperty value
            |> node.set

        [<Extension>]
        static member todayBlackoutForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todayBlackoutForegroundPropertyId CalendarView.TodayBlackoutForegroundProperty value
            |> node.set

        [<Extension>]
        static member todayDisabledBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todayDisabledBackgroundPropertyId CalendarView.TodayDisabledBackgroundProperty value
            |> node.set

        [<Extension>]
        static member todayFontWeight(node: VirtualNode<CalendarView>, value: FontWeight) =
            createDependencyProperty todayFontWeightPropertyId CalendarView.TodayFontWeightProperty value
            |> node.set

        [<Extension>]
        static member todayForeground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todayForegroundPropertyId CalendarView.TodayForegroundProperty value
            |> node.set

        [<Extension>]
        static member todayHoverBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todayHoverBackgroundPropertyId CalendarView.TodayHoverBackgroundProperty value
            |> node.set

        [<Extension>]
        static member todayPressedBackground(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todayPressedBackgroundPropertyId CalendarView.TodayPressedBackgroundProperty value
            |> node.set

        [<Extension>]
        static member todaySelectedInnerBorderBrush(node: VirtualNode<CalendarView>, value: Brush) =
            createDependencyProperty todaySelectedInnerBorderBrushPropertyId CalendarView.TodaySelectedInnerBorderBrushProperty value
            |> node.set

        [<Extension>]
        static member verticalDayItemAlignment(node: VirtualNode<CalendarView>, value: VerticalAlignment) =
            createDependencyProperty verticalDayItemAlignmentPropertyId CalendarView.VerticalDayItemAlignmentProperty value
            |> node.set

        [<Extension>]
        static member verticalFirstOfMonthLabelAlignment(node: VirtualNode<CalendarView>, value: VerticalAlignment) =
            createDependencyProperty verticalFirstOfMonthLabelAlignmentPropertyId CalendarView.VerticalFirstOfMonthLabelAlignmentProperty value
            |> node.set

        [<Extension>]
        static member onCalendarViewDayItemChanging(node: VirtualNode<CalendarView>, handler: CalendarView -> CalendarViewDayItemChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CalendarViewDayItemChanging"
                Subscribe = (fun uiElement ->
                    let calendarView = uiElement :?> CalendarView
                    let h = TypedEventHandler<CalendarView, CalendarViewDayItemChangingEventArgs>(fun _ e -> handler calendarView e)
                    calendarView.add_CalendarViewDayItemChanging h
                    { new IDisposable with
                        member _.Dispose() = calendarView.remove_CalendarViewDayItemChanging h }
                )
            })

        [<Extension>]
        static member onSelectedDatesChanged(node: VirtualNode<CalendarView>, handler: CalendarView -> CalendarViewSelectedDatesChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectedDatesChanged"
                Subscribe = (fun uiElement ->
                    let calendarView = uiElement :?> CalendarView
                    let h = TypedEventHandler<CalendarView, CalendarViewSelectedDatesChangedEventArgs>(fun _ e -> handler calendarView e)
                    calendarView.add_SelectedDatesChanged h
                    { new IDisposable with
                        member _.Dispose() = calendarView.remove_SelectedDatesChanged h }
                )
            })

    let calendarView () =
        createNode<CalendarView> calendarViewNodeId
