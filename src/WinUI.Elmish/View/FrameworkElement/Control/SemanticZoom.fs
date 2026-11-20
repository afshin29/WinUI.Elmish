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


[<AutoOpen>]
module SemanticZoomMod =

    let private semanticZoomNodeId = getNextId ()

    registerDefaultRenderer<SemanticZoom> semanticZoomNodeId

    let private canChangeViewsPropertyId = getNextId ()
    let private isZoomOutButtonEnabledPropertyId = getNextId ()
    let private isZoomedInViewActivePropertyId = getNextId ()
    let private zoomedInViewPropertyId = getNextId ()
    let private zoomedOutViewPropertyId = getNextId ()

    [<Extension>]
    type SemanticZoomExtensions =

        [<Extension>]
        static member canChangeViews(node: VirtualNode<SemanticZoom>, value: bool) =
            createDependencyProperty canChangeViewsPropertyId SemanticZoom.CanChangeViewsProperty value
            |> node.set

        [<Extension>]
        static member isZoomOutButtonEnabled(node: VirtualNode<SemanticZoom>, value: bool) =
            createDependencyProperty isZoomOutButtonEnabledPropertyId SemanticZoom.IsZoomOutButtonEnabledProperty value
            |> node.set

        [<Extension>]
        static member isZoomedInViewActive(node: VirtualNode<SemanticZoom>, value: bool) =
            createDependencyProperty isZoomedInViewActivePropertyId SemanticZoom.IsZoomedInViewActiveProperty value
            |> node.set

        [<Extension>]
        static member zoomedInView(node: VirtualNode<SemanticZoom>, value: ISemanticZoomInformation) =
            createDependencyProperty zoomedInViewPropertyId SemanticZoom.ZoomedInViewProperty value
            |> node.set

        [<Extension>]
        static member zoomedOutView(node: VirtualNode<SemanticZoom>, value: ISemanticZoomInformation) =
            createDependencyProperty zoomedOutViewPropertyId SemanticZoom.ZoomedOutViewProperty value
            |> node.set

        [<Extension>]
        static member onViewChangeCompleted(node: VirtualNode<SemanticZoom>, handler: SemanticZoomViewChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ViewChangeCompleted"
                Subscribe = (fun uiElement ->
                    let semanticZoom = uiElement :?> SemanticZoom
                    let h = SemanticZoomViewChangedEventHandler(fun _ e -> handler e)
                    semanticZoom.ViewChangeCompleted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = semanticZoom.ViewChangeCompleted.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onViewChangeStarted(node: VirtualNode<SemanticZoom>, handler: SemanticZoomViewChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ViewChangeStarted"
                Subscribe = (fun uiElement ->
                    let semanticZoom = uiElement :?> SemanticZoom
                    let h = SemanticZoomViewChangedEventHandler(fun _ e -> handler e)
                    semanticZoom.ViewChangeStarted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = semanticZoom.ViewChangeStarted.RemoveHandler h }
                )
            })

    let semanticZoom () =
        createNode<SemanticZoom> semanticZoomNodeId
