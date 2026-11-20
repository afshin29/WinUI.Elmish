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

// skipped property SelectedItems: read-only


[<AutoOpen>]
module ListBoxMod =

    let private listBoxNodeId = getNextId ()

    registerDefaultRenderer<ListBox> listBoxNodeId

    let private selectionModePropertyId = getNextId ()
    let private singleSelectionFollowsFocusPropertyId = getNextId ()

    [<Extension>]
    type ListBoxExtensions =

        [<Extension>]
        static member selectionMode(node: VirtualNode<ListBox>, value: SelectionMode) =
            createDependencyProperty selectionModePropertyId ListBox.SelectionModeProperty value
            |> node.set

        [<Extension>]
        static member singleSelectionFollowsFocus(node: VirtualNode<ListBox>, value: bool) =
            createDependencyProperty singleSelectionFollowsFocusPropertyId ListBox.SingleSelectionFollowsFocusProperty value
            |> node.set

    let listBox () =
        createNode<ListBox> listBoxNodeId
