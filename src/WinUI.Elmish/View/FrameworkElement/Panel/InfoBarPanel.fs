namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module InfoBarPanelMod =

    let private infoBarPanelNodeId = getNextId ()

    registerDefaultRenderer<InfoBarPanel> infoBarPanelNodeId

    let private horizontalOrientationMarginPropertyId = getNextId ()
    let private horizontalOrientationPaddingPropertyId = getNextId ()
    let private verticalOrientationMarginPropertyId = getNextId ()
    let private verticalOrientationPaddingPropertyId = getNextId ()

    [<Extension>]
    type InfoBarPanelExtensions =

        [<Extension>]
        static member infoBarPanel_horizontalOrientationMargin<'m when 'm :> DependencyObject and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty horizontalOrientationMarginPropertyId InfoBarPanel.HorizontalOrientationMarginProperty value
            |> node.set

        [<Extension>]
        static member horizontalOrientationPadding(node: VirtualNode<InfoBarPanel>, value: Thickness) =
            createDependencyProperty horizontalOrientationPaddingPropertyId InfoBarPanel.HorizontalOrientationPaddingProperty value
            |> node.set

        [<Extension>]
        static member infoBarPanel_verticalOrientationMargin<'m when 'm :> DependencyObject and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty verticalOrientationMarginPropertyId InfoBarPanel.VerticalOrientationMarginProperty value
            |> node.set

        [<Extension>]
        static member verticalOrientationPadding(node: VirtualNode<InfoBarPanel>, value: Thickness) =
            createDependencyProperty verticalOrientationPaddingPropertyId InfoBarPanel.VerticalOrientationPaddingProperty value
            |> node.set

    let infoBarPanel () =
        createNode<InfoBarPanel> infoBarPanelNodeId
