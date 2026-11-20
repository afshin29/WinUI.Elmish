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
module ParallaxViewMod =

    let private parallaxViewNodeId = getNextId ()

    registerDefaultRenderer<ParallaxView> parallaxViewNodeId

    let private childPropertyId = getNextId ()
    let private horizontalShiftPropertyId = getNextId ()
    let private horizontalSourceEndOffsetPropertyId = getNextId ()
    let private horizontalSourceOffsetKindPropertyId = getNextId ()
    let private horizontalSourceStartOffsetPropertyId = getNextId ()
    let private isHorizontalShiftClampedPropertyId = getNextId ()
    let private isVerticalShiftClampedPropertyId = getNextId ()
    let private maxHorizontalShiftRatioPropertyId = getNextId ()
    let private maxVerticalShiftRatioPropertyId = getNextId ()
    let private sourcePropertyId = getNextId ()
    let private verticalShiftPropertyId = getNextId ()
    let private verticalSourceEndOffsetPropertyId = getNextId ()
    let private verticalSourceOffsetKindPropertyId = getNextId ()
    let private verticalSourceStartOffsetPropertyId = getNextId ()

    [<Extension>]
    type ParallaxViewExtensions =

        [<Extension>]
        static member child(node: VirtualNode<ParallaxView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                childPropertyId
                value
                (fun element child -> (element :?> ParallaxView).Child <- child)
                (fun element -> (element :?> ParallaxView).Child)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member horizontalShift(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty horizontalShiftPropertyId ParallaxView.HorizontalShiftProperty value
            |> node.set

        [<Extension>]
        static member horizontalSourceEndOffset(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty horizontalSourceEndOffsetPropertyId ParallaxView.HorizontalSourceEndOffsetProperty value
            |> node.set

        [<Extension>]
        static member horizontalSourceOffsetKind(node: VirtualNode<ParallaxView>, value: ParallaxSourceOffsetKind) =
            createDependencyProperty horizontalSourceOffsetKindPropertyId ParallaxView.HorizontalSourceOffsetKindProperty value
            |> node.set

        [<Extension>]
        static member horizontalSourceStartOffset(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty horizontalSourceStartOffsetPropertyId ParallaxView.HorizontalSourceStartOffsetProperty value
            |> node.set

        [<Extension>]
        static member isHorizontalShiftClamped(node: VirtualNode<ParallaxView>, value: bool) =
            createDependencyProperty isHorizontalShiftClampedPropertyId ParallaxView.IsHorizontalShiftClampedProperty value
            |> node.set

        [<Extension>]
        static member isVerticalShiftClamped(node: VirtualNode<ParallaxView>, value: bool) =
            createDependencyProperty isVerticalShiftClampedPropertyId ParallaxView.IsVerticalShiftClampedProperty value
            |> node.set

        [<Extension>]
        static member maxHorizontalShiftRatio(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty maxHorizontalShiftRatioPropertyId ParallaxView.MaxHorizontalShiftRatioProperty value
            |> node.set

        [<Extension>]
        static member maxVerticalShiftRatio(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty maxVerticalShiftRatioPropertyId ParallaxView.MaxVerticalShiftRatioProperty value
            |> node.set

        [<Extension>]
        static member source(node: VirtualNode<ParallaxView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                sourcePropertyId
                value
                (fun element child -> (element :?> ParallaxView).Source <- child)
                (fun element -> (element :?> ParallaxView).Source)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member verticalShift(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty verticalShiftPropertyId ParallaxView.VerticalShiftProperty value
            |> node.set

        [<Extension>]
        static member verticalSourceEndOffset(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty verticalSourceEndOffsetPropertyId ParallaxView.VerticalSourceEndOffsetProperty value
            |> node.set

        [<Extension>]
        static member verticalSourceOffsetKind(node: VirtualNode<ParallaxView>, value: ParallaxSourceOffsetKind) =
            createDependencyProperty verticalSourceOffsetKindPropertyId ParallaxView.VerticalSourceOffsetKindProperty value
            |> node.set

        [<Extension>]
        static member verticalSourceStartOffset(node: VirtualNode<ParallaxView>, value: float) =
            createDependencyProperty verticalSourceStartOffsetPropertyId ParallaxView.VerticalSourceStartOffsetProperty value
            |> node.set

    let parallaxView () =
        createNode<ParallaxView> parallaxViewNodeId
