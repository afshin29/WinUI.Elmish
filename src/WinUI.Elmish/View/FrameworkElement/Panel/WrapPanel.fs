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
module WrapPanelMod =

    let private wrapPanelNodeId = getNextId ()

    registerDefaultRenderer<WrapPanel> wrapPanelNodeId

    let private itemSpacingPropertyId = getNextId ()
    let private itemsStretchPropertyId = getNextId ()
    let private lineSpacingPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()

    [<Extension>]
    type WrapPanelExtensions =

        [<Extension>]
        static member itemSpacing(node: VirtualNode<WrapPanel>, value: float) =
            createDependencyProperty itemSpacingPropertyId WrapPanel.ItemSpacingProperty value
            |> node.set

        [<Extension>]
        static member itemsStretch(node: VirtualNode<WrapPanel>, value: WrapPanelItemsStretch) =
            createDependencyProperty itemsStretchPropertyId WrapPanel.ItemsStretchProperty value
            |> node.set

        [<Extension>]
        static member lineSpacing(node: VirtualNode<WrapPanel>, value: float) =
            createDependencyProperty lineSpacingPropertyId WrapPanel.LineSpacingProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<WrapPanel>, value: Orientation) =
            createDependencyProperty orientationPropertyId WrapPanel.OrientationProperty value
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<WrapPanel>, value: Thickness) =
            createDependencyProperty paddingPropertyId WrapPanel.PaddingProperty value
            |> node.set

    let wrapPanel () =
        createNode<WrapPanel> wrapPanelNodeId
