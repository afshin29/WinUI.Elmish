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

// skipped dependency property InfoProviderProperty: no wrapper property and no usable attached-property accessors
// skipped dependency property StateProperty: read-only


[<AutoOpen>]
module RefreshVisualizerMod =

    let private refreshVisualizerNodeId = getNextId ()

    registerDefaultRenderer<RefreshVisualizer> refreshVisualizerNodeId

    let private contentPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()

    [<Extension>]
    type RefreshVisualizerExtensions =

        [<Extension>]
        static member content(node: VirtualNode<RefreshVisualizer>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                contentPropertyId
                value
                (fun element child -> (element :?> RefreshVisualizer).Content <- child)
                (fun element -> (element :?> RefreshVisualizer).Content)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<RefreshVisualizer>, value: RefreshVisualizerOrientation) =
            createDependencyProperty orientationPropertyId RefreshVisualizer.OrientationProperty value
            |> node.set

        [<Extension>]
        static member onRefreshRequested(node: VirtualNode<RefreshVisualizer>, handler: RefreshVisualizer -> RefreshRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "RefreshRequested"
                Subscribe = (fun uiElement ->
                    let refreshVisualizer = uiElement :?> RefreshVisualizer
                    let h = TypedEventHandler<RefreshVisualizer, RefreshRequestedEventArgs>(fun _ e -> handler refreshVisualizer e)
                    refreshVisualizer.add_RefreshRequested h
                    { new IDisposable with
                        member _.Dispose() = refreshVisualizer.remove_RefreshRequested h }
                )
            })

        [<Extension>]
        static member onRefreshStateChanged(node: VirtualNode<RefreshVisualizer>, handler: RefreshVisualizer -> RefreshStateChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "RefreshStateChanged"
                Subscribe = (fun uiElement ->
                    let refreshVisualizer = uiElement :?> RefreshVisualizer
                    let h = TypedEventHandler<RefreshVisualizer, RefreshStateChangedEventArgs>(fun _ e -> handler refreshVisualizer e)
                    refreshVisualizer.add_RefreshStateChanged h
                    { new IDisposable with
                        member _.Dispose() = refreshVisualizer.remove_RefreshStateChanged h }
                )
            })

    let refreshVisualizer () =
        createNode<RefreshVisualizer> refreshVisualizerNodeId
