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

// skipped property AreHorizontalSnapPointsRegular: read-only
// skipped property AreVerticalSnapPointsRegular: read-only


[<AutoOpen>]
module StackPanelMod =

    let private stackPanelNodeId = getNextId ()

    registerDefaultRenderer<StackPanel> stackPanelNodeId

    let private areScrollSnapPointsRegularPropertyId = getNextId ()
    let private backgroundSizingPropertyId = getNextId ()
    let private borderBrushPropertyId = getNextId ()
    let private borderThicknessPropertyId = getNextId ()
    let private cornerRadiusPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private spacingPropertyId = getNextId ()

    [<Extension>]
    type StackPanelExtensions =

        [<Extension>]
        static member areScrollSnapPointsRegular(node: VirtualNode<StackPanel>, value: bool) =
            createDependencyProperty areScrollSnapPointsRegularPropertyId StackPanel.AreScrollSnapPointsRegularProperty value
            |> node.set

        [<Extension>]
        static member backgroundSizing(node: VirtualNode<StackPanel>, value: BackgroundSizing) =
            createDependencyProperty backgroundSizingPropertyId StackPanel.BackgroundSizingProperty value
            |> node.set

        [<Extension>]
        static member borderBrush(node: VirtualNode<StackPanel>, value: Brush) =
            createDependencyProperty borderBrushPropertyId StackPanel.BorderBrushProperty value
            |> node.set

        [<Extension>]
        static member borderThickness(node: VirtualNode<StackPanel>, value: Thickness) =
            createDependencyProperty borderThicknessPropertyId StackPanel.BorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member cornerRadius(node: VirtualNode<StackPanel>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId StackPanel.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<StackPanel>, value: Orientation) =
            createDependencyProperty orientationPropertyId StackPanel.OrientationProperty value
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<StackPanel>, value: Thickness) =
            createDependencyProperty paddingPropertyId StackPanel.PaddingProperty value
            |> node.set

        [<Extension>]
        static member spacing(node: VirtualNode<StackPanel>, value: float) =
            createDependencyProperty spacingPropertyId StackPanel.SpacingProperty value
            |> node.set

        [<Extension>]
        static member onHorizontalSnapPointsChanged(node: VirtualNode<StackPanel>, handler: StackPanel -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "HorizontalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let stackPanel = uiElement :?> StackPanel
                    let h = EventHandler<obj>(fun _ e -> handler stackPanel e)
                    stackPanel.HorizontalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = stackPanel.HorizontalSnapPointsChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onVerticalSnapPointsChanged(node: VirtualNode<StackPanel>, handler: StackPanel -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "VerticalSnapPointsChanged"
                Subscribe = (fun uiElement ->
                    let stackPanel = uiElement :?> StackPanel
                    let h = EventHandler<obj>(fun _ e -> handler stackPanel e)
                    stackPanel.VerticalSnapPointsChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = stackPanel.VerticalSnapPointsChanged.RemoveHandler h }
                )
            })

    let stackPanel () =
        createNode<StackPanel> stackPanelNodeId

    let hstack () =
        stackPanel()
            .orientation(Orientation.Horizontal)

    let vstack () =
        stackPanel()
            .orientation(Orientation.Vertical)
