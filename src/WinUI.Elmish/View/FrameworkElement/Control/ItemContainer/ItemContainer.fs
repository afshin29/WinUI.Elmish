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
module ItemContainerMod =

    let private itemContainerNodeId = getNextId ()

    registerDefaultRenderer<ItemContainer> itemContainerNodeId

    let private canUserInvokePropertyId = getNextId ()
    let private canUserSelectPropertyId = getNextId ()
    let private childPropertyId = getNextId ()
    let private isSelectedPropertyId = getNextId ()
    let private multiSelectModePropertyId = getNextId ()

    [<Extension>]
    type ItemContainerExtensions =

        [<Extension>]
        static member canUserInvoke<'m when 'm :> ItemContainer and 'm: not struct>
            (node: VirtualNode<'m>, value: ItemContainerUserInvokeMode) =
            createDependencyProperty canUserInvokePropertyId ItemContainer.CanUserInvokeProperty value
            |> node.set

        [<Extension>]
        static member canUserSelect<'m when 'm :> ItemContainer and 'm: not struct>
            (node: VirtualNode<'m>, value: ItemContainerUserSelectMode) =
            createDependencyProperty canUserSelectPropertyId ItemContainer.CanUserSelectProperty value
            |> node.set

        [<Extension>]
        static member child<'m when 'm :> ItemContainer and 'm: not struct>
            (node: VirtualNode<'m>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                childPropertyId
                value
                (fun element child -> (element :?> ItemContainer).Child <- child)
                (fun element -> (element :?> ItemContainer).Child)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member isSelected<'m when 'm :> ItemContainer and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isSelectedPropertyId ItemContainer.IsSelectedProperty value
            |> node.set

        [<Extension>]
        static member multiSelectMode<'m when 'm :> ItemContainer and 'm: not struct>
            (node: VirtualNode<'m>, value: ItemContainerMultiSelectMode) =
            createDependencyProperty multiSelectModePropertyId ItemContainer.MultiSelectModeProperty value
            |> node.set

        [<Extension>]
        static member onItemInvoked<'m when 'm :> ItemContainer and 'm: not struct>
            (node: VirtualNode<'m>, handler: ItemContainerInvokedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ItemInvoked"
                Subscribe = (fun uiElement ->
                    let itemContainer = uiElement :?> ItemContainer
                    let h = TypedEventHandler<ItemContainer, ItemContainerInvokedEventArgs>(fun _ e -> handler e)
                    itemContainer.add_ItemInvoked h
                    { new IDisposable with
                        member _.Dispose() = itemContainer.remove_ItemInvoked h }
                )
            })

    let itemContainer () =
        createNode<ItemContainer> itemContainerNodeId
