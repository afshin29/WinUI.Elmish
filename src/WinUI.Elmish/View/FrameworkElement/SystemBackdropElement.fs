namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module SystemBackdropElementMod =

    let private systemBackdropElementNodeId = getNextId ()

    registerDefaultRenderer<SystemBackdropElement> systemBackdropElementNodeId

    let private cornerRadiusPropertyId = getNextId ()
    let private systemBackdropPropertyId = getNextId ()

    [<Extension>]
    type SystemBackdropElementExtensions =

        [<Extension>]
        static member cornerRadius(node: VirtualNode<SystemBackdropElement>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId SystemBackdropElement.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member systemBackdrop(node: VirtualNode<SystemBackdropElement>, value: SystemBackdrop) =
            createDependencyProperty systemBackdropPropertyId SystemBackdropElement.SystemBackdropProperty value
            |> node.set

    let systemBackdropElement () =
        createNode<SystemBackdropElement> systemBackdropElementNodeId
