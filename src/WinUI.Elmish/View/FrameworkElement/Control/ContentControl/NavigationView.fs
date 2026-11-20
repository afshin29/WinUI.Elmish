namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property DisplayModeProperty: read-only
// skipped dependency property FooterMenuItemsProperty: read-only
// skipped dependency property MenuItemsProperty: read-only
// skipped dependency property SettingsItemProperty: read-only
// skipped dependency property TemplateSettingsProperty: read-only


[<AutoOpen>]
module NavigationViewMod =

    let private navigationViewNodeId = getNextId ()

    registerDefaultRenderer<NavigationView> navigationViewNodeId

    let private alwaysShowHeaderPropertyId = getNextId ()
    let private autoSuggestBoxPropertyId = getNextId ()
    let private compactModeThresholdWidthPropertyId = getNextId ()
    let private compactPaneLengthPropertyId = getNextId ()
    let private contentOverlayPropertyId = getNextId ()
    let private expandedModeThresholdWidthPropertyId = getNextId ()
    let private footerMenuItemsSourcePropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private isBackButtonVisiblePropertyId = getNextId ()
    let private isBackEnabledPropertyId = getNextId ()
    let private isPaneOpenPropertyId = getNextId ()
    let private isPaneToggleButtonVisiblePropertyId = getNextId ()
    let private isPaneVisiblePropertyId = getNextId ()
    let private isSettingsVisiblePropertyId = getNextId ()
    let private isTitleBarAutoPaddingEnabledPropertyId = getNextId ()
    let private menuItemContainerStylePropertyId = getNextId ()
    let private menuItemContainerStyleSelectorPropertyId = getNextId ()
    let private menuItemTemplatePropertyId = getNextId ()
    let private menuItemTemplateSelectorPropertyId = getNextId ()
    let private menuItemsSourcePropertyId = getNextId ()
    let private menuItemsPropertyId = getNextId ()
    let private openPaneLengthPropertyId = getNextId ()
    let private overflowLabelModePropertyId = getNextId ()
    let private paneCustomContentPropertyId = getNextId ()
    let private paneDisplayModePropertyId = getNextId ()
    let private paneFooterPropertyId = getNextId ()
    let private paneHeaderPropertyId = getNextId ()
    let private paneTitlePropertyId = getNextId ()
    let private paneToggleButtonStylePropertyId = getNextId ()
    let private selectedItemPropertyId = getNextId ()
    let private selectionFollowsFocusPropertyId = getNextId ()
    let private shoulderNavigationEnabledPropertyId = getNextId ()

    [<Extension>]
    type NavigationViewExtensions =

        [<Extension>]
        static member alwaysShowHeader(node: VirtualNode<NavigationView>, value: bool) =
            createDependencyProperty alwaysShowHeaderPropertyId NavigationView.AlwaysShowHeaderProperty value
            |> node.set

        [<Extension>]
        static member autoSuggestBox(node: VirtualNode<NavigationView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                autoSuggestBoxPropertyId
                value
                (fun element child -> (element :?> NavigationView).AutoSuggestBox <- (child :?> AutoSuggestBox))
                (fun element -> (element :?> NavigationView).AutoSuggestBox :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member compactModeThresholdWidth(node: VirtualNode<NavigationView>, value: float) =
            createDependencyProperty compactModeThresholdWidthPropertyId NavigationView.CompactModeThresholdWidthProperty value
            |> node.set

        [<Extension>]
        static member compactPaneLength(node: VirtualNode<NavigationView>, value: float) =
            createDependencyProperty compactPaneLengthPropertyId NavigationView.CompactPaneLengthProperty value
            |> node.set

        [<Extension>]
        static member contentOverlay(node: VirtualNode<NavigationView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                contentOverlayPropertyId
                value
                (fun element child -> (element :?> NavigationView).ContentOverlay <- child)
                (fun element -> (element :?> NavigationView).ContentOverlay)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member expandedModeThresholdWidth(node: VirtualNode<NavigationView>, value: float) =
            createDependencyProperty expandedModeThresholdWidthPropertyId NavigationView.ExpandedModeThresholdWidthProperty value
            |> node.set

        [<Extension>]
        static member footerMenuItemsSource<'source, 'key>
            (node: VirtualNode<NavigationView>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                footerMenuItemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> NavigationView).FooterMenuItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> NavigationView).FooterMenuItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<NavigationView>, value: string) =
            createDependencyProperty str_headerPropertyId NavigationView.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<NavigationView>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> NavigationView).Header <- child)
                (fun element -> (element :?> NavigationView).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<NavigationView>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId NavigationView.HeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member isBackButtonVisible(node: VirtualNode<NavigationView>, value: NavigationViewBackButtonVisible) =
            createDependencyProperty isBackButtonVisiblePropertyId NavigationView.IsBackButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isBackEnabled(node: VirtualNode<NavigationView>, value: bool) =
            createDependencyProperty isBackEnabledPropertyId NavigationView.IsBackEnabledProperty value
            |> node.set

        [<Extension>]
        static member isPaneOpen(node: VirtualNode<NavigationView>, value: bool) =
            createDependencyProperty isPaneOpenPropertyId NavigationView.IsPaneOpenProperty value
            |> node.set

        [<Extension>]
        static member isPaneToggleButtonVisible(node: VirtualNode<NavigationView>, value: bool) =
            createDependencyProperty isPaneToggleButtonVisiblePropertyId NavigationView.IsPaneToggleButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member isPaneVisible(node: VirtualNode<NavigationView>, value: bool) =
            createDependencyProperty isPaneVisiblePropertyId NavigationView.IsPaneVisibleProperty value
            |> node.set

        [<Extension>]
        static member isSettingsVisible(node: VirtualNode<NavigationView>, value: bool) =
            createDependencyProperty isSettingsVisiblePropertyId NavigationView.IsSettingsVisibleProperty value
            |> node.set

        [<Extension>]
        static member isTitleBarAutoPaddingEnabled(node: VirtualNode<NavigationView>, value: bool) =
            createDependencyProperty isTitleBarAutoPaddingEnabledPropertyId NavigationView.IsTitleBarAutoPaddingEnabledProperty value
            |> node.set

        [<Extension>]
        static member menuItemContainerStyle(node: VirtualNode<NavigationView>, value: Style) =
            createDependencyProperty menuItemContainerStylePropertyId NavigationView.MenuItemContainerStyleProperty value
            |> node.set

        [<Extension>]
        static member menuItemContainerStyleSelector(node: VirtualNode<NavigationView>, value: StyleSelector) =
            createDependencyProperty menuItemContainerStyleSelectorPropertyId NavigationView.MenuItemContainerStyleSelectorProperty value
            |> node.set

        [<Extension>]
        static member menuItemTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<NavigationView>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty menuItemTemplatePropertyId NavigationView.MenuItemTemplateProperty
            |> node.set

        [<Extension>]
        static member menuItemTemplateSelector<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<NavigationView>, templates: ('source -> 'vnode) seq, selector: ('source -> int), diffKey) =
            createTemplateSelector templates selector
            |> createDependencyProperty menuItemTemplateSelectorPropertyId NavigationView.MenuItemTemplateSelectorProperty
            |> fun p -> p.useDiffKey (Some diffKey)
            |> node.set

        [<Extension>]
        static member menuItemsSource<'source, 'key>
            (node: VirtualNode<NavigationView>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                menuItemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> NavigationView).MenuItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> NavigationView).MenuItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member menuItems'(node: VirtualNode<NavigationView>, value: IVirtualNode list) =
            createInnerChildrenProperty
                menuItemsPropertyId
                value
                (fun element children -> children |> List.iter (element :?> NavigationView).MenuItems.Add)
                (fun element ->
                    (element :?> NavigationView).MenuItems
                    |> Seq.cast<UIElement>
                    |> ResizeArray
                    :> IList<_>)
            |> node.set

        [<Extension>]
        static member openPaneLength(node: VirtualNode<NavigationView>, value: float) =
            createDependencyProperty openPaneLengthPropertyId NavigationView.OpenPaneLengthProperty value
            |> node.set

        [<Extension>]
        static member overflowLabelMode(node: VirtualNode<NavigationView>, value: NavigationViewOverflowLabelMode) =
            createDependencyProperty overflowLabelModePropertyId NavigationView.OverflowLabelModeProperty value
            |> node.set

        [<Extension>]
        static member paneCustomContent(node: VirtualNode<NavigationView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                paneCustomContentPropertyId
                value
                (fun element child -> (element :?> NavigationView).PaneCustomContent <- child)
                (fun element -> (element :?> NavigationView).PaneCustomContent)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member paneDisplayMode(node: VirtualNode<NavigationView>, value: NavigationViewPaneDisplayMode) =
            createDependencyProperty paneDisplayModePropertyId NavigationView.PaneDisplayModeProperty value
            |> node.set

        [<Extension>]
        static member paneFooter(node: VirtualNode<NavigationView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                paneFooterPropertyId
                value
                (fun element child -> (element :?> NavigationView).PaneFooter <- child)
                (fun element -> (element :?> NavigationView).PaneFooter)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member paneHeader(node: VirtualNode<NavigationView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                paneHeaderPropertyId
                value
                (fun element child -> (element :?> NavigationView).PaneHeader <- child)
                (fun element -> (element :?> NavigationView).PaneHeader)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member paneTitle(node: VirtualNode<NavigationView>, value: string) =
            createDependencyProperty paneTitlePropertyId NavigationView.PaneTitleProperty value
            |> node.set

        [<Extension>]
        static member paneToggleButtonStyle(node: VirtualNode<NavigationView>, value: Style) =
            createDependencyProperty paneToggleButtonStylePropertyId NavigationView.PaneToggleButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member selectedItem(node: VirtualNode<NavigationView>, value: obj) =
            createDependencyProperty selectedItemPropertyId NavigationView.SelectedItemProperty value
            |> node.set

        [<Extension>]
        static member selectionFollowsFocus(node: VirtualNode<NavigationView>, value: NavigationViewSelectionFollowsFocus) =
            createDependencyProperty selectionFollowsFocusPropertyId NavigationView.SelectionFollowsFocusProperty value
            |> node.set

        [<Extension>]
        static member shoulderNavigationEnabled(node: VirtualNode<NavigationView>, value: NavigationViewShoulderNavigationEnabled) =
            createDependencyProperty shoulderNavigationEnabledPropertyId NavigationView.ShoulderNavigationEnabledProperty value
            |> node.set

        [<Extension>]
        static member onBackRequested(node: VirtualNode<NavigationView>, handler: NavigationViewBackRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "BackRequested"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, NavigationViewBackRequestedEventArgs>(fun _ e -> handler e)
                    navigationView.add_BackRequested h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_BackRequested h }
                )
            })

        [<Extension>]
        static member onCollapsed(node: VirtualNode<NavigationView>, handler: NavigationViewItemCollapsedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Collapsed"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, NavigationViewItemCollapsedEventArgs>(fun _ e -> handler e)
                    navigationView.add_Collapsed h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_Collapsed h }
                )
            })

        [<Extension>]
        static member onDisplayModeChanged(node: VirtualNode<NavigationView>, handler: NavigationViewDisplayModeChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DisplayModeChanged"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, NavigationViewDisplayModeChangedEventArgs>(fun _ e -> handler e)
                    navigationView.add_DisplayModeChanged h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_DisplayModeChanged h }
                )
            })

        [<Extension>]
        static member onExpanding(node: VirtualNode<NavigationView>, handler: NavigationViewItemExpandingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Expanding"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, NavigationViewItemExpandingEventArgs>(fun _ e -> handler e)
                    navigationView.add_Expanding h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_Expanding h }
                )
            })

        [<Extension>]
        static member onItemInvoked(node: VirtualNode<NavigationView>, handler: NavigationViewItemInvokedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ItemInvoked"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, NavigationViewItemInvokedEventArgs>(fun _ e -> handler e)
                    navigationView.add_ItemInvoked h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_ItemInvoked h }
                )
            })

        [<Extension>]
        static member onPaneClosed(node: VirtualNode<NavigationView>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneClosed"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, obj>(fun _ e -> handler e)
                    navigationView.add_PaneClosed h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_PaneClosed h }
                )
            })

        [<Extension>]
        static member onPaneClosing(node: VirtualNode<NavigationView>, handler: NavigationViewPaneClosingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneClosing"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, NavigationViewPaneClosingEventArgs>(fun _ e -> handler e)
                    navigationView.add_PaneClosing h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_PaneClosing h }
                )
            })

        [<Extension>]
        static member onPaneOpened(node: VirtualNode<NavigationView>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneOpened"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, obj>(fun _ e -> handler e)
                    navigationView.add_PaneOpened h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_PaneOpened h }
                )
            })

        [<Extension>]
        static member onPaneOpening(node: VirtualNode<NavigationView>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PaneOpening"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, obj>(fun _ e -> handler e)
                    navigationView.add_PaneOpening h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_PaneOpening h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<NavigationView>, handler: NavigationViewSelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let navigationView = uiElement :?> NavigationView
                    let h = TypedEventHandler<NavigationView, NavigationViewSelectionChangedEventArgs>(fun _ e -> handler e)
                    navigationView.add_SelectionChanged h
                    { new IDisposable with
                        member _.Dispose() = navigationView.remove_SelectionChanged h }
                )
            })

    let navigationView () =
        createNode<NavigationView> navigationViewNodeId
