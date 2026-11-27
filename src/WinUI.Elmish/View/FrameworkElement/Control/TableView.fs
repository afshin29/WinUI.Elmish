namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Tabular
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

// skipped dependency property ColumnsProperty: read-only
// skipped dependency property SelectedIndexProperty: read-only
// skipped dependency property SelectedItemProperty: read-only
// skipped property IsEditing: read-only


[<AutoOpen>]
module TableViewMod =

    let private tableViewNodeId = getNextId ()

    registerDefaultRenderer<TableView> tableViewNodeId

    let private alternatingRowBackgroundPropertyId = getNextId ()
    let private canUserResizeColumnsPropertyId = getNextId ()
    let private canUserSortColumnsPropertyId = getNextId ()
    let private densityPropertyId = getNextId ()
    let private emptyTemplatePropertyId = getNextId ()
    let private gridLinesVisibilityPropertyId = getNextId ()
    let private groupHeaderTemplatePropertyId = getNextId ()
    let private headersVisibilityPropertyId = getNextId ()
    let private isReadOnlyPropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()
    let private rowBackgroundPropertyId = getNextId ()
    let private selectionModePropertyId = getNextId ()

    [<Extension>]
    type TableViewExtensions =

        [<Extension>]
        static member alternatingRowBackground(node: VirtualNode<TableView>, value: Brush) =
            createDependencyProperty alternatingRowBackgroundPropertyId TableView.AlternatingRowBackgroundProperty value
            |> node.set

        [<Extension>]
        static member canUserResizeColumns(node: VirtualNode<TableView>, value: bool) =
            createDependencyProperty canUserResizeColumnsPropertyId TableView.CanUserResizeColumnsProperty value
            |> node.set

        [<Extension>]
        static member canUserSortColumns(node: VirtualNode<TableView>, value: bool) =
            createDependencyProperty canUserSortColumnsPropertyId TableView.CanUserSortColumnsProperty value
            |> node.set

        [<Extension>]
        static member density(node: VirtualNode<TableView>, value: TableViewDensity) =
            createDependencyProperty densityPropertyId TableView.DensityProperty value
            |> node.set

        [<Extension>]
        static member emptyTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<TableView>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty emptyTemplatePropertyId TableView.EmptyTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member gridLinesVisibility(node: VirtualNode<TableView>, value: TableViewGridLinesVisibility) =
            createDependencyProperty gridLinesVisibilityPropertyId TableView.GridLinesVisibilityProperty value
            |> node.set

        [<Extension>]
        static member groupHeaderTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<TableView>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty groupHeaderTemplatePropertyId TableView.GroupHeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member headersVisibility(node: VirtualNode<TableView>, value: TableViewHeadersVisibility) =
            createDependencyProperty headersVisibilityPropertyId TableView.HeadersVisibilityProperty value
            |> node.set

        [<Extension>]
        static member isReadOnly(node: VirtualNode<TableView>, value: bool) =
            createDependencyProperty isReadOnlyPropertyId TableView.IsReadOnlyProperty value
            |> node.set

        [<Extension>]
        static member itemsSource<'source, 'key>
            (node: VirtualNode<TableView>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> TableView).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> TableView).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member rowBackground(node: VirtualNode<TableView>, value: Brush) =
            createDependencyProperty rowBackgroundPropertyId TableView.RowBackgroundProperty value
            |> node.set

        [<Extension>]
        static member selectionMode(node: VirtualNode<TableView>, value: TableViewSelectionMode) =
            createDependencyProperty selectionModePropertyId TableView.SelectionModeProperty value
            |> node.set

        [<Extension>]
        static member onBeginningEdit(node: VirtualNode<TableView>, handler: TableView -> TableViewBeginningEditEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "BeginningEdit"
                Subscribe = (fun uiElement ->
                    let tableView = uiElement :?> TableView
                    let h = TypedEventHandler<TableView, TableViewBeginningEditEventArgs>(fun _ e -> handler tableView e)
                    tableView.add_BeginningEdit h
                    { new IDisposable with
                        member _.Dispose() = tableView.remove_BeginningEdit h }
                )
            })

        [<Extension>]
        static member onCellEditEnding(node: VirtualNode<TableView>, handler: TableView -> TableViewCellEditEndingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CellEditEnding"
                Subscribe = (fun uiElement ->
                    let tableView = uiElement :?> TableView
                    let h = TypedEventHandler<TableView, TableViewCellEditEndingEventArgs>(fun _ e -> handler tableView e)
                    tableView.add_CellEditEnding h
                    { new IDisposable with
                        member _.Dispose() = tableView.remove_CellEditEnding h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<TableView>, handler: TableView -> SelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let tableView = uiElement :?> TableView
                    let h = TypedEventHandler<TableView, SelectionChangedEventArgs>(fun _ e -> handler tableView e)
                    tableView.add_SelectionChanged h
                    { new IDisposable with
                        member _.Dispose() = tableView.remove_SelectionChanged h }
                )
            })

        [<Extension>]
        static member onSorted(node: VirtualNode<TableView>, handler: TableView -> TableViewSortedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Sorted"
                Subscribe = (fun uiElement ->
                    let tableView = uiElement :?> TableView
                    let h = TypedEventHandler<TableView, TableViewSortedEventArgs>(fun _ e -> handler tableView e)
                    tableView.add_Sorted h
                    { new IDisposable with
                        member _.Dispose() = tableView.remove_Sorted h }
                )
            })

        [<Extension>]
        static member onSorting(node: VirtualNode<TableView>, handler: TableView -> TableViewSortingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Sorting"
                Subscribe = (fun uiElement ->
                    let tableView = uiElement :?> TableView
                    let h = TypedEventHandler<TableView, TableViewSortingEventArgs>(fun _ e -> handler tableView e)
                    tableView.add_Sorting h
                    { new IDisposable with
                        member _.Dispose() = tableView.remove_Sorting h }
                )
            })

    let tableView () =
        createNode<TableView> tableViewNodeId
