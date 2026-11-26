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

// skipped dependency property TabViewTemplateSettingsProperty: read-only


[<AutoOpen>]
module TabViewItemMod =

    let private tabViewItemNodeId = getNextId ()

    registerDefaultRenderer<TabViewItem> tabViewItemNodeId

    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private iconSourcePropertyId = getNextId ()
    let private isClosablePropertyId = getNextId ()

    [<Extension>]
    type TabViewItemExtensions =

        [<Extension>]
        static member header(node: VirtualNode<TabViewItem>, value: string) =
            createDependencyProperty str_headerPropertyId TabViewItem.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TabViewItem>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> TabViewItem).Header <- child)
                (fun element -> (element :?> TabViewItem).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<TabViewItem>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId TabViewItem.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member iconSource(node: VirtualNode<TabViewItem>, value: IconSource) =
            createDependencyProperty iconSourcePropertyId TabViewItem.IconSourceProperty value
            |> node.set

        [<Extension>]
        static member isClosable(node: VirtualNode<TabViewItem>, value: bool) =
            createDependencyProperty isClosablePropertyId TabViewItem.IsClosableProperty value
            |> node.set

        [<Extension>]
        static member onCloseRequested(node: VirtualNode<TabViewItem>, handler: TabViewTabCloseRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CloseRequested"
                Subscribe = (fun uiElement ->
                    let tabViewItem = uiElement :?> TabViewItem
                    let h = TypedEventHandler<TabViewItem, TabViewTabCloseRequestedEventArgs>(fun _ e -> handler e)
                    tabViewItem.add_CloseRequested h
                    { new IDisposable with
                        member _.Dispose() = tabViewItem.remove_CloseRequested h }
                )
            })

    let tabViewItem () =
        createNode<TabViewItem> tabViewItemNodeId
