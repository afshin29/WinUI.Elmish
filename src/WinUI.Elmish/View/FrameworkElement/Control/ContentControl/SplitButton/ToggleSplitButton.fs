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
module ToggleSplitButtonMod =

    let private toggleSplitButtonNodeId = getNextId ()

    registerDefaultRenderer<ToggleSplitButton> toggleSplitButtonNodeId

    let private isCheckedPropertyId = getNextId ()

    [<Extension>]
    type ToggleSplitButtonExtensions =

        [<Extension>]
        static member isChecked(node: VirtualNode<ToggleSplitButton>, value: bool) =
            createDependencyProperty isCheckedPropertyId ToggleSplitButton.IsCheckedProperty value
            |> node.set

        [<Extension>]
        static member onIsCheckedChanged(node: VirtualNode<ToggleSplitButton>, handler: ToggleSplitButtonIsCheckedChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "IsCheckedChanged"
                Subscribe = (fun uiElement ->
                    let toggleSplitButton = uiElement :?> ToggleSplitButton
                    let h = TypedEventHandler<ToggleSplitButton, ToggleSplitButtonIsCheckedChangedEventArgs>(fun _ e -> handler e)
                    toggleSplitButton.add_IsCheckedChanged h
                    { new IDisposable with
                        member _.Dispose() = toggleSplitButton.remove_IsCheckedChanged h }
                )
            })

    let toggleSplitButton () =
        createNode<ToggleSplitButton> toggleSplitButtonNodeId
