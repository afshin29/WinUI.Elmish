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
module InkToolbarCustomToolButtonMod =

    let private inkToolbarCustomToolButtonNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarCustomToolButton> inkToolbarCustomToolButtonNodeId

    let private configurationContentPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarCustomToolButtonExtensions =

        [<Extension>]
        static member configurationContent(node: VirtualNode<InkToolbarCustomToolButton>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                configurationContentPropertyId
                value
                (fun element child -> (element :?> InkToolbarCustomToolButton).ConfigurationContent <- child)
                (fun element -> (element :?> InkToolbarCustomToolButton).ConfigurationContent)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

    let inkToolbarCustomToolButton () =
        createNode<InkToolbarCustomToolButton> inkToolbarCustomToolButtonNodeId
