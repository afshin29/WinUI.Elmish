namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property RootNodes: read-only
// skipped property SelectedItems: read-only
// skipped property SelectedNodes: read-only


[<AutoOpen>]
module TreeViewMod =

    let private treeViewNodeId = getNextId ()

    registerDefaultRenderer<TreeView> treeViewNodeId

    let private canDragItemsPropertyId = getNextId ()
    let private canReorderItemsPropertyId = getNextId ()
    let private itemContainerStylePropertyId = getNextId ()
    let private itemContainerStyleSelectorPropertyId = getNextId ()
    let private itemContainerTransitionsPropertyId = getNextId ()
    let private itemTemplatePropertyId = getNextId ()
    let private itemTemplateSelectorPropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()
    let private selectedItemPropertyId = getNextId ()
    let private selectionModePropertyId = getNextId ()
    let private selectedNodePropertyId = getNextId ()

    [<Extension>]
    type TreeViewExtensions =

        [<Extension>]
        static member canDragItems(node: VirtualNode<TreeView>, value: bool) =
            createDependencyProperty canDragItemsPropertyId TreeView.CanDragItemsProperty value
            |> node.set

        [<Extension>]
        static member canReorderItems(node: VirtualNode<TreeView>, value: bool) =
            createDependencyProperty canReorderItemsPropertyId TreeView.CanReorderItemsProperty value
            |> node.set

        [<Extension>]
        static member itemContainerStyle(node: VirtualNode<TreeView>, value: Style) =
            createDependencyProperty itemContainerStylePropertyId TreeView.ItemContainerStyleProperty value
            |> node.set

        [<Extension>]
        static member itemContainerStyleSelector<'source>
            (node: VirtualNode<TreeView>, styles: Style seq, selector: ('source -> int), ?diffKey: obj) =
            InternalStyleSelector(styles, selector)
            |> createDependencyProperty itemContainerStyleSelectorPropertyId TreeView.ItemContainerStyleSelectorProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemContainerTransitions(node: VirtualNode<TreeView>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty itemContainerTransitionsPropertyId TreeView.ItemContainerTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<TreeView>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty itemTemplatePropertyId TreeView.ItemTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemTemplateSelector<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<TreeView>, templates: ('source -> VirtualNode<'el>) seq, selector: ('source -> int), ?diffKey: obj) =
            createTemplateSelector templates selector
            |> createDependencyProperty itemTemplateSelectorPropertyId TreeView.ItemTemplateSelectorProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemsSource<'source, 'key>
            (node: VirtualNode<TreeView>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> TreeView).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> TreeView).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member selectedItem(node: VirtualNode<TreeView>, value: obj) =
            createDependencyProperty selectedItemPropertyId TreeView.SelectedItemProperty value
            |> node.set

        [<Extension>]
        static member selectionMode(node: VirtualNode<TreeView>, value: TreeViewSelectionMode) =
            createDependencyProperty selectionModePropertyId TreeView.SelectionModeProperty value
            |> node.set

        [<Extension>]
        static member selectedNode(node: VirtualNode<TreeView>, value: TreeViewNode) =
            createClrProperty
                selectedNodePropertyId
                (fun uiElement _ -> (uiElement :?> TreeView).SelectedNode <- value)
                (fun uiElement -> (uiElement :?> TreeView).SelectedNode <- Unchecked.defaultof<TreeViewNode>)
                value
            |> node.set

        [<Extension>]
        static member onCollapsed(node: VirtualNode<TreeView>, handler: TreeViewCollapsedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Collapsed"
                Subscribe = (fun uiElement ->
                    let treeView = uiElement :?> TreeView
                    let h = TypedEventHandler<TreeView, TreeViewCollapsedEventArgs>(fun _ e -> handler e)
                    treeView.add_Collapsed h
                    { new IDisposable with
                        member _.Dispose() = treeView.remove_Collapsed h }
                )
            })

        [<Extension>]
        static member onDragItemsCompleted(node: VirtualNode<TreeView>, handler: TreeViewDragItemsCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragItemsCompleted"
                Subscribe = (fun uiElement ->
                    let treeView = uiElement :?> TreeView
                    let h = TypedEventHandler<TreeView, TreeViewDragItemsCompletedEventArgs>(fun _ e -> handler e)
                    treeView.add_DragItemsCompleted h
                    { new IDisposable with
                        member _.Dispose() = treeView.remove_DragItemsCompleted h }
                )
            })

        [<Extension>]
        static member onDragItemsStarting(node: VirtualNode<TreeView>, handler: TreeViewDragItemsStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DragItemsStarting"
                Subscribe = (fun uiElement ->
                    let treeView = uiElement :?> TreeView
                    let h = TypedEventHandler<TreeView, TreeViewDragItemsStartingEventArgs>(fun _ e -> handler e)
                    treeView.add_DragItemsStarting h
                    { new IDisposable with
                        member _.Dispose() = treeView.remove_DragItemsStarting h }
                )
            })

        [<Extension>]
        static member onExpanding(node: VirtualNode<TreeView>, handler: TreeViewExpandingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Expanding"
                Subscribe = (fun uiElement ->
                    let treeView = uiElement :?> TreeView
                    let h = TypedEventHandler<TreeView, TreeViewExpandingEventArgs>(fun _ e -> handler e)
                    treeView.add_Expanding h
                    { new IDisposable with
                        member _.Dispose() = treeView.remove_Expanding h }
                )
            })

        [<Extension>]
        static member onItemInvoked(node: VirtualNode<TreeView>, handler: TreeViewItemInvokedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ItemInvoked"
                Subscribe = (fun uiElement ->
                    let treeView = uiElement :?> TreeView
                    let h = TypedEventHandler<TreeView, TreeViewItemInvokedEventArgs>(fun _ e -> handler e)
                    treeView.add_ItemInvoked h
                    { new IDisposable with
                        member _.Dispose() = treeView.remove_ItemInvoked h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<TreeView>, handler: TreeViewSelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let treeView = uiElement :?> TreeView
                    let h = TypedEventHandler<TreeView, TreeViewSelectionChangedEventArgs>(fun _ e -> handler e)
                    treeView.add_SelectionChanged h
                    { new IDisposable with
                        member _.Dispose() = treeView.remove_SelectionChanged h }
                )
            })

    let treeView () =
        createNode<TreeView> treeViewNodeId

    let treeViewReadOnly (treeNodes: TreeViewNode seq) =
        treeView()
            .config(fun treeView ->
                treeNodes
                |> Seq.iter treeView.RootNodes.Add
            )
            .memoize()
