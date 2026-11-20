namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Navigation
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property BackStackDepthProperty: read-only
// skipped dependency property BackStackProperty: read-only
// skipped dependency property CanGoBackProperty: read-only
// skipped dependency property CanGoForwardProperty: read-only
// skipped dependency property CurrentSourcePageTypeProperty: read-only
// skipped dependency property ForwardStackProperty: read-only


[<AutoOpen>]
module FrameMod =

    let private frameNodeId = getNextId ()

    registerDefaultRenderer<Frame> frameNodeId

    let private cacheSizePropertyId = getNextId ()
    let private isNavigationStackEnabledPropertyId = getNextId ()
    let private sourcePageTypePropertyId = getNextId ()

    [<Extension>]
    type FrameExtensions =

        [<Extension>]
        static member cacheSize(node: VirtualNode<Frame>, value: int) =
            createDependencyProperty cacheSizePropertyId Frame.CacheSizeProperty value
            |> node.set

        [<Extension>]
        static member isNavigationStackEnabled(node: VirtualNode<Frame>, value: bool) =
            createDependencyProperty isNavigationStackEnabledPropertyId Frame.IsNavigationStackEnabledProperty value
            |> node.set

        [<Extension>]
        static member sourcePageType(node: VirtualNode<Frame>, value: Type) =
            createDependencyProperty sourcePageTypePropertyId Frame.SourcePageTypeProperty value
            |> node.set

        [<Extension>]
        static member onNavigated(node: VirtualNode<Frame>, handler: NavigationEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Navigated"
                Subscribe = (fun uiElement ->
                    let frame = uiElement :?> Frame
                    let h = NavigatedEventHandler(fun _ e -> handler e)
                    frame.Navigated.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frame.Navigated.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onNavigating(node: VirtualNode<Frame>, handler: NavigatingCancelEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Navigating"
                Subscribe = (fun uiElement ->
                    let frame = uiElement :?> Frame
                    let h = NavigatingCancelEventHandler(fun _ e -> handler e)
                    frame.Navigating.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frame.Navigating.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onNavigationFailed(node: VirtualNode<Frame>, handler: NavigationFailedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "NavigationFailed"
                Subscribe = (fun uiElement ->
                    let frame = uiElement :?> Frame
                    let h = NavigationFailedEventHandler(fun _ e -> handler e)
                    frame.NavigationFailed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frame.NavigationFailed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onNavigationStopped(node: VirtualNode<Frame>, handler: NavigationEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "NavigationStopped"
                Subscribe = (fun uiElement ->
                    let frame = uiElement :?> Frame
                    let h = NavigationStoppedEventHandler(fun _ e -> handler e)
                    frame.NavigationStopped.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = frame.NavigationStopped.RemoveHandler h }
                )
            })

    let frame () =
        createNode<Frame> frameNodeId
