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
module SwipeControlMod =

    let private swipeControlNodeId = getNextId ()

    registerDefaultRenderer<SwipeControl> swipeControlNodeId

    let private bottomItemsPropertyId = getNextId ()
    let private leftItemsPropertyId = getNextId ()
    let private rightItemsPropertyId = getNextId ()
    let private topItemsPropertyId = getNextId ()

    [<Extension>]
    type SwipeControlExtensions =

        [<Extension>]
        static member bottomItems(node: VirtualNode<SwipeControl>, value: SwipeItems) =
            createDependencyProperty bottomItemsPropertyId SwipeControl.BottomItemsProperty value
            |> node.set

        [<Extension>]
        static member leftItems(node: VirtualNode<SwipeControl>, value: SwipeItems) =
            createDependencyProperty leftItemsPropertyId SwipeControl.LeftItemsProperty value
            |> node.set

        [<Extension>]
        static member rightItems(node: VirtualNode<SwipeControl>, value: SwipeItems) =
            createDependencyProperty rightItemsPropertyId SwipeControl.RightItemsProperty value
            |> node.set

        [<Extension>]
        static member topItems(node: VirtualNode<SwipeControl>, value: SwipeItems) =
            createDependencyProperty topItemsPropertyId SwipeControl.TopItemsProperty value
            |> node.set

    let swipeControl () =
        createNode<SwipeControl> swipeControlNodeId
