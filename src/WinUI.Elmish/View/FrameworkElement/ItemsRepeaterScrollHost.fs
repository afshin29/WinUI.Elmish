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

// skipped property CurrentAnchor: read-only


[<AutoOpen>]
module ItemsRepeaterScrollHostMod =

    let private itemsRepeaterScrollHostNodeId = getNextId ()

    registerDefaultRenderer<ItemsRepeaterScrollHost> itemsRepeaterScrollHostNodeId

    let private horizontalAnchorRatioPropertyId = getNextId ()
    let private scrollViewerPropertyId = getNextId ()
    let private verticalAnchorRatioPropertyId = getNextId ()

    [<Extension>]
    type ItemsRepeaterScrollHostExtensions =

        [<Extension>]
        static member horizontalAnchorRatio(node: VirtualNode<ItemsRepeaterScrollHost>, value: float) =
            createClrProperty
                horizontalAnchorRatioPropertyId
                (fun uiElement _ -> (uiElement :?> ItemsRepeaterScrollHost).HorizontalAnchorRatio <- value)
                (fun uiElement -> (uiElement :?> ItemsRepeaterScrollHost).HorizontalAnchorRatio <- Unchecked.defaultof<float>)
                value
            |> node.set

        [<Extension>]
        static member scrollViewer(node: VirtualNode<ItemsRepeaterScrollHost>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                scrollViewerPropertyId
                value
                (fun element child -> (element :?> ItemsRepeaterScrollHost).ScrollViewer <- (child :?> ScrollViewer))
                (fun element -> (element :?> ItemsRepeaterScrollHost).ScrollViewer :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member verticalAnchorRatio(node: VirtualNode<ItemsRepeaterScrollHost>, value: float) =
            createClrProperty
                verticalAnchorRatioPropertyId
                (fun uiElement _ -> (uiElement :?> ItemsRepeaterScrollHost).VerticalAnchorRatio <- value)
                (fun uiElement -> (uiElement :?> ItemsRepeaterScrollHost).VerticalAnchorRatio <- Unchecked.defaultof<float>)
                value
            |> node.set

    let itemsRepeaterScrollHost () =
        createNode<ItemsRepeaterScrollHost> itemsRepeaterScrollHostNodeId
