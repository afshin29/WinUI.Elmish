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
module InkToolbarBallpointPenButtonMod =

    let private inkToolbarBallpointPenButtonNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarBallpointPenButton> inkToolbarBallpointPenButtonNodeId

    let inkToolbarBallpointPenButton () =
        createNode<InkToolbarBallpointPenButton> inkToolbarBallpointPenButtonNodeId
