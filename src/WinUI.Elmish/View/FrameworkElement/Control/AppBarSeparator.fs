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
module AppBarSeparatorMod =

    let private appBarSeparatorNodeId = getNextId ()

    registerDefaultRenderer<AppBarSeparator> appBarSeparatorNodeId

    let private dynamicOverflowOrderPropertyId = getNextId ()
    let private isCompactPropertyId = getNextId ()

    [<Extension>]
    type AppBarSeparatorExtensions =

        [<Extension>]
        static member dynamicOverflowOrder(node: VirtualNode<AppBarSeparator>, value: int) =
            createDependencyProperty dynamicOverflowOrderPropertyId AppBarSeparator.DynamicOverflowOrderProperty value
            |> node.set

        [<Extension>]
        static member isCompact(node: VirtualNode<AppBarSeparator>, value: bool) =
            createDependencyProperty isCompactPropertyId AppBarSeparator.IsCompactProperty value
            |> node.set

    let appBarSeparator () =
        createNode<AppBarSeparator> appBarSeparatorNodeId
