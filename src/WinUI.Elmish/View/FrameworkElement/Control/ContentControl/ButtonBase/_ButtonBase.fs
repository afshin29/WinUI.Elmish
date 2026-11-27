namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices
open System.Windows.Input

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property IsPointerOverProperty: read-only
// skipped dependency property IsPressedProperty: read-only


[<AutoOpen>]
module ButtonBaseMod =

    let private clickModePropertyId = getNextId ()
    let private commandParameterPropertyId = getNextId ()
    let private commandPropertyId = getNextId ()

    [<Extension>]
    type ButtonBaseExtensions =

        [<Extension>]
        static member clickMode<'m when 'm :> ButtonBase and 'm: not struct>
            (node: VirtualNode<'m>, value: ClickMode) =
            createDependencyProperty clickModePropertyId ButtonBase.ClickModeProperty value
            |> node.set

        [<Extension>]
        static member commandParameter<'m when 'm :> ButtonBase and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty commandParameterPropertyId ButtonBase.CommandParameterProperty value
            |> node.set

        [<Extension>]
        static member command<'m when 'm :> ButtonBase and 'm: not struct>
            (node: VirtualNode<'m>, value: ICommand) =
            createDependencyProperty commandPropertyId ButtonBase.CommandProperty value
            |> node.set

        [<Extension>]
        static member onClick<'m when 'm :> ButtonBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Click"
                Subscribe = (fun uiElement ->
                    let buttonBase = uiElement :?> ButtonBase
                    let sender = uiElement :?> 'm
                    let h = RoutedEventHandler(fun _ e -> handler sender e)
                    buttonBase.Click.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = buttonBase.Click.RemoveHandler h }
                )
            })
