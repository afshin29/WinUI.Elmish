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


[<AutoOpen>]
module MediaPlayerPresenterMod =

    let private mediaPlayerPresenterNodeId = getNextId ()

    registerDefaultRenderer<MediaPlayerPresenter> mediaPlayerPresenterNodeId

    let private isFullWindowPropertyId = getNextId ()
    let private mediaPlayerPropertyId = getNextId ()
    let private stretchPropertyId = getNextId ()

    [<Extension>]
    type MediaPlayerPresenterExtensions =

        [<Extension>]
        static member isFullWindow(node: VirtualNode<MediaPlayerPresenter>, value: bool) =
            createDependencyProperty isFullWindowPropertyId MediaPlayerPresenter.IsFullWindowProperty value
            |> node.set

        [<Extension>]
        static member mediaPlayer(node: VirtualNode<MediaPlayerPresenter>, value: MediaPlayer) =
            createDependencyProperty mediaPlayerPropertyId MediaPlayerPresenter.MediaPlayerProperty value
            |> node.set

        [<Extension>]
        static member stretch(node: VirtualNode<MediaPlayerPresenter>, value: Stretch) =
            createDependencyProperty stretchPropertyId MediaPlayerPresenter.StretchProperty value
            |> node.set

    let mediaPlayerPresenter () =
        createNode<MediaPlayerPresenter> mediaPlayerPresenterNodeId
