namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module HubSectionMod =

    let private hubSectionNodeId = getNextId ()

    registerDefaultRenderer<HubSection> hubSectionNodeId

    let private contentTemplatePropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private isHeaderInteractivePropertyId = getNextId ()

    [<Extension>]
    type HubSectionExtensions =

        [<Extension>]
        static member contentTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<HubSection>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty contentTemplatePropertyId HubSection.ContentTemplateProperty
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<HubSection>, value: string) =
            createDependencyProperty str_headerPropertyId HubSection.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<HubSection>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> HubSection).Header <- child)
                (fun element -> (element :?> HubSection).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<HubSection>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId HubSection.HeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member isHeaderInteractive(node: VirtualNode<HubSection>, value: bool) =
            createDependencyProperty isHeaderInteractivePropertyId HubSection.IsHeaderInteractiveProperty value
            |> node.set

    let hubSection () =
        createNode<HubSection> hubSectionNodeId
