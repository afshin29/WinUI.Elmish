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

// skipped property InkPresenter: read-only


[<AutoOpen>]
module InkCanvasMod =

    let private inkCanvasNodeId = getNextId ()

    registerDefaultRenderer<InkCanvas> inkCanvasNodeId

    let inkCanvas () =
        createNode<InkCanvas> inkCanvasNodeId
