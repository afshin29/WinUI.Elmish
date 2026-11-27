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

// skipped dependency property IsDraggingProperty: read-only


[<AutoOpen>]
module ThumbMod =

    let private thumbNodeId = getNextId ()

    registerDefaultRenderer<Thumb> thumbNodeId

    [<Extension>]
    type ThumbExtensions =

        [<Extension>]
        static member onDragCompleted(node: VirtualNode<Thumb>, handler: Thumb -> DragCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragCompleted"
                Subscribe = (fun uiElement ->
                    let thumb = uiElement :?> Thumb
                    let h = DragCompletedEventHandler(fun _ e -> handler thumb e)
                    thumb.DragCompleted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = thumb.DragCompleted.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDragDelta(node: VirtualNode<Thumb>, handler: Thumb -> DragDeltaEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragDelta"
                Subscribe = (fun uiElement ->
                    let thumb = uiElement :?> Thumb
                    let h = DragDeltaEventHandler(fun _ e -> handler thumb e)
                    thumb.DragDelta.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = thumb.DragDelta.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDragStarted(node: VirtualNode<Thumb>, handler: Thumb -> DragStartedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragStarted"
                Subscribe = (fun uiElement ->
                    let thumb = uiElement :?> Thumb
                    let h = DragStartedEventHandler(fun _ e -> handler thumb e)
                    thumb.DragStarted.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = thumb.DragStarted.RemoveHandler h }
                )
            })

    let thumb () =
        createNode<Thumb> thumbNodeId
