namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module ToolTipMod =

    let private toolTipNodeId = getNextId ()

    registerDefaultRenderer<ToolTip> toolTipNodeId

    let private horizontalOffsetPropertyId = getNextId ()
    let private isOpenPropertyId = getNextId ()
    let private placementPropertyId = getNextId ()
    let private placementRectPropertyId = getNextId ()
    let private placementTargetPropertyId = getNextId ()
    let private verticalOffsetPropertyId = getNextId ()

    [<Extension>]
    type ToolTipExtensions =

        [<Extension>]
        static member horizontalOffset(node: VirtualNode<ToolTip>, value: float) =
            createDependencyProperty horizontalOffsetPropertyId ToolTip.HorizontalOffsetProperty value
            |> node.set

        [<Extension>]
        static member isOpen(node: VirtualNode<ToolTip>, value: bool) =
            createDependencyProperty isOpenPropertyId ToolTip.IsOpenProperty value
            |> node.set

        [<Extension>]
        static member placement(node: VirtualNode<ToolTip>, value: PlacementMode) =
            createDependencyProperty placementPropertyId ToolTip.PlacementProperty value
            |> node.set

        [<Extension>]
        static member placementRect(node: VirtualNode<ToolTip>, value: Nullable<Rect>) =
            createDependencyProperty placementRectPropertyId ToolTip.PlacementRectProperty value
            |> node.set

        [<Extension>]
        static member placementTarget(node: VirtualNode<ToolTip>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                placementTargetPropertyId
                value
                (fun element child -> (element :?> ToolTip).PlacementTarget <- child)
                (fun element -> (element :?> ToolTip).PlacementTarget)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member verticalOffset(node: VirtualNode<ToolTip>, value: float) =
            createDependencyProperty verticalOffsetPropertyId ToolTip.VerticalOffsetProperty value
            |> node.set

        [<Extension>]
        static member onClosed(node: VirtualNode<ToolTip>, handler: ToolTip -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closed"
                Subscribe = (fun uiElement ->
                    let toolTip = uiElement :?> ToolTip
                    let h = RoutedEventHandler(fun _ e -> handler toolTip e)
                    toolTip.Closed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = toolTip.Closed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onOpened(node: VirtualNode<ToolTip>, handler: ToolTip -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opened"
                Subscribe = (fun uiElement ->
                    let toolTip = uiElement :?> ToolTip
                    let h = RoutedEventHandler(fun _ e -> handler toolTip e)
                    toolTip.Opened.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = toolTip.Opened.RemoveHandler h }
                )
            })

    let toolTip () =
        createNode<ToolTip> toolTipNodeId
