namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices
open System.Windows.Input
open Windows.Foundation
open Windows.Foundation.Collections

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property TabItemsProperty: read-only


[<AutoOpen>]
module TabViewMod =

    let private tabViewNodeId = getNextId ()

    registerDefaultRenderer<TabView> tabViewNodeId

    let private addTabButtonCommandParameterPropertyId = getNextId ()
    let private addTabButtonCommandPropertyId = getNextId ()
    let private allowDropTabsPropertyId = getNextId ()
    let private canDragTabsPropertyId = getNextId ()
    let private canReorderTabsPropertyId = getNextId ()
    let private canTearOutTabsPropertyId = getNextId ()
    let private closeButtonOverlayModePropertyId = getNextId ()
    let private isAddTabButtonVisiblePropertyId = getNextId ()
    let private selectedIndexPropertyId = getNextId ()
    let private selectedItemPropertyId = getNextId ()
    let private tabItemTemplatePropertyId = getNextId ()
    let private tabItemTemplateSelectorPropertyId = getNextId ()
    let private tabItemsSourcePropertyId = getNextId ()
    let private str_tabStripFooterPropertyId = getNextId ()
    let private vnode_tabStripFooterPropertyId = getNextId ()
    let private tabStripFooterTemplatePropertyId = getNextId ()
    let private str_tabStripHeaderPropertyId = getNextId ()
    let private vnode_tabStripHeaderPropertyId = getNextId ()
    let private tabStripHeaderTemplatePropertyId = getNextId ()
    let private tabWidthModePropertyId = getNextId ()

    [<Extension>]
    type TabViewExtensions =

        [<Extension>]
        static member addTabButtonCommandParameter(node: VirtualNode<TabView>, value: obj) =
            createDependencyProperty addTabButtonCommandParameterPropertyId TabView.AddTabButtonCommandParameterProperty value
            |> node.set

        [<Extension>]
        static member addTabButtonCommand(node: VirtualNode<TabView>, value: ICommand) =
            createDependencyProperty addTabButtonCommandPropertyId TabView.AddTabButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member allowDropTabs(node: VirtualNode<TabView>, value: bool) =
            createDependencyProperty allowDropTabsPropertyId TabView.AllowDropTabsProperty value
            |> node.set

        [<Extension>]
        static member canDragTabs(node: VirtualNode<TabView>, value: bool) =
            createDependencyProperty canDragTabsPropertyId TabView.CanDragTabsProperty value
            |> node.set

        [<Extension>]
        static member canReorderTabs(node: VirtualNode<TabView>, value: bool) =
            createDependencyProperty canReorderTabsPropertyId TabView.CanReorderTabsProperty value
            |> node.set

        [<Extension>]
        static member canTearOutTabs(node: VirtualNode<TabView>, value: bool) =
            createDependencyProperty canTearOutTabsPropertyId TabView.CanTearOutTabsProperty value
            |> node.set

        [<Extension>]
        static member closeButtonOverlayMode(node: VirtualNode<TabView>, value: TabViewCloseButtonOverlayMode) =
            createDependencyProperty closeButtonOverlayModePropertyId TabView.CloseButtonOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member isAddTabButtonVisible(node: VirtualNode<TabView>, value: bool) =
            createDependencyProperty isAddTabButtonVisiblePropertyId TabView.IsAddTabButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member selectedIndex(node: VirtualNode<TabView>, value: int) =
            createDependencyProperty selectedIndexPropertyId TabView.SelectedIndexProperty value
            |> node.set

        [<Extension>]
        static member selectedItem(node: VirtualNode<TabView>, value: obj) =
            createDependencyProperty selectedItemPropertyId TabView.SelectedItemProperty value
            |> node.set

        [<Extension>]
        static member tabItemTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TabView>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty tabItemTemplatePropertyId TabView.TabItemTemplateProperty
            |> node.set

        [<Extension>]
        static member tabItemTemplateSelector<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TabView>, templates: ('source -> 'vnode) seq, selector: ('source -> int)) =
            createTemplateSelector templates selector
            |> createDependencyProperty tabItemTemplateSelectorPropertyId TabView.TabItemTemplateSelectorProperty
            |> node.set

        [<Extension>]
        static member tabItemsSource<'source, 'key>
            (node: VirtualNode<TabView>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                tabItemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> TabView).TabItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> TabView).TabItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member tabStripFooter(node: VirtualNode<TabView>, value: string) =
            createDependencyProperty str_tabStripFooterPropertyId TabView.TabStripFooterProperty value
            |> node.set

        [<Extension>]
        static member tabStripFooter<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TabView>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_tabStripFooterPropertyId
                value
                (fun element child -> (element :?> TabView).TabStripFooter <- child)
                (fun element -> (element :?> TabView).TabStripFooter :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member tabStripFooterTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TabView>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty tabStripFooterTemplatePropertyId TabView.TabStripFooterTemplateProperty
            |> node.set

        [<Extension>]
        static member tabStripHeader(node: VirtualNode<TabView>, value: string) =
            createDependencyProperty str_tabStripHeaderPropertyId TabView.TabStripHeaderProperty value
            |> node.set

        [<Extension>]
        static member tabStripHeader<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TabView>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_tabStripHeaderPropertyId
                value
                (fun element child -> (element :?> TabView).TabStripHeader <- child)
                (fun element -> (element :?> TabView).TabStripHeader :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member tabStripHeaderTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TabView>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty tabStripHeaderTemplatePropertyId TabView.TabStripHeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member tabWidthMode(node: VirtualNode<TabView>, value: TabViewWidthMode) =
            createDependencyProperty tabWidthModePropertyId TabView.TabWidthModeProperty value
            |> node.set

        [<Extension>]
        static member onAddTabButtonClick(node: VirtualNode<TabView>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "AddTabButtonClick"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, obj>(fun _ e -> handler e)
                    tabView.add_AddTabButtonClick h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_AddTabButtonClick h }
                )
            })

        [<Extension>]
        static member onExternalTornOutTabsDropped(node: VirtualNode<TabView>, handler: TabViewExternalTornOutTabsDroppedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ExternalTornOutTabsDropped"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewExternalTornOutTabsDroppedEventArgs>(fun _ e -> handler e)
                    tabView.add_ExternalTornOutTabsDropped h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_ExternalTornOutTabsDropped h }
                )
            })

        [<Extension>]
        static member onExternalTornOutTabsDropping(node: VirtualNode<TabView>, handler: TabViewExternalTornOutTabsDroppingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ExternalTornOutTabsDropping"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewExternalTornOutTabsDroppingEventArgs>(fun _ e -> handler e)
                    tabView.add_ExternalTornOutTabsDropping h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_ExternalTornOutTabsDropping h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<TabView>, handler: SelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = SelectionChangedEventHandler(fun _ e -> handler e)
                    tabView.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = tabView.SelectionChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onTabCloseRequested(node: VirtualNode<TabView>, handler: TabViewTabCloseRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabCloseRequested"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewTabCloseRequestedEventArgs>(fun _ e -> handler e)
                    tabView.add_TabCloseRequested h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_TabCloseRequested h }
                )
            })

        [<Extension>]
        static member onTabDragCompleted(node: VirtualNode<TabView>, handler: TabViewTabDragCompletedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabDragCompleted"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewTabDragCompletedEventArgs>(fun _ e -> handler e)
                    tabView.add_TabDragCompleted h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_TabDragCompleted h }
                )
            })

        [<Extension>]
        static member onTabDragStarting(node: VirtualNode<TabView>, handler: TabViewTabDragStartingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabDragStarting"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewTabDragStartingEventArgs>(fun _ e -> handler e)
                    tabView.add_TabDragStarting h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_TabDragStarting h }
                )
            })

        [<Extension>]
        static member onTabDroppedOutside(node: VirtualNode<TabView>, handler: TabViewTabDroppedOutsideEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabDroppedOutside"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewTabDroppedOutsideEventArgs>(fun _ e -> handler e)
                    tabView.add_TabDroppedOutside h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_TabDroppedOutside h }
                )
            })

        [<Extension>]
        static member onTabItemsChanged(node: VirtualNode<TabView>, handler: IVectorChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabItemsChanged"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, IVectorChangedEventArgs>(fun _ e -> handler e)
                    tabView.add_TabItemsChanged h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_TabItemsChanged h }
                )
            })

        [<Extension>]
        static member onTabStripDragOver(node: VirtualNode<TabView>, handler: DragEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabStripDragOver"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = DragEventHandler(fun _ e -> handler e)
                    tabView.TabStripDragOver.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = tabView.TabStripDragOver.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onTabStripDrop(node: VirtualNode<TabView>, handler: DragEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabStripDrop"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = DragEventHandler(fun _ e -> handler e)
                    tabView.TabStripDrop.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = tabView.TabStripDrop.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onTabTearOutRequested(node: VirtualNode<TabView>, handler: TabViewTabTearOutRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabTearOutRequested"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewTabTearOutRequestedEventArgs>(fun _ e -> handler e)
                    tabView.add_TabTearOutRequested h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_TabTearOutRequested h }
                )
            })

        [<Extension>]
        static member onTabTearOutWindowRequested(node: VirtualNode<TabView>, handler: TabViewTabTearOutWindowRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TabTearOutWindowRequested"
                Subscribe = (fun uiElement ->
                    let tabView = uiElement :?> TabView
                    let h = TypedEventHandler<TabView, TabViewTabTearOutWindowRequestedEventArgs>(fun _ e -> handler e)
                    tabView.add_TabTearOutWindowRequested h
                    { new IDisposable with
                        member _.Dispose() = tabView.remove_TabTearOutWindowRequested h }
                )
            })

    let tabView () =
        createNode<TabView> tabViewNodeId
