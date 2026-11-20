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
module UserControlMod =

    let private userControlNodeId = getNextId ()

    registerDefaultRenderer<UserControl> userControlNodeId

    let private contentPropertyId = getNextId ()

    [<Extension>]
    type UserControlExtensions =

        [<Extension>]
        static member content<'m when 'm :> UserControl and 'm: not struct>
            (node: VirtualNode<'m>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                contentPropertyId
                value
                (fun element child -> (element :?> UserControl).Content <- child)
                (fun element -> (element :?> UserControl).Content)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

    let userControl () =
        createNode<UserControl> userControlNodeId
