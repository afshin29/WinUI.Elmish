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
module HyperlinkButtonMod =

    let private hyperlinkButtonNodeId = getNextId ()

    registerDefaultRenderer<HyperlinkButton> hyperlinkButtonNodeId

    let private navigateUriPropertyId = getNextId ()

    [<Extension>]
    type HyperlinkButtonExtensions =

        [<Extension>]
        static member navigateUri(node: VirtualNode<HyperlinkButton>, value: Uri) =
            createDependencyProperty navigateUriPropertyId HyperlinkButton.NavigateUriProperty value
            |> node.set

    let hyperlinkButton () =
        createNode<HyperlinkButton> hyperlinkButtonNodeId
