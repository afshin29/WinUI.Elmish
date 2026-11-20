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

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module ProgressBarMod =

    let private progressBarNodeId = getNextId ()

    registerDefaultRenderer<ProgressBar> progressBarNodeId

    let private isIndeterminatePropertyId = getNextId ()
    let private showErrorPropertyId = getNextId ()
    let private showPausedPropertyId = getNextId ()

    [<Extension>]
    type ProgressBarExtensions =

        [<Extension>]
        static member isIndeterminate(node: VirtualNode<ProgressBar>, value: bool) =
            createDependencyProperty isIndeterminatePropertyId ProgressBar.IsIndeterminateProperty value
            |> node.set

        [<Extension>]
        static member showError(node: VirtualNode<ProgressBar>, value: bool) =
            createDependencyProperty showErrorPropertyId ProgressBar.ShowErrorProperty value
            |> node.set

        [<Extension>]
        static member showPaused(node: VirtualNode<ProgressBar>, value: bool) =
            createDependencyProperty showPausedPropertyId ProgressBar.ShowPausedProperty value
            |> node.set

    let progressBar () =
        createNode<ProgressBar> progressBarNodeId
