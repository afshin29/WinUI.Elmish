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

// skipped dependency property ItemsProperty: read-only


[<AutoOpen>]
module MenuBarMod =

    let private menuBarNodeId = getNextId ()

    registerDefaultRenderer<MenuBar> menuBarNodeId

    let menuBar () =
        createNode<MenuBar> menuBarNodeId
