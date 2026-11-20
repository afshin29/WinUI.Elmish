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
module InkToolbarCustomPenButtonMod =

    let private inkToolbarCustomPenButtonNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarCustomPenButton> inkToolbarCustomPenButtonNodeId

    let private configurationContentPropertyId = getNextId ()
    let private customPenPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarCustomPenButtonExtensions =

        [<Extension>]
        static member configurationContent(node: VirtualNode<InkToolbarCustomPenButton>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                configurationContentPropertyId
                value
                (fun element child -> (element :?> InkToolbarCustomPenButton).ConfigurationContent <- child)
                (fun element -> (element :?> InkToolbarCustomPenButton).ConfigurationContent)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member customPen(node: VirtualNode<InkToolbarCustomPenButton>, value: InkToolbarCustomPen) =
            createDependencyProperty customPenPropertyId InkToolbarCustomPenButton.CustomPenProperty value
            |> node.set

    let inkToolbarCustomPenButton () =
        createNode<InkToolbarCustomPenButton> inkToolbarCustomPenButtonNodeId
