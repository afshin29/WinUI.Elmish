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
module LayoutPanelMod =

    let private layoutPanelNodeId = getNextId ()

    registerDefaultRenderer<LayoutPanel> layoutPanelNodeId

    let private borderBrushPropertyId = getNextId ()
    let private borderThicknessPropertyId = getNextId ()
    let private cornerRadiusPropertyId = getNextId ()
    let private layoutPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()

    [<Extension>]
    type LayoutPanelExtensions =

        [<Extension>]
        static member borderBrush(node: VirtualNode<LayoutPanel>, value: Brush) =
            createDependencyProperty borderBrushPropertyId LayoutPanel.BorderBrushProperty value
            |> node.set

        [<Extension>]
        static member borderThickness(node: VirtualNode<LayoutPanel>, value: Thickness) =
            createDependencyProperty borderThicknessPropertyId LayoutPanel.BorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member cornerRadius(node: VirtualNode<LayoutPanel>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId LayoutPanel.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member layout(node: VirtualNode<LayoutPanel>, value: Layout) =
            createDependencyProperty layoutPropertyId LayoutPanel.LayoutProperty value
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<LayoutPanel>, value: Thickness) =
            createDependencyProperty paddingPropertyId LayoutPanel.PaddingProperty value
            |> node.set

    let layoutPanel () =
        createNode<LayoutPanel> layoutPanelNodeId
