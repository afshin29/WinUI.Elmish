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

// skipped dependency property IsInOverflowProperty: read-only


[<AutoOpen>]
module AppBarElementContainerMod =

    let private appBarElementContainerNodeId = getNextId ()

    registerDefaultRenderer<AppBarElementContainer> appBarElementContainerNodeId

    let private dynamicOverflowOrderPropertyId = getNextId ()
    let private isCompactPropertyId = getNextId ()

    [<Extension>]
    type AppBarElementContainerExtensions =

        [<Extension>]
        static member dynamicOverflowOrder(node: VirtualNode<AppBarElementContainer>, value: int) =
            createDependencyProperty dynamicOverflowOrderPropertyId AppBarElementContainer.DynamicOverflowOrderProperty value
            |> node.set

        [<Extension>]
        static member isCompact(node: VirtualNode<AppBarElementContainer>, value: bool) =
            createDependencyProperty isCompactPropertyId AppBarElementContainer.IsCompactProperty value
            |> node.set

    let appBarElementContainer () =
        createNode<AppBarElementContainer> appBarElementContainerNodeId
