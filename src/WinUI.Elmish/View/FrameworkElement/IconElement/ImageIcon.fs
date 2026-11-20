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
module ImageIconMod =

    let private imageIconNodeId = getNextId ()

    registerDefaultRenderer<ImageIcon> imageIconNodeId

    let private sourcePropertyId = getNextId ()

    [<Extension>]
    type ImageIconExtensions =

        [<Extension>]
        static member source(node: VirtualNode<ImageIcon>, value: ImageSource) =
            createDependencyProperty sourcePropertyId ImageIcon.SourceProperty value
            |> node.set

    let imageIcon () =
        createNode<ImageIcon> imageIconNodeId
