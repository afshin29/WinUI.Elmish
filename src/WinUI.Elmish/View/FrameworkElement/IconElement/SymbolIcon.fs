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
module SymbolIconMod =

    let private symbolIconNodeId = getNextId ()

    registerDefaultRenderer<SymbolIcon> symbolIconNodeId

    let private symbolPropertyId = getNextId ()

    [<Extension>]
    type SymbolIconExtensions =

        [<Extension>]
        static member symbol(node: VirtualNode<SymbolIcon>, value: Symbol) =
            createDependencyProperty symbolPropertyId SymbolIcon.SymbolProperty value
            |> node.set

    let symbolIcon () =
        createNode<SymbolIcon> symbolIconNodeId
