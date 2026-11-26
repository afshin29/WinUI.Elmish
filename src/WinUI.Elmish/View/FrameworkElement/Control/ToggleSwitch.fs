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

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module ToggleSwitchMod =

    let private toggleSwitchNodeId = getNextId ()

    registerDefaultRenderer<ToggleSwitch> toggleSwitchNodeId

    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private isOnPropertyId = getNextId ()
    let private str_offContentPropertyId = getNextId ()
    let private vnode_offContentPropertyId = getNextId ()
    let private offContentTemplatePropertyId = getNextId ()
    let private str_onContentPropertyId = getNextId ()
    let private vnode_onContentPropertyId = getNextId ()
    let private onContentTemplatePropertyId = getNextId ()

    [<Extension>]
    type ToggleSwitchExtensions =

        [<Extension>]
        static member header(node: VirtualNode<ToggleSwitch>, value: string) =
            createDependencyProperty str_headerPropertyId ToggleSwitch.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<ToggleSwitch>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> ToggleSwitch).Header <- child)
                (fun element -> (element :?> ToggleSwitch).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<ToggleSwitch>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId ToggleSwitch.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member isOn(node: VirtualNode<ToggleSwitch>, value: bool) =
            createDependencyProperty isOnPropertyId ToggleSwitch.IsOnProperty value
            |> node.set

        [<Extension>]
        static member offContent(node: VirtualNode<ToggleSwitch>, value: string) =
            createDependencyProperty str_offContentPropertyId ToggleSwitch.OffContentProperty value
            |> node.set

        [<Extension>]
        static member offContent<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<ToggleSwitch>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_offContentPropertyId
                value
                (fun element child -> (element :?> ToggleSwitch).OffContent <- child)
                (fun element -> (element :?> ToggleSwitch).OffContent :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member offContentTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<ToggleSwitch>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty offContentTemplatePropertyId ToggleSwitch.OffContentTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member onContent(node: VirtualNode<ToggleSwitch>, value: string) =
            createDependencyProperty str_onContentPropertyId ToggleSwitch.OnContentProperty value
            |> node.set

        [<Extension>]
        static member onContent<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<ToggleSwitch>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_onContentPropertyId
                value
                (fun element child -> (element :?> ToggleSwitch).OnContent <- child)
                (fun element -> (element :?> ToggleSwitch).OnContent :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member onContentTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<ToggleSwitch>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty onContentTemplatePropertyId ToggleSwitch.OnContentTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member onToggled(node: VirtualNode<ToggleSwitch>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Toggled"
                Subscribe = (fun uiElement ->
                    let toggleSwitch = uiElement :?> ToggleSwitch
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    toggleSwitch.Toggled.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = toggleSwitch.Toggled.RemoveHandler h }
                )
            })

    let toggleSwitch () =
        createNode<ToggleSwitch> toggleSwitchNodeId
