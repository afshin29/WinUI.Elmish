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
module TimePickerMod =

    let private timePickerNodeId = getNextId ()

    registerDefaultRenderer<TimePicker> timePickerNodeId

    let private clockIdentifierPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()
    let private minuteIncrementPropertyId = getNextId ()
    let private selectedTimePropertyId = getNextId ()
    let private timePropertyId = getNextId ()

    [<Extension>]
    type TimePickerExtensions =

        [<Extension>]
        static member clockIdentifier(node: VirtualNode<TimePicker>, value: string) =
            createDependencyProperty clockIdentifierPropertyId TimePicker.ClockIdentifierProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<TimePicker>, value: string) =
            createDependencyProperty str_headerPropertyId TimePicker.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TimePicker>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> TimePicker).Header <- child)
                (fun element -> (element :?> TimePicker).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<TimePicker>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId TimePicker.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode(node: VirtualNode<TimePicker>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId TimePicker.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member minuteIncrement(node: VirtualNode<TimePicker>, value: int) =
            createDependencyProperty minuteIncrementPropertyId TimePicker.MinuteIncrementProperty value
            |> node.set

        [<Extension>]
        static member selectedTime(node: VirtualNode<TimePicker>, value: Nullable<TimeSpan>) =
            createDependencyProperty selectedTimePropertyId TimePicker.SelectedTimeProperty value
            |> node.set

        [<Extension>]
        static member time(node: VirtualNode<TimePicker>, value: TimeSpan) =
            createDependencyProperty timePropertyId TimePicker.TimeProperty value
            |> node.set

        [<Extension>]
        static member onSelectedTimeChanged(node: VirtualNode<TimePicker>, handler: TimePicker -> TimePickerSelectedValueChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectedTimeChanged"
                Subscribe = (fun uiElement ->
                    let timePicker = uiElement :?> TimePicker
                    let h = TypedEventHandler<TimePicker, TimePickerSelectedValueChangedEventArgs>(fun _ e -> handler timePicker e)
                    timePicker.add_SelectedTimeChanged h
                    { new IDisposable with
                        member _.Dispose() = timePicker.remove_SelectedTimeChanged h }
                )
            })

        [<Extension>]
        static member onTimeChanged(node: VirtualNode<TimePicker>, handler: TimePicker -> TimePickerValueChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TimeChanged"
                Subscribe = (fun uiElement ->
                    let timePicker = uiElement :?> TimePicker
                    let h = EventHandler<TimePickerValueChangedEventArgs>(fun _ e -> handler timePicker e)
                    timePicker.TimeChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = timePicker.TimeChanged.RemoveHandler h }
                )
            })

    let timePicker () =
        createNode<TimePicker> timePickerNodeId
