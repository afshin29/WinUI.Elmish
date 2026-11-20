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
module CanvasMod =

    let private canvasNodeId = getNextId ()

    registerDefaultRenderer<Canvas> canvasNodeId

    let private leftPropertyId = getNextId ()
    let private topPropertyId = getNextId ()
    let private zIndexPropertyId = getNextId ()

    [<Extension>]
    type CanvasExtensions =

        [<Extension>]
        static member canvas_left<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty leftPropertyId Canvas.LeftProperty value
            |> node.set

        [<Extension>]
        static member canvas_top<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty topPropertyId Canvas.TopProperty value
            |> node.set

        [<Extension>]
        static member canvas_zIndex<'m when 'm :> UIElement and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty zIndexPropertyId Canvas.ZIndexProperty value
            |> node.set

    let canvas () =
        createNode<Canvas> canvasNodeId
