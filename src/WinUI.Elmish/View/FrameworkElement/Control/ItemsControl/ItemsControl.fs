namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property IsGroupingProperty: read-only
// skipped property GroupStyle: read-only
// skipped property ItemContainerGenerator: read-only
// skipped property Items: read-only
// skipped property ItemsPanelRoot: read-only


[<AutoOpen>]
module ItemsControlMod =

    let private itemsControlNodeId = getNextId ()

    registerDefaultRenderer<ItemsControl> itemsControlNodeId

    let private displayMemberPathPropertyId = getNextId ()
    let private groupStyleSelectorPropertyId = getNextId ()
    let private itemContainerStylePropertyId = getNextId ()
    let private itemContainerStyleSelectorPropertyId = getNextId ()
    let private itemContainerTransitionsPropertyId = getNextId ()
    let private itemTemplatePropertyId = getNextId ()
    let private itemTemplateSelectorPropertyId = getNextId ()
    let private itemsPanelPropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()

    [<Extension>]
    type ItemsControlExtensions =

        [<Extension>]
        static member displayMemberPath<'m when 'm :> ItemsControl and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty displayMemberPathPropertyId ItemsControl.DisplayMemberPathProperty value
            |> node.set

        [<Extension>]
        static member groupStyleSelector<'m when 'm :> ItemsControl and 'm: not struct>
            (node: VirtualNode<'m>, value: GroupStyleSelector) =
            createDependencyProperty groupStyleSelectorPropertyId ItemsControl.GroupStyleSelectorProperty value
            |> node.set

        [<Extension>]
        static member itemContainerStyle<'m when 'm :> ItemsControl and 'm: not struct>
            (node: VirtualNode<'m>, value: Style) =
            createDependencyProperty itemContainerStylePropertyId ItemsControl.ItemContainerStyleProperty value
            |> node.set

        [<Extension>]
        static member itemContainerStyleSelector<'m, 'source when 'm :> ItemsControl and 'm: not struct>
            (node: VirtualNode<'m>, styles: Style seq, selector: ('source -> int), ?diffKey: obj) =
            InternalStyleSelector(styles, selector)
            |> createDependencyProperty itemContainerStyleSelectorPropertyId ItemsControl.ItemContainerStyleSelectorProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemContainerTransitions<'m when 'm :> ItemsControl and 'm: not struct>
            (node: VirtualNode<'m>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty itemContainerTransitionsPropertyId ItemsControl.ItemContainerTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemTemplate<'m, 'source, 'el when 'm :> ItemsControl and 'm: not struct and 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<'m>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty itemTemplatePropertyId ItemsControl.ItemTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemTemplateSelector<'m, 'source, 'el when 'm :> ItemsControl and 'm: not struct and 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<'m>, templates: ('source -> VirtualNode<'el>) seq, selector: ('source -> int), ?diffKey: obj) =
            createTemplateSelector templates selector
            |> createDependencyProperty itemTemplateSelectorPropertyId ItemsControl.ItemTemplateSelectorProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemsPanel<'m when 'm :> ItemsControl and 'm: not struct>
            (node: VirtualNode<'m>, value: ItemsPanelTemplate) =
            createDependencyProperty itemsPanelPropertyId ItemsControl.ItemsPanelProperty value
            |> node.set

        [<Extension>]
        static member itemsSource<'m, 'source, 'key when 'm :> ItemsControl and 'm: not struct>
            (node: VirtualNode<'m>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> ItemsControl).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> ItemsControl).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

    let itemsControl () =
        createNode<ItemsControl> itemsControlNodeId
