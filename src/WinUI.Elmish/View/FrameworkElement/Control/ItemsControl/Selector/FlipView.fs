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
module FlipViewMod =

    let private flipViewNodeId = getNextId ()

    registerDefaultRenderer<FlipView> flipViewNodeId

    let private useTouchAnimationsForAllNavigationPropertyId = getNextId ()

    [<Extension>]
    type FlipViewExtensions =

        [<Extension>]
        static member useTouchAnimationsForAllNavigation(node: VirtualNode<FlipView>, value: bool) =
            createDependencyProperty useTouchAnimationsForAllNavigationPropertyId FlipView.UseTouchAnimationsForAllNavigationProperty value
            |> node.set

    let flipView () =
        createNode<FlipView> flipViewNodeId
