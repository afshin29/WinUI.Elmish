namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Media.Playback

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property MediaPlayerProperty: read-only


[<AutoOpen>]
module MediaPlayerElementMod =

    let private mediaPlayerElementNodeId = getNextId ()

    registerDefaultRenderer<MediaPlayerElement> mediaPlayerElementNodeId

    let private areTransportControlsEnabledPropertyId = getNextId ()
    let private autoPlayPropertyId = getNextId ()
    let private isFullWindowPropertyId = getNextId ()
    let private posterSourcePropertyId = getNextId ()
    let private sourcePropertyId = getNextId ()
    let private stretchPropertyId = getNextId ()
    let private transportControlsPropertyId = getNextId ()

    [<Extension>]
    type MediaPlayerElementExtensions =

        [<Extension>]
        static member areTransportControlsEnabled(node: VirtualNode<MediaPlayerElement>, value: bool) =
            createDependencyProperty areTransportControlsEnabledPropertyId MediaPlayerElement.AreTransportControlsEnabledProperty value
            |> node.set

        [<Extension>]
        static member autoPlay(node: VirtualNode<MediaPlayerElement>, value: bool) =
            createDependencyProperty autoPlayPropertyId MediaPlayerElement.AutoPlayProperty value
            |> node.set

        [<Extension>]
        static member isFullWindow(node: VirtualNode<MediaPlayerElement>, value: bool) =
            createDependencyProperty isFullWindowPropertyId MediaPlayerElement.IsFullWindowProperty value
            |> node.set

        [<Extension>]
        static member posterSource(node: VirtualNode<MediaPlayerElement>, value: ImageSource) =
            createDependencyProperty posterSourcePropertyId MediaPlayerElement.PosterSourceProperty value
            |> node.set

        [<Extension>]
        static member source(node: VirtualNode<MediaPlayerElement>, value: IMediaPlaybackSource) =
            createDependencyProperty sourcePropertyId MediaPlayerElement.SourceProperty value
            |> node.set

        [<Extension>]
        static member stretch(node: VirtualNode<MediaPlayerElement>, value: Stretch) =
            createDependencyProperty stretchPropertyId MediaPlayerElement.StretchProperty value
            |> node.set

        [<Extension>]
        static member transportControls(node: VirtualNode<MediaPlayerElement>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                transportControlsPropertyId
                value
                (fun element child -> (element :?> MediaPlayerElement).TransportControls <- (child :?> MediaTransportControls))
                (fun element -> (element :?> MediaPlayerElement).TransportControls :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

    let mediaPlayerElement () =
        createNode<MediaPlayerElement> mediaPlayerElementNodeId
