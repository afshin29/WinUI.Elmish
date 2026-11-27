namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module ScrollBarMod =

    let private scrollBarNodeId = getNextId ()

    registerDefaultRenderer<ScrollBar> scrollBarNodeId

    let private indicatorModePropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private viewportSizePropertyId = getNextId ()

    [<Extension>]
    type ScrollBarExtensions =

        [<Extension>]
        static member indicatorMode(node: VirtualNode<ScrollBar>, value: ScrollingIndicatorMode) =
            createDependencyProperty indicatorModePropertyId ScrollBar.IndicatorModeProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<ScrollBar>, value: Orientation) =
            createDependencyProperty orientationPropertyId ScrollBar.OrientationProperty value
            |> node.set

        [<Extension>]
        static member viewportSize(node: VirtualNode<ScrollBar>, value: float) =
            createDependencyProperty viewportSizePropertyId ScrollBar.ViewportSizeProperty value
            |> node.set

        [<Extension>]
        static member onScroll(node: VirtualNode<ScrollBar>, handler: ScrollBar -> ScrollEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Scroll"
                Subscribe = (fun uiElement ->
                    let scrollBar = uiElement :?> ScrollBar
                    let h = ScrollEventHandler(fun _ e -> handler scrollBar e)
                    scrollBar.Scroll.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = scrollBar.Scroll.RemoveHandler h }
                )
            })

    let scrollBar () =
        createNode<ScrollBar> scrollBarNodeId
