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


[<AutoOpen>]
module BitmapIconMod =

    let private bitmapIconNodeId = getNextId ()

    registerDefaultRenderer<BitmapIcon> bitmapIconNodeId

    let private showAsMonochromePropertyId = getNextId ()
    let private uriSourcePropertyId = getNextId ()

    [<Extension>]
    type BitmapIconExtensions =

        [<Extension>]
        static member showAsMonochrome(node: VirtualNode<BitmapIcon>, value: bool) =
            createDependencyProperty showAsMonochromePropertyId BitmapIcon.ShowAsMonochromeProperty value
            |> node.set

        [<Extension>]
        static member uriSource(node: VirtualNode<BitmapIcon>, value: Uri) =
            createDependencyProperty uriSourcePropertyId BitmapIcon.UriSourceProperty value
            |> node.set

    let bitmapIcon () =
        createNode<BitmapIcon> bitmapIconNodeId
