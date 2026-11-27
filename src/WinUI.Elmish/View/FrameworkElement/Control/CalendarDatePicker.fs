namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open Windows.Foundation
open Windows.Globalization

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module CalendarDatePickerMod =

    let private calendarDatePickerNodeId = getNextId ()

    registerDefaultRenderer<CalendarDatePicker> calendarDatePickerNodeId

    let private calendarIdentifierPropertyId = getNextId ()
    let private calendarViewStylePropertyId = getNextId ()
    let private dateFormatPropertyId = getNextId ()
    let private datePropertyId = getNextId ()
    let private dayOfWeekFormatPropertyId = getNextId ()
    let private descriptionPropertyId = getNextId ()
    let private displayModePropertyId = getNextId ()
    let private firstDayOfWeekPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private isCalendarOpenPropertyId = getNextId ()
    let private isGroupLabelVisiblePropertyId = getNextId ()
    let private isOutOfScopeEnabledPropertyId = getNextId ()
    let private isTodayHighlightedPropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()
    let private maxDatePropertyId = getNextId ()
    let private minDatePropertyId = getNextId ()
    let private placeholderTextPropertyId = getNextId ()

    [<Extension>]
    type CalendarDatePickerExtensions =

        [<Extension>]
        static member calendarIdentifier(node: VirtualNode<CalendarDatePicker>, value: string) =
            createDependencyProperty calendarIdentifierPropertyId CalendarDatePicker.CalendarIdentifierProperty value
            |> node.set

        [<Extension>]
        static member calendarViewStyle(node: VirtualNode<CalendarDatePicker>, value: Style) =
            createDependencyProperty calendarViewStylePropertyId CalendarDatePicker.CalendarViewStyleProperty value
            |> node.set

        [<Extension>]
        static member dateFormat(node: VirtualNode<CalendarDatePicker>, value: string) =
            createDependencyProperty dateFormatPropertyId CalendarDatePicker.DateFormatProperty value
            |> node.set

        [<Extension>]
        static member date(node: VirtualNode<CalendarDatePicker>, value: Nullable<DateTimeOffset>) =
            createDependencyProperty datePropertyId CalendarDatePicker.DateProperty value
            |> node.set

        [<Extension>]
        static member dayOfWeekFormat(node: VirtualNode<CalendarDatePicker>, value: string) =
            createDependencyProperty dayOfWeekFormatPropertyId CalendarDatePicker.DayOfWeekFormatProperty value
            |> node.set

        [<Extension>]
        static member description(node: VirtualNode<CalendarDatePicker>, value: obj) =
            createDependencyProperty descriptionPropertyId CalendarDatePicker.DescriptionProperty value
            |> node.set

        [<Extension>]
        static member displayMode(node: VirtualNode<CalendarDatePicker>, value: CalendarViewDisplayMode) =
            createDependencyProperty displayModePropertyId CalendarDatePicker.DisplayModeProperty value
            |> node.set

        [<Extension>]
        static member firstDayOfWeek(node: VirtualNode<CalendarDatePicker>, value: DayOfWeek) =
            createDependencyProperty firstDayOfWeekPropertyId CalendarDatePicker.FirstDayOfWeekProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<CalendarDatePicker>, value: string) =
            createDependencyProperty str_headerPropertyId CalendarDatePicker.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<CalendarDatePicker>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> CalendarDatePicker).Header <- child)
                (fun element -> (element :?> CalendarDatePicker).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<CalendarDatePicker>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId CalendarDatePicker.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member isCalendarOpen(node: VirtualNode<CalendarDatePicker>, value: bool) =
            createDependencyProperty isCalendarOpenPropertyId CalendarDatePicker.IsCalendarOpenProperty value
            |> node.set

        [<Extension>]
        static member isGroupLabelVisible(node: VirtualNode<CalendarDatePicker>, value: bool) =
            createDependencyProperty isGroupLabelVisiblePropertyId CalendarDatePicker.IsGroupLabelVisibleProperty value
            |> node.set

        [<Extension>]
        static member isOutOfScopeEnabled(node: VirtualNode<CalendarDatePicker>, value: bool) =
            createDependencyProperty isOutOfScopeEnabledPropertyId CalendarDatePicker.IsOutOfScopeEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTodayHighlighted(node: VirtualNode<CalendarDatePicker>, value: bool) =
            createDependencyProperty isTodayHighlightedPropertyId CalendarDatePicker.IsTodayHighlightedProperty value
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode(node: VirtualNode<CalendarDatePicker>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId CalendarDatePicker.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member maxDate(node: VirtualNode<CalendarDatePicker>, value: DateTimeOffset) =
            createDependencyProperty maxDatePropertyId CalendarDatePicker.MaxDateProperty value
            |> node.set

        [<Extension>]
        static member minDate(node: VirtualNode<CalendarDatePicker>, value: DateTimeOffset) =
            createDependencyProperty minDatePropertyId CalendarDatePicker.MinDateProperty value
            |> node.set

        [<Extension>]
        static member placeholderText(node: VirtualNode<CalendarDatePicker>, value: string) =
            createDependencyProperty placeholderTextPropertyId CalendarDatePicker.PlaceholderTextProperty value
            |> node.set

        [<Extension>]
        static member onCalendarViewDayItemChanging(node: VirtualNode<CalendarDatePicker>, handler: CalendarDatePicker -> CalendarViewDayItemChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CalendarViewDayItemChanging"
                Subscribe = (fun uiElement ->
                    let calendarDatePicker = uiElement :?> CalendarDatePicker
                    let h = CalendarViewDayItemChangingEventHandler(fun _ e -> handler calendarDatePicker e)
                    calendarDatePicker.add_CalendarViewDayItemChanging h
                    { new IDisposable with
                        member _.Dispose() = calendarDatePicker.remove_CalendarViewDayItemChanging h }
                )
            })

        [<Extension>]
        static member onClosed(node: VirtualNode<CalendarDatePicker>, handler: CalendarDatePicker -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closed"
                Subscribe = (fun uiElement ->
                    let calendarDatePicker = uiElement :?> CalendarDatePicker
                    let h = EventHandler<obj>(fun _ e -> handler calendarDatePicker e)
                    calendarDatePicker.Closed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = calendarDatePicker.Closed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDateChanged(node: VirtualNode<CalendarDatePicker>, handler: CalendarDatePicker -> CalendarDatePickerDateChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DateChanged"
                Subscribe = (fun uiElement ->
                    let calendarDatePicker = uiElement :?> CalendarDatePicker
                    let h = TypedEventHandler<CalendarDatePicker, CalendarDatePickerDateChangedEventArgs>(fun _ e -> handler calendarDatePicker e)
                    calendarDatePicker.add_DateChanged h
                    { new IDisposable with
                        member _.Dispose() = calendarDatePicker.remove_DateChanged h }
                )
            })

        [<Extension>]
        static member onOpened(node: VirtualNode<CalendarDatePicker>, handler: CalendarDatePicker -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opened"
                Subscribe = (fun uiElement ->
                    let calendarDatePicker = uiElement :?> CalendarDatePicker
                    let h = EventHandler<obj>(fun _ e -> handler calendarDatePicker e)
                    calendarDatePicker.Opened.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = calendarDatePicker.Opened.RemoveHandler h }
                )
            })

    let calendarDatePicker () =
        createNode<CalendarDatePicker> calendarDatePickerNodeId
