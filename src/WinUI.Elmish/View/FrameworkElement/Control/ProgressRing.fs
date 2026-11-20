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
module ProgressRingMod =

    let private progressRingNodeId = getNextId ()

    registerDefaultRenderer<ProgressRing> progressRingNodeId

    let private determinateSourcePropertyId = getNextId ()
    let private indeterminateSourcePropertyId = getNextId ()
    let private isActivePropertyId = getNextId ()
    let private isIndeterminatePropertyId = getNextId ()
    let private maximumPropertyId = getNextId ()
    let private minimumPropertyId = getNextId ()
    let private valuePropertyId = getNextId ()

    [<Extension>]
    type ProgressRingExtensions =

        [<Extension>]
        static member determinateSource(node: VirtualNode<ProgressRing>, value: IAnimatedVisualSource) =
            createDependencyProperty determinateSourcePropertyId ProgressRing.DeterminateSourceProperty value
            |> node.set

        [<Extension>]
        static member indeterminateSource(node: VirtualNode<ProgressRing>, value: IAnimatedVisualSource) =
            createDependencyProperty indeterminateSourcePropertyId ProgressRing.IndeterminateSourceProperty value
            |> node.set

        [<Extension>]
        static member isActive(node: VirtualNode<ProgressRing>, value: bool) =
            createDependencyProperty isActivePropertyId ProgressRing.IsActiveProperty value
            |> node.set

        [<Extension>]
        static member isIndeterminate(node: VirtualNode<ProgressRing>, value: bool) =
            createDependencyProperty isIndeterminatePropertyId ProgressRing.IsIndeterminateProperty value
            |> node.set

        [<Extension>]
        static member maximum(node: VirtualNode<ProgressRing>, value: float) =
            createDependencyProperty maximumPropertyId ProgressRing.MaximumProperty value
            |> node.set

        [<Extension>]
        static member minimum(node: VirtualNode<ProgressRing>, value: float) =
            createDependencyProperty minimumPropertyId ProgressRing.MinimumProperty value
            |> node.set

        [<Extension>]
        static member value(node: VirtualNode<ProgressRing>, value: float) =
            createDependencyProperty valuePropertyId ProgressRing.ValueProperty value
            |> node.set

    let progressRing () =
        createNode<ProgressRing> progressRingNodeId
