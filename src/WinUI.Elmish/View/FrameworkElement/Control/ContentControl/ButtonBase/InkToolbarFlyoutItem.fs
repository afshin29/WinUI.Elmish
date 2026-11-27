namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module InkToolbarFlyoutItemMod =

    let private inkToolbarFlyoutItemNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarFlyoutItem> inkToolbarFlyoutItemNodeId

    let private isCheckedPropertyId = getNextId ()
    let private kindPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarFlyoutItemExtensions =

        [<Extension>]
        static member isChecked(node: VirtualNode<InkToolbarFlyoutItem>, value: bool) =
            createDependencyProperty isCheckedPropertyId InkToolbarFlyoutItem.IsCheckedProperty value
            |> node.set

        [<Extension>]
        static member kind(node: VirtualNode<InkToolbarFlyoutItem>, value: InkToolbarFlyoutItemKind) =
            createDependencyProperty kindPropertyId InkToolbarFlyoutItem.KindProperty value
            |> node.set

        [<Extension>]
        static member onChecked(node: VirtualNode<InkToolbarFlyoutItem>, handler: InkToolbarFlyoutItem -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Checked"
                Subscribe = (fun uiElement ->
                    let inkToolbarFlyoutItem = uiElement :?> InkToolbarFlyoutItem
                    let h = TypedEventHandler<InkToolbarFlyoutItem, obj>(fun _ e -> handler inkToolbarFlyoutItem e)
                    inkToolbarFlyoutItem.add_Checked h
                    { new IDisposable with
                        member _.Dispose() = inkToolbarFlyoutItem.remove_Checked h }
                )
            })

        [<Extension>]
        static member onUnchecked(node: VirtualNode<InkToolbarFlyoutItem>, handler: InkToolbarFlyoutItem -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Unchecked"
                Subscribe = (fun uiElement ->
                    let inkToolbarFlyoutItem = uiElement :?> InkToolbarFlyoutItem
                    let h = TypedEventHandler<InkToolbarFlyoutItem, obj>(fun _ e -> handler inkToolbarFlyoutItem e)
                    inkToolbarFlyoutItem.add_Unchecked h
                    { new IDisposable with
                        member _.Dispose() = inkToolbarFlyoutItem.remove_Unchecked h }
                )
            })

    let inkToolbarFlyoutItem () =
        createNode<InkToolbarFlyoutItem> inkToolbarFlyoutItemNodeId
