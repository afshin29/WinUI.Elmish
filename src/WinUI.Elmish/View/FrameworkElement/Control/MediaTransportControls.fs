namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module MediaTransportControlsMod =

    let private mediaTransportControlsNodeId = getNextId ()

    registerDefaultRenderer<MediaTransportControls> mediaTransportControlsNodeId

    let private fastPlayFallbackBehaviourPropertyId = getNextId ()
    let private isCompactPropertyId = getNextId ()
    let private isFastForwardButtonVisiblePropertyId = getNextId ()
    let private isFastForwardEnabledPropertyId = getNextId ()
    let private isFastRewindButtonVisiblePropertyId = getNextId ()
    let private isFastRewindEnabledPropertyId = getNextId ()
    let private isNextTrackButtonVisiblePropertyId = getNextId ()
    let private isPlaybackRateButtonVisiblePropertyId = getNextId ()
    let private isPlaybackRateEnabledPropertyId = getNextId ()
    let private isPreviousTrackButtonVisiblePropertyId = getNextId ()
    let private isRepeatButtonVisiblePropertyId = getNextId ()
    let private isRepeatEnabledPropertyId = getNextId ()
    let private isSeekBarVisiblePropertyId = getNextId ()
    let private isSeekEnabledPropertyId = getNextId ()
    let private isSkipBackwardButtonVisiblePropertyId = getNextId ()
    let private isSkipBackwardEnabledPropertyId = getNextId ()
    let private isSkipForwardButtonVisiblePropertyId = getNextId ()
    let private isSkipForwardEnabledPropertyId = getNextId ()
    let private isStopButtonVisiblePropertyId = getNextId ()
    let private isStopEnabledPropertyId = getNextId ()
    let private isVolumeButtonVisiblePropertyId = getNextId ()
    let private isVolumeEnabledPropertyId = getNextId ()
    let private isZoomButtonVisiblePropertyId = getNextId ()
    let private isZoomEnabledPropertyId = getNextId ()
    let private showAndHideAutomaticallyPropertyId = getNextId ()

    [<Extension>]
    type MediaTransportControlsExtensions =

        [<Extension>]
        static member fastPlayFallbackBehaviour(node: VirtualNode<MediaTransportControls>, value: FastPlayFallbackBehaviour) =
            createDependencyProperty fastPlayFallbackBehaviourPropertyId MediaTransportControls.FastPlayFallbackBehaviourProperty value
            |> node.set

        [<Extension>]
        static member isCompact(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isCompactPropertyId MediaTransportControls.IsCompactProperty value
            |> node.set

        [<Extension>]
        static member isFastForwardButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isFastForwardButtonVisiblePropertyId MediaTransportControls.IsFastForwardButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isFastForwardEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isFastForwardEnabledPropertyId MediaTransportControls.IsFastForwardEnabledProperty value
            |> node.set

        [<Extension>]
        static member isFastRewindButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isFastRewindButtonVisiblePropertyId MediaTransportControls.IsFastRewindButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isFastRewindEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isFastRewindEnabledPropertyId MediaTransportControls.IsFastRewindEnabledProperty value
            |> node.set

        [<Extension>]
        static member isNextTrackButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isNextTrackButtonVisiblePropertyId MediaTransportControls.IsNextTrackButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isPlaybackRateButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isPlaybackRateButtonVisiblePropertyId MediaTransportControls.IsPlaybackRateButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isPlaybackRateEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isPlaybackRateEnabledPropertyId MediaTransportControls.IsPlaybackRateEnabledProperty value
            |> node.set

        [<Extension>]
        static member isPreviousTrackButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isPreviousTrackButtonVisiblePropertyId MediaTransportControls.IsPreviousTrackButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isRepeatButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isRepeatButtonVisiblePropertyId MediaTransportControls.IsRepeatButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isRepeatEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isRepeatEnabledPropertyId MediaTransportControls.IsRepeatEnabledProperty value
            |> node.set

        [<Extension>]
        static member isSeekBarVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isSeekBarVisiblePropertyId MediaTransportControls.IsSeekBarVisibleProperty value
            |> node.set

        [<Extension>]
        static member isSeekEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isSeekEnabledPropertyId MediaTransportControls.IsSeekEnabledProperty value
            |> node.set

        [<Extension>]
        static member isSkipBackwardButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isSkipBackwardButtonVisiblePropertyId MediaTransportControls.IsSkipBackwardButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isSkipBackwardEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isSkipBackwardEnabledPropertyId MediaTransportControls.IsSkipBackwardEnabledProperty value
            |> node.set

        [<Extension>]
        static member isSkipForwardButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isSkipForwardButtonVisiblePropertyId MediaTransportControls.IsSkipForwardButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isSkipForwardEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isSkipForwardEnabledPropertyId MediaTransportControls.IsSkipForwardEnabledProperty value
            |> node.set

        [<Extension>]
        static member isStopButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isStopButtonVisiblePropertyId MediaTransportControls.IsStopButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isStopEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isStopEnabledPropertyId MediaTransportControls.IsStopEnabledProperty value
            |> node.set

        [<Extension>]
        static member isVolumeButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isVolumeButtonVisiblePropertyId MediaTransportControls.IsVolumeButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isVolumeEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isVolumeEnabledPropertyId MediaTransportControls.IsVolumeEnabledProperty value
            |> node.set

        [<Extension>]
        static member isZoomButtonVisible(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isZoomButtonVisiblePropertyId MediaTransportControls.IsZoomButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isZoomEnabled(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty isZoomEnabledPropertyId MediaTransportControls.IsZoomEnabledProperty value
            |> node.set

        [<Extension>]
        static member showAndHideAutomatically(node: VirtualNode<MediaTransportControls>, value: bool) =
            createDependencyProperty showAndHideAutomaticallyPropertyId MediaTransportControls.ShowAndHideAutomaticallyProperty value
            |> node.set

        [<Extension>]
        static member onThumbnailRequested(node: VirtualNode<MediaTransportControls>, handler: MediaTransportControlsThumbnailRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ThumbnailRequested"
                Subscribe = (fun uiElement ->
                    let mediaTransportControls = uiElement :?> MediaTransportControls
                    let h = TypedEventHandler<MediaTransportControls, MediaTransportControlsThumbnailRequestedEventArgs>(fun _ e -> handler e)
                    mediaTransportControls.add_ThumbnailRequested h
                    { new IDisposable with
                        member _.Dispose() = mediaTransportControls.remove_ThumbnailRequested h }
                )
            })

    let mediaTransportControls () =
        createNode<MediaTransportControls> mediaTransportControlsNodeId
