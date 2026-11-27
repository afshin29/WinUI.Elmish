namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
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
module TitleBarMod =

    let private titleBarNodeId = getNextId ()

    registerDefaultRenderer<TitleBar> titleBarNodeId

    let private autoRefreshDragRegionsPropertyId = getNextId ()
    let private contentPropertyId = getNextId ()
    let private iconSourcePropertyId = getNextId ()
    let private isBackButtonEnabledPropertyId = getNextId ()
    let private isBackButtonVisiblePropertyId = getNextId ()
    let private isDragRegionPropertyId = getNextId ()
    let private isPaneToggleButtonVisiblePropertyId = getNextId ()
    let private leftHeaderPropertyId = getNextId ()
    let private rightHeaderPropertyId = getNextId ()
    let private subtitlePropertyId = getNextId ()
    let private titlePropertyId = getNextId ()

    [<Extension>]
    type TitleBarExtensions =

        [<Extension>]
        static member autoRefreshDragRegions(node: VirtualNode<TitleBar>, value: bool) =
            createDependencyProperty autoRefreshDragRegionsPropertyId TitleBar.AutoRefreshDragRegionsProperty value
            |> node.set

        [<Extension>]
        static member content(node: VirtualNode<TitleBar>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                contentPropertyId
                value
                (fun element child -> (element :?> TitleBar).Content <- child)
                (fun element -> (element :?> TitleBar).Content)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member iconSource(node: VirtualNode<TitleBar>, value: IconSource) =
            createDependencyProperty iconSourcePropertyId TitleBar.IconSourceProperty value
            |> node.set

        [<Extension>]
        static member isBackButtonEnabled(node: VirtualNode<TitleBar>, value: bool) =
            createDependencyProperty isBackButtonEnabledPropertyId TitleBar.IsBackButtonEnabledProperty value
            |> node.set

        [<Extension>]
        static member isBackButtonVisible(node: VirtualNode<TitleBar>, value: bool) =
            createDependencyProperty isBackButtonVisiblePropertyId TitleBar.IsBackButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member titleBar_isDragRegion<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Nullable<bool>) =
            createDependencyProperty isDragRegionPropertyId TitleBar.IsDragRegionProperty value
            |> node.set

        [<Extension>]
        static member isPaneToggleButtonVisible(node: VirtualNode<TitleBar>, value: bool) =
            createDependencyProperty isPaneToggleButtonVisiblePropertyId TitleBar.IsPaneToggleButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member leftHeader(node: VirtualNode<TitleBar>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                leftHeaderPropertyId
                value
                (fun element child -> (element :?> TitleBar).LeftHeader <- child)
                (fun element -> (element :?> TitleBar).LeftHeader)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member rightHeader(node: VirtualNode<TitleBar>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                rightHeaderPropertyId
                value
                (fun element child -> (element :?> TitleBar).RightHeader <- child)
                (fun element -> (element :?> TitleBar).RightHeader)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member subtitle(node: VirtualNode<TitleBar>, value: string) =
            createDependencyProperty subtitlePropertyId TitleBar.SubtitleProperty value
            |> node.set

        [<Extension>]
        static member title(node: VirtualNode<TitleBar>, value: string) =
            createDependencyProperty titlePropertyId TitleBar.TitleProperty value
            |> node.set

        [<Extension>]
        static member onBackRequested(node: VirtualNode<TitleBar>, handler: TitleBar -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "BackRequested"
                Subscribe = (fun uiElement ->
                    let titleBar = uiElement :?> TitleBar
                    let h = TypedEventHandler<TitleBar, obj>(fun _ e -> handler titleBar e)
                    titleBar.add_BackRequested h
                    { new IDisposable with
                        member _.Dispose() = titleBar.remove_BackRequested h }
                )
            })

        [<Extension>]
        static member onPaneToggleRequested(node: VirtualNode<TitleBar>, handler: TitleBar -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneToggleRequested"
                Subscribe = (fun uiElement ->
                    let titleBar = uiElement :?> TitleBar
                    let h = TypedEventHandler<TitleBar, obj>(fun _ e -> handler titleBar e)
                    titleBar.add_PaneToggleRequested h
                    { new IDisposable with
                        member _.Dispose() = titleBar.remove_PaneToggleRequested h }
                )
            })

    let titleBar () =
        createNode<TitleBar> titleBarNodeId
