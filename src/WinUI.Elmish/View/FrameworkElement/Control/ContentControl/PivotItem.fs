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
module PivotItemMod =

    let private pivotItemNodeId = getNextId ()

    registerDefaultRenderer<PivotItem> pivotItemNodeId

    let private headerPropertyId = getNextId ()

    [<Extension>]
    type PivotItemExtensions =

        [<Extension>]
        static member header(node: VirtualNode<PivotItem>, value: obj) =
            createDependencyProperty headerPropertyId PivotItem.HeaderProperty value
            |> node.set

    let pivotItem () =
        createNode<PivotItem> pivotItemNodeId
