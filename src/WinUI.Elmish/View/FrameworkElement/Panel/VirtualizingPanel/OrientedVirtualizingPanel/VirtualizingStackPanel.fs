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

// skipped dependency property IsVirtualizingProperty: no wrapper property and no usable attached-property accessors


[<AutoOpen>]
module VirtualizingStackPanelMod =

    let private virtualizingStackPanelNodeId = getNextId ()

    registerDefaultRenderer<VirtualizingStackPanel> virtualizingStackPanelNodeId

    let private areScrollSnapPointsRegularPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private virtualizationModePropertyId = getNextId ()

    [<Extension>]
    type VirtualizingStackPanelExtensions =

        [<Extension>]
        static member areScrollSnapPointsRegular(node: VirtualNode<VirtualizingStackPanel>, value: bool) =
            createDependencyProperty areScrollSnapPointsRegularPropertyId VirtualizingStackPanel.AreScrollSnapPointsRegularProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<VirtualizingStackPanel>, value: Orientation) =
            createDependencyProperty orientationPropertyId VirtualizingStackPanel.OrientationProperty value
            |> node.set

        [<Extension>]
        static member virtualizingStackPanel_virtualizationMode<'m when 'm :> DependencyObject and 'm: not struct>
            (node: VirtualNode<'m>, value: VirtualizationMode) =
            createDependencyProperty virtualizationModePropertyId VirtualizingStackPanel.VirtualizationModeProperty value
            |> node.set

        [<Extension>]
        static member onCleanUpVirtualizedItemEvent(node: VirtualNode<VirtualizingStackPanel>, handler: CleanUpVirtualizedItemEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CleanUpVirtualizedItemEvent"
                Subscribe = (fun uiElement ->
                    let virtualizingStackPanel = uiElement :?> VirtualizingStackPanel
                    let h = CleanUpVirtualizedItemEventHandler(fun _ e -> handler e)
                    virtualizingStackPanel.CleanUpVirtualizedItemEvent.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = virtualizingStackPanel.CleanUpVirtualizedItemEvent.RemoveHandler h }
                )
            })

    let virtualizingStackPanel () =
        createNode<VirtualizingStackPanel> virtualizingStackPanelNodeId
