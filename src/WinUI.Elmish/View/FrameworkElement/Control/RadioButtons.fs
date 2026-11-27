namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property ItemsProperty: read-only


[<AutoOpen>]
module RadioButtonsMod =

    let private radioButtonsNodeId = getNextId ()

    registerDefaultRenderer<RadioButtons> radioButtonsNodeId

    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private itemTemplatePropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()
    let private maxColumnsPropertyId = getNextId ()
    let private selectedIndexPropertyId = getNextId ()
    let private selectedItemPropertyId = getNextId ()

    [<Extension>]
    type RadioButtonsExtensions =

        [<Extension>]
        static member header(node: VirtualNode<RadioButtons>, value: string) =
            createDependencyProperty str_headerPropertyId RadioButtons.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<RadioButtons>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> RadioButtons).Header <- child)
                (fun element -> (element :?> RadioButtons).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<RadioButtons>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId RadioButtons.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<RadioButtons>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty itemTemplatePropertyId RadioButtons.ItemTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemsSource<'source, 'key>
            (node: VirtualNode<RadioButtons>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> RadioButtons).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> RadioButtons).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

        [<Extension>]
        static member maxColumns(node: VirtualNode<RadioButtons>, value: int) =
            createDependencyProperty maxColumnsPropertyId RadioButtons.MaxColumnsProperty value
            |> node.set

        [<Extension>]
        static member selectedIndex(node: VirtualNode<RadioButtons>, value: int) =
            createDependencyProperty selectedIndexPropertyId RadioButtons.SelectedIndexProperty value
            |> node.set

        [<Extension>]
        static member selectedItem(node: VirtualNode<RadioButtons>, value: obj) =
            createDependencyProperty selectedItemPropertyId RadioButtons.SelectedItemProperty value
            |> node.set

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<RadioButtons>, handler: RadioButtons -> SelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let radioButtons = uiElement :?> RadioButtons
                    let h = SelectionChangedEventHandler(fun _ e -> handler radioButtons e)
                    radioButtons.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = radioButtons.SelectionChanged.RemoveHandler h }
                )
            })

    let radioButtons () =
        createNode<RadioButtons> radioButtonsNodeId
