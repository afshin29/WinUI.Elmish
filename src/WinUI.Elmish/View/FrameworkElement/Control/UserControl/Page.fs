namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Navigation
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property FrameProperty: read-only


[<AutoOpen>]
module PageMod =

    let private pageNodeId = getNextId ()

    registerDefaultRenderer<Page> pageNodeId

    let private bottomAppBarPropertyId = getNextId ()
    let private topAppBarPropertyId = getNextId ()
    let private navigationCacheModePropertyId = getNextId ()

    [<Extension>]
    type PageExtensions =

        [<Extension>]
        static member bottomAppBar(node: VirtualNode<Page>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                bottomAppBarPropertyId
                value
                (fun element child -> (element :?> Page).BottomAppBar <- (child :?> AppBar))
                (fun element -> (element :?> Page).BottomAppBar :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member topAppBar(node: VirtualNode<Page>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                topAppBarPropertyId
                value
                (fun element child -> (element :?> Page).TopAppBar <- (child :?> AppBar))
                (fun element -> (element :?> Page).TopAppBar :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member navigationCacheMode(node: VirtualNode<Page>, value: NavigationCacheMode) =
            createClrProperty
                navigationCacheModePropertyId
                (fun uiElement _ -> (uiElement :?> Page).NavigationCacheMode <- value)
                (fun uiElement -> (uiElement :?> Page).NavigationCacheMode <- Unchecked.defaultof<NavigationCacheMode>)
                value
            |> node.set

    let page () =
        createNode<Page> pageNodeId
