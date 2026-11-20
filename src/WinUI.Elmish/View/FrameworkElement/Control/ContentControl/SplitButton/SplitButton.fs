namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices
open System.Windows.Input
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module SplitButtonMod =

    let private splitButtonNodeId = getNextId ()

    registerDefaultRenderer<SplitButton> splitButtonNodeId

    let private commandParameterPropertyId = getNextId ()
    let private commandPropertyId = getNextId ()
    let private flyoutPropertyId = getNextId ()

    [<Extension>]
    type SplitButtonExtensions =

        [<Extension>]
        static member commandParameter<'m when 'm :> SplitButton and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty commandParameterPropertyId SplitButton.CommandParameterProperty value
            |> node.set

        [<Extension>]
        static member command<'m when 'm :> SplitButton and 'm: not struct>
            (node: VirtualNode<'m>, value: ICommand) =
            createDependencyProperty commandPropertyId SplitButton.CommandProperty value
            |> node.set

        [<Extension>]
        static member flyout<'m when 'm :> SplitButton and 'm: not struct>
            (node: VirtualNode<'m>, value: FlyoutBase) =
            createDependencyProperty flyoutPropertyId SplitButton.FlyoutProperty value
            |> node.set

        [<Extension>]
        static member onClick<'m when 'm :> SplitButton and 'm: not struct>
            (node: VirtualNode<'m>, handler: SplitButtonClickEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Click"
                Subscribe = (fun uiElement ->
                    let splitButton = uiElement :?> SplitButton
                    let h = TypedEventHandler<SplitButton, SplitButtonClickEventArgs>(fun _ e -> handler e)
                    splitButton.add_Click h
                    { new IDisposable with
                        member _.Dispose() = splitButton.remove_Click h }
                )
            })

    let splitButton () =
        createNode<SplitButton> splitButtonNodeId
