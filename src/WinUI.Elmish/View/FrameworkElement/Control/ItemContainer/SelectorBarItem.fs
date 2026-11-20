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
module SelectorBarItemMod =

    let private selectorBarItemNodeId = getNextId ()

    registerDefaultRenderer<SelectorBarItem> selectorBarItemNodeId

    let private iconPropertyId = getNextId ()
    let private textPropertyId = getNextId ()

    [<Extension>]
    type SelectorBarItemExtensions =

        [<Extension>]
        static member icon(node: VirtualNode<SelectorBarItem>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                iconPropertyId
                value
                (fun element child -> (element :?> SelectorBarItem).Icon <- (child :?> IconElement))
                (fun element -> (element :?> SelectorBarItem).Icon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member text(node: VirtualNode<SelectorBarItem>, value: string) =
            createDependencyProperty textPropertyId SelectorBarItem.TextProperty value
            |> node.set

    let selectorBarItem () =
        createNode<SelectorBarItem> selectorBarItemNodeId
