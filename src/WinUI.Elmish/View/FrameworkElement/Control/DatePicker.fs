namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module DatePickerMod =

    let private datePickerNodeId = getNextId ()

    registerDefaultRenderer<DatePicker> datePickerNodeId

    let private calendarIdentifierPropertyId = getNextId ()
    let private datePropertyId = getNextId ()
    let private dayFormatPropertyId = getNextId ()
    let private dayVisiblePropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()
    let private maxYearPropertyId = getNextId ()
    let private minYearPropertyId = getNextId ()
    let private monthFormatPropertyId = getNextId ()
    let private monthVisiblePropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private selectedDatePropertyId = getNextId ()
    let private yearFormatPropertyId = getNextId ()
    let private yearVisiblePropertyId = getNextId ()

    [<Extension>]
    type DatePickerExtensions =

        [<Extension>]
        static member calendarIdentifier(node: VirtualNode<DatePicker>, value: string) =
            createDependencyProperty calendarIdentifierPropertyId DatePicker.CalendarIdentifierProperty value
            |> node.set

        [<Extension>]
        static member date(node: VirtualNode<DatePicker>, value: DateTimeOffset) =
            createDependencyProperty datePropertyId DatePicker.DateProperty value
            |> node.set

        [<Extension>]
        static member dayFormat(node: VirtualNode<DatePicker>, value: string) =
            createDependencyProperty dayFormatPropertyId DatePicker.DayFormatProperty value
            |> node.set

        [<Extension>]
        static member dayVisible(node: VirtualNode<DatePicker>, value: bool) =
            createDependencyProperty dayVisiblePropertyId DatePicker.DayVisibleProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<DatePicker>, value: string) =
            createDependencyProperty str_headerPropertyId DatePicker.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<DatePicker>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> DatePicker).Header <- child)
                (fun element -> (element :?> DatePicker).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<DatePicker>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId DatePicker.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode(node: VirtualNode<DatePicker>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId DatePicker.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member maxYear(node: VirtualNode<DatePicker>, value: DateTimeOffset) =
            createDependencyProperty maxYearPropertyId DatePicker.MaxYearProperty value
            |> node.set

        [<Extension>]
        static member minYear(node: VirtualNode<DatePicker>, value: DateTimeOffset) =
            createDependencyProperty minYearPropertyId DatePicker.MinYearProperty value
            |> node.set

        [<Extension>]
        static member monthFormat(node: VirtualNode<DatePicker>, value: string) =
            createDependencyProperty monthFormatPropertyId DatePicker.MonthFormatProperty value
            |> node.set

        [<Extension>]
        static member monthVisible(node: VirtualNode<DatePicker>, value: bool) =
            createDependencyProperty monthVisiblePropertyId DatePicker.MonthVisibleProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<DatePicker>, value: Orientation) =
            createDependencyProperty orientationPropertyId DatePicker.OrientationProperty value
            |> node.set

        [<Extension>]
        static member selectedDate(node: VirtualNode<DatePicker>, value: Nullable<DateTimeOffset>) =
            createDependencyProperty selectedDatePropertyId DatePicker.SelectedDateProperty value
            |> node.set

        [<Extension>]
        static member yearFormat(node: VirtualNode<DatePicker>, value: string) =
            createDependencyProperty yearFormatPropertyId DatePicker.YearFormatProperty value
            |> node.set

        [<Extension>]
        static member yearVisible(node: VirtualNode<DatePicker>, value: bool) =
            createDependencyProperty yearVisiblePropertyId DatePicker.YearVisibleProperty value
            |> node.set

        [<Extension>]
        static member onDateChanged(node: VirtualNode<DatePicker>, handler: DatePicker -> DatePickerValueChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DateChanged"
                Subscribe = (fun uiElement ->
                    let datePicker = uiElement :?> DatePicker
                    let h = EventHandler<DatePickerValueChangedEventArgs>(fun _ e -> handler datePicker e)
                    datePicker.DateChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = datePicker.DateChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onSelectedDateChanged(node: VirtualNode<DatePicker>, handler: DatePicker -> DatePickerSelectedValueChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectedDateChanged"
                Subscribe = (fun uiElement ->
                    let datePicker = uiElement :?> DatePicker
                    let h = TypedEventHandler<DatePicker, DatePickerSelectedValueChangedEventArgs>(fun _ e -> handler datePicker e)
                    datePicker.add_SelectedDateChanged h
                    { new IDisposable with
                        member _.Dispose() = datePicker.remove_SelectedDateChanged h }
                )
            })

    let datePicker () =
        createNode<DatePicker> datePickerNodeId
