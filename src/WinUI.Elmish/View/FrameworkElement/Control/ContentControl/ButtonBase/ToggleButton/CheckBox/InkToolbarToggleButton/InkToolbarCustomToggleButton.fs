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
module InkToolbarCustomToggleButtonMod =

    let private inkToolbarCustomToggleButtonNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarCustomToggleButton> inkToolbarCustomToggleButtonNodeId

    let inkToolbarCustomToggleButton () =
        createNode<InkToolbarCustomToggleButton> inkToolbarCustomToggleButtonNodeId
