namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module TickBarMod =

    let private tickBarNodeId = getNextId ()

    registerDefaultRenderer<TickBar> tickBarNodeId

    let private fillPropertyId = getNextId ()

    [<Extension>]
    type TickBarExtensions =

        [<Extension>]
        static member fill(node: VirtualNode<TickBar>, value: Brush) =
            createDependencyProperty fillPropertyId TickBar.FillProperty value
            |> node.set

    let tickBar () =
        createNode<TickBar> tickBarNodeId
