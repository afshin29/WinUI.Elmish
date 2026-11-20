namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module RadioButtonMod =

    let private radioButtonNodeId = getNextId ()

    registerDefaultRenderer<RadioButton> radioButtonNodeId

    let private groupNamePropertyId = getNextId ()

    [<Extension>]
    type RadioButtonExtensions =

        [<Extension>]
        static member groupName<'m when 'm :> RadioButton and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty groupNamePropertyId RadioButton.GroupNameProperty value
            |> node.set

    let radioButton () =
        createNode<RadioButton> radioButtonNodeId
