namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property TemplateSettingsProperty: read-only


[<AutoOpen>]
module SplitViewMod =

    let private splitViewNodeId = getNextId ()

    registerDefaultRenderer<SplitView> splitViewNodeId

    let private compactPaneLengthPropertyId = getNextId ()
    let private contentPropertyId = getNextId ()
    let private displayModePropertyId = getNextId ()
    let private isPaneOpenPropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()
    let private openPaneLengthPropertyId = getNextId ()
    let private paneBackgroundPropertyId = getNextId ()
    let private panePlacementPropertyId = getNextId ()
    let private panePropertyId = getNextId ()

    [<Extension>]
    type SplitViewExtensions =

        [<Extension>]
        static member compactPaneLength(node: VirtualNode<SplitView>, value: float) =
            createDependencyProperty compactPaneLengthPropertyId SplitView.CompactPaneLengthProperty value
            |> node.set

        [<Extension>]
        static member content(node: VirtualNode<SplitView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                contentPropertyId
                value
                (fun element child -> (element :?> SplitView).Content <- child)
                (fun element -> (element :?> SplitView).Content)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member displayMode(node: VirtualNode<SplitView>, value: SplitViewDisplayMode) =
            createDependencyProperty displayModePropertyId SplitView.DisplayModeProperty value
            |> node.set

        [<Extension>]
        static member isPaneOpen(node: VirtualNode<SplitView>, value: bool) =
            createDependencyProperty isPaneOpenPropertyId SplitView.IsPaneOpenProperty value
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode(node: VirtualNode<SplitView>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId SplitView.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member openPaneLength(node: VirtualNode<SplitView>, value: float) =
            createDependencyProperty openPaneLengthPropertyId SplitView.OpenPaneLengthProperty value
            |> node.set

        [<Extension>]
        static member paneBackground(node: VirtualNode<SplitView>, value: Brush) =
            createDependencyProperty paneBackgroundPropertyId SplitView.PaneBackgroundProperty value
            |> node.set

        [<Extension>]
        static member panePlacement(node: VirtualNode<SplitView>, value: SplitViewPanePlacement) =
            createDependencyProperty panePlacementPropertyId SplitView.PanePlacementProperty value
            |> node.set

        [<Extension>]
        static member pane(node: VirtualNode<SplitView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                panePropertyId
                value
                (fun element child -> (element :?> SplitView).Pane <- child)
                (fun element -> (element :?> SplitView).Pane)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member onPaneClosed(node: VirtualNode<SplitView>, handler: SplitView -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneClosed"
                Subscribe = (fun uiElement ->
                    let splitView = uiElement :?> SplitView
                    let h = TypedEventHandler<SplitView, obj>(fun _ e -> handler splitView e)
                    splitView.add_PaneClosed h
                    { new IDisposable with
                        member _.Dispose() = splitView.remove_PaneClosed h }
                )
            })

        [<Extension>]
        static member onPaneClosing(node: VirtualNode<SplitView>, handler: SplitView -> SplitViewPaneClosingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneClosing"
                Subscribe = (fun uiElement ->
                    let splitView = uiElement :?> SplitView
                    let h = TypedEventHandler<SplitView, SplitViewPaneClosingEventArgs>(fun _ e -> handler splitView e)
                    splitView.add_PaneClosing h
                    { new IDisposable with
                        member _.Dispose() = splitView.remove_PaneClosing h }
                )
            })

        [<Extension>]
        static member onPaneOpened(node: VirtualNode<SplitView>, handler: SplitView -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneOpened"
                Subscribe = (fun uiElement ->
                    let splitView = uiElement :?> SplitView
                    let h = TypedEventHandler<SplitView, obj>(fun _ e -> handler splitView e)
                    splitView.add_PaneOpened h
                    { new IDisposable with
                        member _.Dispose() = splitView.remove_PaneOpened h }
                )
            })

        [<Extension>]
        static member onPaneOpening(node: VirtualNode<SplitView>, handler: SplitView -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneOpening"
                Subscribe = (fun uiElement ->
                    let splitView = uiElement :?> SplitView
                    let h = TypedEventHandler<SplitView, obj>(fun _ e -> handler splitView e)
                    splitView.add_PaneOpening h
                    { new IDisposable with
                        member _.Dispose() = splitView.remove_PaneOpening h }
                )
            })

    let splitView () =
        createNode<SplitView> splitViewNodeId
