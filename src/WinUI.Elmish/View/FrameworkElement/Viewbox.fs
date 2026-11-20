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
module ViewboxMod =

    let private viewboxNodeId = getNextId ()

    registerDefaultRenderer<Viewbox> viewboxNodeId

    let private stretchDirectionPropertyId = getNextId ()
    let private stretchPropertyId = getNextId ()
    let private childPropertyId = getNextId ()

    [<Extension>]
    type ViewboxExtensions =

        [<Extension>]
        static member stretchDirection(node: VirtualNode<Viewbox>, value: StretchDirection) =
            createDependencyProperty stretchDirectionPropertyId Viewbox.StretchDirectionProperty value
            |> node.set

        [<Extension>]
        static member stretch(node: VirtualNode<Viewbox>, value: Stretch) =
            createDependencyProperty stretchPropertyId Viewbox.StretchProperty value
            |> node.set

        [<Extension>]
        static member child(node: VirtualNode<Viewbox>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                childPropertyId
                value
                (fun element child -> (element :?> Viewbox).Child <- child)
                (fun element -> (element :?> Viewbox).Child)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

    let viewbox () =
        createNode<Viewbox> viewboxNodeId
