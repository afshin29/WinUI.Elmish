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

// skipped dependency property CompositionScaleXProperty: read-only
// skipped dependency property CompositionScaleYProperty: read-only


[<AutoOpen>]
module SwapChainPanelMod =

    let private swapChainPanelNodeId = getNextId ()

    registerDefaultRenderer<SwapChainPanel> swapChainPanelNodeId

    [<Extension>]
    type SwapChainPanelExtensions =

        [<Extension>]
        static member onCompositionScaleChanged(node: VirtualNode<SwapChainPanel>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CompositionScaleChanged"
                Subscribe = (fun uiElement ->
                    let swapChainPanel = uiElement :?> SwapChainPanel
                    let h = TypedEventHandler<SwapChainPanel, obj>(fun _ e -> handler e)
                    swapChainPanel.add_CompositionScaleChanged h
                    { new IDisposable with
                        member _.Dispose() = swapChainPanel.remove_CompositionScaleChanged h }
                )
            })

    let swapChainPanel () =
        createNode<SwapChainPanel> swapChainPanelNodeId
