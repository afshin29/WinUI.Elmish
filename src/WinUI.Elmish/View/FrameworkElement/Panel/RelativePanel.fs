namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module RelativePanelMod =

    let private relativePanelNodeId = getNextId ()

    registerDefaultRenderer<RelativePanel> relativePanelNodeId

    let private abovePropertyId = getNextId ()
    let private alignBottomWithPanelPropertyId = getNextId ()
    let private alignBottomWithPropertyId = getNextId ()
    let private alignHorizontalCenterWithPanelPropertyId = getNextId ()
    let private alignHorizontalCenterWithPropertyId = getNextId ()
    let private alignLeftWithPanelPropertyId = getNextId ()
    let private alignLeftWithPropertyId = getNextId ()
    let private alignRightWithPanelPropertyId = getNextId ()
    let private alignRightWithPropertyId = getNextId ()
    let private alignTopWithPanelPropertyId = getNextId ()
    let private alignTopWithPropertyId = getNextId ()
    let private alignVerticalCenterWithPanelPropertyId = getNextId ()
    let private alignVerticalCenterWithPropertyId = getNextId ()
    let private backgroundSizingPropertyId = getNextId ()
    let private belowPropertyId = getNextId ()
    let private borderBrushPropertyId = getNextId ()
    let private borderThicknessPropertyId = getNextId ()
    let private cornerRadiusPropertyId = getNextId ()
    let private leftOfPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private rightOfPropertyId = getNextId ()

    [<Extension>]
    type RelativePanelExtensions =

        [<Extension>]
        static member relativePanel_above<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty abovePropertyId RelativePanel.AboveProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignBottomWithPanel<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty alignBottomWithPanelPropertyId RelativePanel.AlignBottomWithPanelProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignBottomWith<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty alignBottomWithPropertyId RelativePanel.AlignBottomWithProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignHorizontalCenterWithPanel<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty alignHorizontalCenterWithPanelPropertyId RelativePanel.AlignHorizontalCenterWithPanelProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignHorizontalCenterWith<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty alignHorizontalCenterWithPropertyId RelativePanel.AlignHorizontalCenterWithProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignLeftWithPanel<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty alignLeftWithPanelPropertyId RelativePanel.AlignLeftWithPanelProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignLeftWith<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty alignLeftWithPropertyId RelativePanel.AlignLeftWithProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignRightWithPanel<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty alignRightWithPanelPropertyId RelativePanel.AlignRightWithPanelProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignRightWith<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty alignRightWithPropertyId RelativePanel.AlignRightWithProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignTopWithPanel<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty alignTopWithPanelPropertyId RelativePanel.AlignTopWithPanelProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignTopWith<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty alignTopWithPropertyId RelativePanel.AlignTopWithProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignVerticalCenterWithPanel<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty alignVerticalCenterWithPanelPropertyId RelativePanel.AlignVerticalCenterWithPanelProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_alignVerticalCenterWith<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty alignVerticalCenterWithPropertyId RelativePanel.AlignVerticalCenterWithProperty value
            |> node.set

        [<Extension>]
        static member backgroundSizing(node: VirtualNode<RelativePanel>, value: BackgroundSizing) =
            createDependencyProperty backgroundSizingPropertyId RelativePanel.BackgroundSizingProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_below<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty belowPropertyId RelativePanel.BelowProperty value
            |> node.set

        [<Extension>]
        static member borderBrush(node: VirtualNode<RelativePanel>, value: Brush) =
            createDependencyProperty borderBrushPropertyId RelativePanel.BorderBrushProperty value
            |> node.set

        [<Extension>]
        static member borderThickness(node: VirtualNode<RelativePanel>, value: Thickness) =
            createDependencyProperty borderThicknessPropertyId RelativePanel.BorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member cornerRadius(node: VirtualNode<RelativePanel>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId RelativePanel.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_leftOf<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty leftOfPropertyId RelativePanel.LeftOfProperty value
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<RelativePanel>, value: Thickness) =
            createDependencyProperty paddingPropertyId RelativePanel.PaddingProperty value
            |> node.set

        [<Extension>]
        static member relativePanel_rightOf<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty rightOfPropertyId RelativePanel.RightOfProperty value
            |> node.set

    let relativePanel () =
        createNode<RelativePanel> relativePanelNodeId
