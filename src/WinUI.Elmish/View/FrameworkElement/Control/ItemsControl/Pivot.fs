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
module PivotMod =

    let private pivotNodeId = getNextId ()

    registerDefaultRenderer<Pivot> pivotNodeId

    let private headerFocusVisualPlacementPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private isHeaderItemsCarouselEnabledPropertyId = getNextId ()
    let private isLockedPropertyId = getNextId ()
    let private str_leftHeaderPropertyId = getNextId ()
    let private vnode_leftHeaderPropertyId = getNextId ()
    let private leftHeaderTemplatePropertyId = getNextId ()
    let private str_rightHeaderPropertyId = getNextId ()
    let private vnode_rightHeaderPropertyId = getNextId ()
    let private rightHeaderTemplatePropertyId = getNextId ()
    let private selectedIndexPropertyId = getNextId ()
    let private selectedItemPropertyId = getNextId ()
    let private slideInAnimationGroupPropertyId = getNextId ()
    let private str_titlePropertyId = getNextId ()
    let private vnode_titlePropertyId = getNextId ()
    let private titleTemplatePropertyId = getNextId ()

    [<Extension>]
    type PivotExtensions =

        [<Extension>]
        static member headerFocusVisualPlacement(node: VirtualNode<Pivot>, value: PivotHeaderFocusVisualPlacement) =
            createDependencyProperty headerFocusVisualPlacementPropertyId Pivot.HeaderFocusVisualPlacementProperty value
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Pivot>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId Pivot.HeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member isHeaderItemsCarouselEnabled(node: VirtualNode<Pivot>, value: bool) =
            createDependencyProperty isHeaderItemsCarouselEnabledPropertyId Pivot.IsHeaderItemsCarouselEnabledProperty value
            |> node.set

        [<Extension>]
        static member isLocked(node: VirtualNode<Pivot>, value: bool) =
            createDependencyProperty isLockedPropertyId Pivot.IsLockedProperty value
            |> node.set

        [<Extension>]
        static member leftHeader(node: VirtualNode<Pivot>, value: string) =
            createDependencyProperty str_leftHeaderPropertyId Pivot.LeftHeaderProperty value
            |> node.set

        [<Extension>]
        static member leftHeader<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Pivot>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_leftHeaderPropertyId
                value
                (fun element child -> (element :?> Pivot).LeftHeader <- child)
                (fun element -> (element :?> Pivot).LeftHeader :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member leftHeaderTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Pivot>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty leftHeaderTemplatePropertyId Pivot.LeftHeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member rightHeader(node: VirtualNode<Pivot>, value: string) =
            createDependencyProperty str_rightHeaderPropertyId Pivot.RightHeaderProperty value
            |> node.set

        [<Extension>]
        static member rightHeader<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Pivot>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_rightHeaderPropertyId
                value
                (fun element child -> (element :?> Pivot).RightHeader <- child)
                (fun element -> (element :?> Pivot).RightHeader :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member rightHeaderTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Pivot>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty rightHeaderTemplatePropertyId Pivot.RightHeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member selectedIndex(node: VirtualNode<Pivot>, value: int) =
            createDependencyProperty selectedIndexPropertyId Pivot.SelectedIndexProperty value
            |> node.set

        [<Extension>]
        static member selectedItem(node: VirtualNode<Pivot>, value: obj) =
            createDependencyProperty selectedItemPropertyId Pivot.SelectedItemProperty value
            |> node.set

        [<Extension>]
        static member pivot_slideInAnimationGroup<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: PivotSlideInAnimationGroup) =
            createDependencyProperty slideInAnimationGroupPropertyId Pivot.SlideInAnimationGroupProperty value
            |> node.set

        [<Extension>]
        static member title(node: VirtualNode<Pivot>, value: string) =
            createDependencyProperty str_titlePropertyId Pivot.TitleProperty value
            |> node.set

        [<Extension>]
        static member title<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Pivot>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_titlePropertyId
                value
                (fun element child -> (element :?> Pivot).Title <- child)
                (fun element -> (element :?> Pivot).Title :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member titleTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Pivot>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty titleTemplatePropertyId Pivot.TitleTemplateProperty
            |> node.set

        [<Extension>]
        static member onPivotItemLoaded(node: VirtualNode<Pivot>, handler: PivotItemEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PivotItemLoaded"
                Subscribe = (fun uiElement ->
                    let pivot = uiElement :?> Pivot
                    let h = TypedEventHandler<Pivot, PivotItemEventArgs>(fun _ e -> handler e)
                    pivot.add_PivotItemLoaded h
                    { new IDisposable with
                        member _.Dispose() = pivot.remove_PivotItemLoaded h }
                )
            })

        [<Extension>]
        static member onPivotItemLoading(node: VirtualNode<Pivot>, handler: PivotItemEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PivotItemLoading"
                Subscribe = (fun uiElement ->
                    let pivot = uiElement :?> Pivot
                    let h = TypedEventHandler<Pivot, PivotItemEventArgs>(fun _ e -> handler e)
                    pivot.add_PivotItemLoading h
                    { new IDisposable with
                        member _.Dispose() = pivot.remove_PivotItemLoading h }
                )
            })

        [<Extension>]
        static member onPivotItemUnloaded(node: VirtualNode<Pivot>, handler: PivotItemEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PivotItemUnloaded"
                Subscribe = (fun uiElement ->
                    let pivot = uiElement :?> Pivot
                    let h = TypedEventHandler<Pivot, PivotItemEventArgs>(fun _ e -> handler e)
                    pivot.add_PivotItemUnloaded h
                    { new IDisposable with
                        member _.Dispose() = pivot.remove_PivotItemUnloaded h }
                )
            })

        [<Extension>]
        static member onPivotItemUnloading(node: VirtualNode<Pivot>, handler: PivotItemEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PivotItemUnloading"
                Subscribe = (fun uiElement ->
                    let pivot = uiElement :?> Pivot
                    let h = TypedEventHandler<Pivot, PivotItemEventArgs>(fun _ e -> handler e)
                    pivot.add_PivotItemUnloading h
                    { new IDisposable with
                        member _.Dispose() = pivot.remove_PivotItemUnloading h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<Pivot>, handler: SelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let pivot = uiElement :?> Pivot
                    let h = SelectionChangedEventHandler(fun _ e -> handler e)
                    pivot.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = pivot.SelectionChanged.RemoveHandler h }
                )
            })

    let pivot () =
        createNode<Pivot> pivotNodeId
