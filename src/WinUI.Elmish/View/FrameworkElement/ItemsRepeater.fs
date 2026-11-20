namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property ItemsSourceView: read-only


[<AutoOpen>]
module ItemsRepeaterMod =

    let private itemsRepeaterNodeId = getNextId ()

    registerDefaultRenderer<ItemsRepeater> itemsRepeaterNodeId

    let private backgroundPropertyId = getNextId ()
    let private horizontalCacheLengthPropertyId = getNextId ()
    let private itemTemplatePropertyId = getNextId ()
    let private itemTransitionProviderPropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()
    let private layoutPropertyId = getNextId ()
    let private verticalCacheLengthPropertyId = getNextId ()

    [<Extension>]
    type ItemsRepeaterExtensions =

        [<Extension>]
        static member background(node: VirtualNode<ItemsRepeater>, value: Brush) =
            createDependencyProperty backgroundPropertyId ItemsRepeater.BackgroundProperty value
            |> node.set

        [<Extension>]
        static member horizontalCacheLength(node: VirtualNode<ItemsRepeater>, value: float) =
            createDependencyProperty horizontalCacheLengthPropertyId ItemsRepeater.HorizontalCacheLengthProperty value
            |> node.set

        [<Extension>]
        static member itemTemplate(node: VirtualNode<ItemsRepeater>, value: obj) =
            createDependencyProperty itemTemplatePropertyId ItemsRepeater.ItemTemplateProperty value
            |> node.set

        [<Extension>]
        static member itemTransitionProvider(node: VirtualNode<ItemsRepeater>, value: ItemCollectionTransitionProvider) =
            createDependencyProperty itemTransitionProviderPropertyId ItemsRepeater.ItemTransitionProviderProperty value
            |> node.set

        [<Extension>]
        static member itemsSource<'source, 'key>
            (node: VirtualNode<ItemsRepeater>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> ItemsRepeater).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> ItemsRepeater).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member layout(node: VirtualNode<ItemsRepeater>, value: Layout) =
            createDependencyProperty layoutPropertyId ItemsRepeater.LayoutProperty value
            |> node.set

        [<Extension>]
        static member verticalCacheLength(node: VirtualNode<ItemsRepeater>, value: float) =
            createDependencyProperty verticalCacheLengthPropertyId ItemsRepeater.VerticalCacheLengthProperty value
            |> node.set

        [<Extension>]
        static member onElementClearing(node: VirtualNode<ItemsRepeater>, handler: ItemsRepeaterElementClearingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ElementClearing"
                Subscribe = (fun uiElement ->
                    let itemsRepeater = uiElement :?> ItemsRepeater
                    let h = TypedEventHandler<ItemsRepeater, ItemsRepeaterElementClearingEventArgs>(fun _ e -> handler e)
                    itemsRepeater.add_ElementClearing h
                    { new IDisposable with
                        member _.Dispose() = itemsRepeater.remove_ElementClearing h }
                )
            })

        [<Extension>]
        static member onElementIndexChanged(node: VirtualNode<ItemsRepeater>, handler: ItemsRepeaterElementIndexChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ElementIndexChanged"
                Subscribe = (fun uiElement ->
                    let itemsRepeater = uiElement :?> ItemsRepeater
                    let h = TypedEventHandler<ItemsRepeater, ItemsRepeaterElementIndexChangedEventArgs>(fun _ e -> handler e)
                    itemsRepeater.add_ElementIndexChanged h
                    { new IDisposable with
                        member _.Dispose() = itemsRepeater.remove_ElementIndexChanged h }
                )
            })

        [<Extension>]
        static member onElementPrepared(node: VirtualNode<ItemsRepeater>, handler: ItemsRepeaterElementPreparedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ElementPrepared"
                Subscribe = (fun uiElement ->
                    let itemsRepeater = uiElement :?> ItemsRepeater
                    let h = TypedEventHandler<ItemsRepeater, ItemsRepeaterElementPreparedEventArgs>(fun _ e -> handler e)
                    itemsRepeater.add_ElementPrepared h
                    { new IDisposable with
                        member _.Dispose() = itemsRepeater.remove_ElementPrepared h }
                )
            })

    let itemsRepeater () =
        createNode<ItemsRepeater> itemsRepeaterNodeId
