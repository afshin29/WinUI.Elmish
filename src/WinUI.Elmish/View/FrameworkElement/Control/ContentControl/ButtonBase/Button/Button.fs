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
module ButtonMod =

    let private buttonNodeId = getNextId ()

    registerDefaultRenderer<Button> buttonNodeId

    let private flyoutPropertyId = getNextId ()

    [<Extension>]
    type ButtonExtensions =

        [<Extension>]
        static member flyout<'m when 'm :> Button and 'm: not struct>
            (node: VirtualNode<'m>, value: FlyoutBase) =
            createDependencyProperty flyoutPropertyId Button.FlyoutProperty value
            |> node.set

    let button () =
        createNode<Button> buttonNodeId

    let button' (text: string) =
        button()
            .content(text)

    let button_accent () =
        button()
            .style(getStaticResource "AccentButtonStyle")

    let button_accent' (text: string) =
        button_accent()
            .content(text)
