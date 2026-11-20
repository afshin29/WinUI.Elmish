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
module InkToolbarEraserButtonMod =

    let private inkToolbarEraserButtonNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarEraserButton> inkToolbarEraserButtonNodeId

    let private isClearAllVisiblePropertyId = getNextId ()

    [<Extension>]
    type InkToolbarEraserButtonExtensions =

        [<Extension>]
        static member isClearAllVisible(node: VirtualNode<InkToolbarEraserButton>, value: bool) =
            createDependencyProperty isClearAllVisiblePropertyId InkToolbarEraserButton.IsClearAllVisibleProperty value
            |> node.set

    let inkToolbarEraserButton () =
        createNode<InkToolbarEraserButton> inkToolbarEraserButtonNodeId
