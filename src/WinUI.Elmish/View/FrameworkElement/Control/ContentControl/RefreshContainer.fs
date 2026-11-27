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


[<AutoOpen>]
module RefreshContainerMod =

    let private refreshContainerNodeId = getNextId ()

    registerDefaultRenderer<RefreshContainer> refreshContainerNodeId

    let private pullDirectionPropertyId = getNextId ()
    let private visualizerPropertyId = getNextId ()

    [<Extension>]
    type RefreshContainerExtensions =

        [<Extension>]
        static member pullDirection(node: VirtualNode<RefreshContainer>, value: RefreshPullDirection) =
            createDependencyProperty pullDirectionPropertyId RefreshContainer.PullDirectionProperty value
            |> node.set

        [<Extension>]
        static member visualizer(node: VirtualNode<RefreshContainer>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                visualizerPropertyId
                value
                (fun element child -> (element :?> RefreshContainer).Visualizer <- (child :?> RefreshVisualizer))
                (fun element -> (element :?> RefreshContainer).Visualizer :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member onRefreshRequested(node: VirtualNode<RefreshContainer>, handler: RefreshContainer -> RefreshRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "RefreshRequested"
                Subscribe = (fun uiElement ->
                    let refreshContainer = uiElement :?> RefreshContainer
                    let h = TypedEventHandler<RefreshContainer, RefreshRequestedEventArgs>(fun _ e -> handler refreshContainer e)
                    refreshContainer.add_RefreshRequested h
                    { new IDisposable with
                        member _.Dispose() = refreshContainer.remove_RefreshRequested h }
                )
            })

    let refreshContainer () =
        createNode<RefreshContainer> refreshContainerNodeId
