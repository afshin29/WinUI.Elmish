namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.Web.WebView2.Core
open System
open System.Runtime.CompilerServices
open Windows.Foundation
open Windows.UI

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property CoreWebView2: read-only


[<AutoOpen>]
module WebView2Mod =

    let private webView2NodeId = getNextId ()

    registerDefaultRenderer<WebView2> webView2NodeId

    let private canGoBackPropertyId = getNextId ()
    let private canGoForwardPropertyId = getNextId ()
    let private defaultBackgroundColorPropertyId = getNextId ()
    let private sourcePropertyId = getNextId ()

    [<Extension>]
    type WebView2Extensions =

        [<Extension>]
        static member canGoBack(node: VirtualNode<WebView2>, value: bool) =
            createDependencyProperty canGoBackPropertyId WebView2.CanGoBackProperty value
            |> node.set

        [<Extension>]
        static member canGoForward(node: VirtualNode<WebView2>, value: bool) =
            createDependencyProperty canGoForwardPropertyId WebView2.CanGoForwardProperty value
            |> node.set

        [<Extension>]
        static member defaultBackgroundColor(node: VirtualNode<WebView2>, value: Color) =
            createDependencyProperty defaultBackgroundColorPropertyId WebView2.DefaultBackgroundColorProperty value
            |> node.set

        [<Extension>]
        static member source(node: VirtualNode<WebView2>, value: Uri) =
            createDependencyProperty sourcePropertyId WebView2.SourceProperty value
            |> node.set

        [<Extension>]
        static member onCoreProcessFailed(node: VirtualNode<WebView2>, handler: WebView2 -> CoreWebView2ProcessFailedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CoreProcessFailed"
                Subscribe = (fun uiElement ->
                    let webView2 = uiElement :?> WebView2
                    let h = TypedEventHandler<WebView2, CoreWebView2ProcessFailedEventArgs>(fun _ e -> handler webView2 e)
                    webView2.add_CoreProcessFailed h
                    { new IDisposable with
                        member _.Dispose() = webView2.remove_CoreProcessFailed h }
                )
            })

        [<Extension>]
        static member onCoreWebView2Initialized(node: VirtualNode<WebView2>, handler: WebView2 -> CoreWebView2InitializedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CoreWebView2Initialized"
                Subscribe = (fun uiElement ->
                    let webView2 = uiElement :?> WebView2
                    let h = TypedEventHandler<WebView2, CoreWebView2InitializedEventArgs>(fun _ e -> handler webView2 e)
                    webView2.add_CoreWebView2Initialized h
                    { new IDisposable with
                        member _.Dispose() = webView2.remove_CoreWebView2Initialized h }
                )
            })

        [<Extension>]
        static member onNavigationCompleted(node: VirtualNode<WebView2>, handler: WebView2 -> CoreWebView2NavigationCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "NavigationCompleted"
                Subscribe = (fun uiElement ->
                    let webView2 = uiElement :?> WebView2
                    let h = TypedEventHandler<WebView2, CoreWebView2NavigationCompletedEventArgs>(fun _ e -> handler webView2 e)
                    webView2.add_NavigationCompleted h
                    { new IDisposable with
                        member _.Dispose() = webView2.remove_NavigationCompleted h }
                )
            })

        [<Extension>]
        static member onNavigationStarting(node: VirtualNode<WebView2>, handler: WebView2 -> CoreWebView2NavigationStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "NavigationStarting"
                Subscribe = (fun uiElement ->
                    let webView2 = uiElement :?> WebView2
                    let h = TypedEventHandler<WebView2, CoreWebView2NavigationStartingEventArgs>(fun _ e -> handler webView2 e)
                    webView2.add_NavigationStarting h
                    { new IDisposable with
                        member _.Dispose() = webView2.remove_NavigationStarting h }
                )
            })

        [<Extension>]
        static member onWebMessageReceived(node: VirtualNode<WebView2>, handler: WebView2 -> CoreWebView2WebMessageReceivedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "WebMessageReceived"
                Subscribe = (fun uiElement ->
                    let webView2 = uiElement :?> WebView2
                    let h = TypedEventHandler<WebView2, CoreWebView2WebMessageReceivedEventArgs>(fun _ e -> handler webView2 e)
                    webView2.add_WebMessageReceived h
                    { new IDisposable with
                        member _.Dispose() = webView2.remove_WebMessageReceived h }
                )
            })

    let webView2 () =
        createNode<WebView2> webView2NodeId
