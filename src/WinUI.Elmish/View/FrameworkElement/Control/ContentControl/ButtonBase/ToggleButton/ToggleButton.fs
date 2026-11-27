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


[<AutoOpen>]
module ToggleButtonMod =

    let private toggleButtonNodeId = getNextId ()

    registerDefaultRenderer<ToggleButton> toggleButtonNodeId

    let private isCheckedPropertyId = getNextId ()
    let private isThreeStatePropertyId = getNextId ()

    [<Extension>]
    type ToggleButtonExtensions =

        [<Extension>]
        static member isChecked<'m when 'm :> ToggleButton and 'm: not struct>
            (node: VirtualNode<'m>, value: Nullable<bool>) =
            createDependencyProperty isCheckedPropertyId ToggleButton.IsCheckedProperty value
            |> node.set

        [<Extension>]
        static member isThreeState<'m when 'm :> ToggleButton and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isThreeStatePropertyId ToggleButton.IsThreeStateProperty value
            |> node.set

        [<Extension>]
        static member onChecked<'m when 'm :> ToggleButton and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Checked"
                Subscribe = (fun uiElement ->
                    let toggleButton = uiElement :?> ToggleButton
                    let sender = uiElement :?> 'm
                    let h = RoutedEventHandler(fun _ e -> handler sender e)
                    toggleButton.Checked.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = toggleButton.Checked.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onIndeterminate<'m when 'm :> ToggleButton and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Indeterminate"
                Subscribe = (fun uiElement ->
                    let toggleButton = uiElement :?> ToggleButton
                    let sender = uiElement :?> 'm
                    let h = RoutedEventHandler(fun _ e -> handler sender e)
                    toggleButton.Indeterminate.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = toggleButton.Indeterminate.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onUnchecked<'m when 'm :> ToggleButton and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Unchecked"
                Subscribe = (fun uiElement ->
                    let toggleButton = uiElement :?> ToggleButton
                    let sender = uiElement :?> 'm
                    let h = RoutedEventHandler(fun _ e -> handler sender e)
                    toggleButton.Unchecked.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = toggleButton.Unchecked.RemoveHandler h }
                )
            })

    let toggleButton () =
        createNode<ToggleButton> toggleButtonNodeId
