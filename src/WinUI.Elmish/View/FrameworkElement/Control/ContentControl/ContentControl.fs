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
        static member contentTemplate<'m, 'source, 'el when 'm :> ContentControl and 'm: not struct and 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<'m>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty contentTemplatePropertyId ContentControl.ContentTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member contentTemplateSelector<'m, 'source, 'el when 'm :> ContentControl and 'm: not struct and 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<'m>, templates: ('source -> VirtualNode<'el>) seq, selector: ('source -> int), ?diffKey: obj) =
            createTemplateSelector templates selector
            |> createDependencyProperty contentTemplateSelectorPropertyId ContentControl.ContentTemplateSelectorProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member contentTransitions<'m when 'm :> ContentControl and 'm: not struct>
            (node: VirtualNode<'m>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty contentTransitionsPropertyId ContentControl.ContentTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

    let contentControl () =
        createNode<ContentControl> contentControlNodeId
