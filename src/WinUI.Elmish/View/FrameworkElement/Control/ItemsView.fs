namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property CurrentItemIndexProperty: read-only
// skipped dependency property ScrollViewProperty: read-only
// skipped dependency property SelectedItemProperty: read-only
// skipped property SelectedItems: read-only


[<AutoOpen>]
module ItemsViewMod =

    let private itemsViewNodeId = getNextId ()

    registerDefaultRenderer<ItemsView> itemsViewNodeId

    let private isItemInvokedEnabledPropertyId = getNextId ()
    let private itemTemplatePropertyId = getNextId ()
    let private itemTransitionProviderPropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()
    let private layoutPropertyId = getNextId ()
    let private selectionModePropertyId = getNextId ()
    let private verticalScrollControllerPropertyId = getNextId ()

    [<Extension>]
    type ItemsViewExtensions =

        [<Extension>]
        static member isItemInvokedEnabled(node: VirtualNode<ItemsView>, value: bool) =
            createDependencyProperty isItemInvokedEnabledPropertyId ItemsView.IsItemInvokedEnabledProperty value
            |> node.set

        [<Extension>]
        static member itemTemplate(node: VirtualNode<ItemsView>, value: IElementFactory) =
            createDependencyProperty itemTemplatePropertyId ItemsView.ItemTemplateProperty value
            |> node.set

        [<Extension>]
        static member itemTransitionProvider(node: VirtualNode<ItemsView>, value: ItemCollectionTransitionProvider) =
            createDependencyProperty itemTransitionProviderPropertyId ItemsView.ItemTransitionProviderProperty value
            |> node.set

        [<Extension>]
        static member itemsSource<'source, 'key>
            (node: VirtualNode<ItemsView>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> ItemsView).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> ItemsView).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member layout(node: VirtualNode<ItemsView>, value: Layout) =
            createDependencyProperty layoutPropertyId ItemsView.LayoutProperty value
            |> node.set

        [<Extension>]
        static member selectionMode(node: VirtualNode<ItemsView>, value: ItemsViewSelectionMode) =
            createDependencyProperty selectionModePropertyId ItemsView.SelectionModeProperty value
            |> node.set

        [<Extension>]
        static member verticalScrollController(node: VirtualNode<ItemsView>, value: IScrollController) =
            createDependencyProperty verticalScrollControllerPropertyId ItemsView.VerticalScrollControllerProperty value
            |> node.set

        [<Extension>]
        static member onItemInvoked(node: VirtualNode<ItemsView>, handler: ItemsViewItemInvokedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ItemInvoked"
                Subscribe = (fun uiElement ->
                    let itemsView = uiElement :?> ItemsView
                    let h = TypedEventHandler<ItemsView, ItemsViewItemInvokedEventArgs>(fun _ e -> handler e)
                    itemsView.add_ItemInvoked h
                    { new IDisposable with
                        member _.Dispose() = itemsView.remove_ItemInvoked h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<ItemsView>, handler: ItemsViewSelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let itemsView = uiElement :?> ItemsView
                    let h = TypedEventHandler<ItemsView, ItemsViewSelectionChangedEventArgs>(fun _ e -> handler e)
                    itemsView.add_SelectionChanged h
                    { new IDisposable with
                        member _.Dispose() = itemsView.remove_SelectionChanged h }
                )
            })

    let itemsView () =
        createNode<ItemsView> itemsViewNodeId
