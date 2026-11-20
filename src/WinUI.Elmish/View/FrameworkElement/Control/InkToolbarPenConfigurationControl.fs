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

// skipped dependency property PenButtonProperty: read-only


[<AutoOpen>]
module InkToolbarPenConfigurationControlMod =

    let private inkToolbarPenConfigurationControlNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarPenConfigurationControl> inkToolbarPenConfigurationControlNodeId

    let inkToolbarPenConfigurationControl () =
        createNode<InkToolbarPenConfigurationControl> inkToolbarPenConfigurationControlNodeId
