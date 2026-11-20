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

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module ExpanderMod =

    let private expanderNodeId = getNextId ()

    registerDefaultRenderer<Expander> expanderNodeId

    let private expandDirectionPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private headerTemplateSelectorPropertyId = getNextId ()
    let private isExpandedPropertyId = getNextId ()

    [<Extension>]
    type ExpanderExtensions =

        [<Extension>]
        static member expandDirection(node: VirtualNode<Expander>, value: ExpandDirection) =
            createDependencyProperty expandDirectionPropertyId Expander.ExpandDirectionProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<Expander>, value: string) =
            createDependencyProperty str_headerPropertyId Expander.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Expander>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> Expander).Header <- child)
                (fun element -> (element :?> Expander).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Expander>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId Expander.HeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member headerTemplateSelector<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<Expander>, templates: ('source -> 'vnode) seq, selector: ('source -> int), diffKey) =
            createTemplateSelector templates selector
            |> createDependencyProperty headerTemplateSelectorPropertyId Expander.HeaderTemplateSelectorProperty
            |> fun p -> p.useDiffKey (Some diffKey)
            |> node.set

        [<Extension>]
        static member isExpanded(node: VirtualNode<Expander>, value: bool) =
            createDependencyProperty isExpandedPropertyId Expander.IsExpandedProperty value
            |> node.set

        [<Extension>]
        static member onCollapsed(node: VirtualNode<Expander>, handler: ExpanderCollapsedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Collapsed"
                Subscribe = (fun uiElement ->
                    let expander = uiElement :?> Expander
                    let h = TypedEventHandler<Expander, ExpanderCollapsedEventArgs>(fun _ e -> handler e)
                    expander.add_Collapsed h
                    { new IDisposable with
                        member _.Dispose() = expander.remove_Collapsed h }
                )
            })

        [<Extension>]
        static member onExpanding(node: VirtualNode<Expander>, handler: ExpanderExpandingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Expanding"
                Subscribe = (fun uiElement ->
                    let expander = uiElement :?> Expander
                    let h = TypedEventHandler<Expander, ExpanderExpandingEventArgs>(fun _ e -> handler e)
                    expander.add_Expanding h
                    { new IDisposable with
                        member _.Dispose() = expander.remove_Expanding h }
                )
            })

    let expander () =
        createNode<Expander> expanderNodeId
