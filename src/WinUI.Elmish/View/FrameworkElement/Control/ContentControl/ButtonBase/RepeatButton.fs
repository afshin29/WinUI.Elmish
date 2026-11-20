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
module RepeatButtonMod =

    let private repeatButtonNodeId = getNextId ()

    registerDefaultRenderer<RepeatButton> repeatButtonNodeId

    let private delayPropertyId = getNextId ()
    let private intervalPropertyId = getNextId ()

    [<Extension>]
    type RepeatButtonExtensions =

        [<Extension>]
        static member delay(node: VirtualNode<RepeatButton>, value: int) =
            createDependencyProperty delayPropertyId RepeatButton.DelayProperty value
            |> node.set

        [<Extension>]
        static member interval(node: VirtualNode<RepeatButton>, value: int) =
            createDependencyProperty intervalPropertyId RepeatButton.IntervalProperty value
            |> node.set

    let repeatButton () =
        createNode<RepeatButton> repeatButtonNodeId
