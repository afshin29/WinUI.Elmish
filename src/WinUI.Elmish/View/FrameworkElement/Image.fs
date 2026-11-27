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


[<AutoOpen>]
module ImageMod =

    let private imageNodeId = getNextId ()

    registerDefaultRenderer<Image> imageNodeId

    let private nineGridPropertyId = getNextId ()
    let private sourcePropertyId = getNextId ()
    let private stretchPropertyId = getNextId ()

    [<Extension>]
    type ImageExtensions =

        [<Extension>]
        static member nineGrid(node: VirtualNode<Image>, value: Thickness) =
            createDependencyProperty nineGridPropertyId Image.NineGridProperty value
            |> node.set

        [<Extension>]
        static member source(node: VirtualNode<Image>, value: ImageSource) =
            createDependencyProperty sourcePropertyId Image.SourceProperty value
            |> node.set

        [<Extension>]
        static member stretch(node: VirtualNode<Image>, value: Stretch) =
            createDependencyProperty stretchPropertyId Image.StretchProperty value
            |> node.set

        [<Extension>]
        static member onImageFailed(node: VirtualNode<Image>, handler: Image -> ExceptionRoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ImageFailed"
                Subscribe = (fun uiElement ->
                    let image = uiElement :?> Image
                    let h = ExceptionRoutedEventHandler(fun _ e -> handler image e)
                    image.ImageFailed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = image.ImageFailed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onImageOpened(node: VirtualNode<Image>, handler: Image -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ImageOpened"
                Subscribe = (fun uiElement ->
                    let image = uiElement :?> Image
                    let h = RoutedEventHandler(fun _ e -> handler image e)
                    image.ImageOpened.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = image.ImageOpened.RemoveHandler h }
                )
            })

    let image () =
        createNode<Image> imageNodeId
