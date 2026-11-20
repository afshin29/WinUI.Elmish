namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module BreadcrumbBarMod =

    let private breadcrumbBarNodeId = getNextId ()

    registerDefaultRenderer<BreadcrumbBar> breadcrumbBarNodeId

    let private itemTemplatePropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()

    [<Extension>]
    type BreadcrumbBarExtensions =

        [<Extension>]
        static member itemTemplate(node: VirtualNode<BreadcrumbBar>, value: obj) =
            createDependencyProperty itemTemplatePropertyId BreadcrumbBar.ItemTemplateProperty value
            |> node.set

        [<Extension>]
        static member itemsSource<'source, 'key>
            (node: VirtualNode<BreadcrumbBar>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> BreadcrumbBar).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> BreadcrumbBar).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member onItemClicked(node: VirtualNode<BreadcrumbBar>, handler: BreadcrumbBarItemClickedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ItemClicked"
                Subscribe = (fun uiElement ->
                    let breadcrumbBar = uiElement :?> BreadcrumbBar
                    let h = TypedEventHandler<BreadcrumbBar, BreadcrumbBarItemClickedEventArgs>(fun _ e -> handler e)
                    breadcrumbBar.add_ItemClicked h
                    { new IDisposable with
                        member _.Dispose() = breadcrumbBar.remove_ItemClicked h }
                )
            })

    let breadcrumbBar () =
        createNode<BreadcrumbBar> breadcrumbBarNodeId
