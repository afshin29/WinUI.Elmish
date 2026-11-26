namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property DiagnosticsProperty: read-only
// skipped dependency property DurationProperty: read-only
// skipped dependency property IsAnimatedVisualLoadedProperty: read-only
// skipped dependency property IsPlayingProperty: read-only
// skipped property ProgressObject: read-only


[<AutoOpen>]
module AnimatedVisualPlayerMod =

    let private animatedVisualPlayerNodeId = getNextId ()

    registerDefaultRenderer<AnimatedVisualPlayer> animatedVisualPlayerNodeId

    let private animationOptimizationPropertyId = getNextId ()
    let private autoPlayPropertyId = getNextId ()
    let private fallbackContentPropertyId = getNextId ()
    let private playbackRatePropertyId = getNextId ()
    let private sourcePropertyId = getNextId ()
    let private stretchPropertyId = getNextId ()

    [<Extension>]
    type AnimatedVisualPlayerExtensions =

        [<Extension>]
        static member animationOptimization(node: VirtualNode<AnimatedVisualPlayer>, value: PlayerAnimationOptimization) =
            createDependencyProperty animationOptimizationPropertyId AnimatedVisualPlayer.AnimationOptimizationProperty value
            |> node.set

        [<Extension>]
        static member autoPlay(node: VirtualNode<AnimatedVisualPlayer>, value: bool) =
            createDependencyProperty autoPlayPropertyId AnimatedVisualPlayer.AutoPlayProperty value
            |> node.set

        [<Extension>]
        static member fallbackContent<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<AnimatedVisualPlayer>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty fallbackContentPropertyId AnimatedVisualPlayer.FallbackContentProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member playbackRate(node: VirtualNode<AnimatedVisualPlayer>, value: float) =
            createDependencyProperty playbackRatePropertyId AnimatedVisualPlayer.PlaybackRateProperty value
            |> node.set

        [<Extension>]
        static member source(node: VirtualNode<AnimatedVisualPlayer>, value: IAnimatedVisualSource) =
            createDependencyProperty sourcePropertyId AnimatedVisualPlayer.SourceProperty value
            |> node.set

        [<Extension>]
        static member stretch(node: VirtualNode<AnimatedVisualPlayer>, value: Stretch) =
            createDependencyProperty stretchPropertyId AnimatedVisualPlayer.StretchProperty value
            |> node.set

    let animatedVisualPlayer () =
        createNode<AnimatedVisualPlayer> animatedVisualPlayerNodeId
