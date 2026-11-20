namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property ContentTemplateRoot: read-only


[<AutoOpen>]
module ContentControlMod =

    let private contentControlNodeId = getNextId ()

    registerDefaultRenderer<ContentControl> contentControlNodeId

    let private str_contentPropertyId = getNextId ()
    let private vnode_contentPropertyId = getNextId ()
    let private contentTemplatePropertyId = getNextId ()
    let private contentTemplateSelectorPropertyId = getNextId ()
    let private contentTransitionsPropertyId = getNextId ()

    [<Extension>]
    type ContentControlExtensions =

        [<Extension>]
        static member content<'m when 'm :> ContentControl and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty str_contentPropertyId ContentControl.ContentProperty value
            |> node.set

        [<Extension>]
        static member content<'m, 'vnode when 'm :> ContentControl and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_contentPropertyId
                value
                (fun element child -> (element :?> ContentControl).Content <- child)
                (fun element -> (element :?> ContentControl).Content :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member contentTemplate<'m, 'source, 'vnode when 'm :> ContentControl and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty contentTemplatePropertyId ContentControl.ContentTemplateProperty
            |> node.set

        [<Extension>]
        static member contentTemplateSelector<'m, 'source, 'vnode when 'm :> ContentControl and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, templates: ('source -> 'vnode) seq, selector: ('source -> int), diffKey) =
            createTemplateSelector templates selector
            |> createDependencyProperty contentTemplateSelectorPropertyId ContentControl.ContentTemplateSelectorProperty
            |> fun p -> p.useDiffKey (Some diffKey)
            |> node.set

        [<Extension>]
        static member contentTransitions<'m when 'm :> ContentControl and 'm: not struct>
            (node: VirtualNode<'m>, value: TransitionCollection) =
            createDependencyProperty contentTransitionsPropertyId ContentControl.ContentTransitionsProperty value
            |> node.set

    let contentControl () =
        createNode<ContentControl> contentControlNodeId
