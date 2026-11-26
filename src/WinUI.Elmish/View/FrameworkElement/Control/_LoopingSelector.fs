namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Collections.Generic
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module LoopingSelectorMod =

    let private itemHeightPropertyId = getNextId ()
    let private itemTemplatePropertyId = getNextId ()
    let private itemWidthPropertyId = getNextId ()
    let private itemsPropertyId = getNextId ()
    let private selectedIndexPropertyId = getNextId ()
    let private selectedItemPropertyId = getNextId ()
    let private shouldLoopPropertyId = getNextId ()

    [<Extension>]
    type LoopingSelectorExtensions =

        [<Extension>]
        static member itemHeight(node: VirtualNode<LoopingSelector>, value: int) =
            createDependencyProperty itemHeightPropertyId LoopingSelector.ItemHeightProperty value
            |> node.set

        [<Extension>]
        static member itemTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<LoopingSelector>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty itemTemplatePropertyId LoopingSelector.ItemTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member itemWidth(node: VirtualNode<LoopingSelector>, value: int) =
            createDependencyProperty itemWidthPropertyId LoopingSelector.ItemWidthProperty value
            |> node.set

        [<Extension>]
        static member items(node: VirtualNode<LoopingSelector>, value: IList<obj>) =
            createDependencyProperty itemsPropertyId LoopingSelector.ItemsProperty value
            |> node.set

        [<Extension>]
        static member selectedIndex(node: VirtualNode<LoopingSelector>, value: int) =
            createDependencyProperty selectedIndexPropertyId LoopingSelector.SelectedIndexProperty value
            |> node.set

        [<Extension>]
        static member selectedItem(node: VirtualNode<LoopingSelector>, value: obj) =
            createDependencyProperty selectedItemPropertyId LoopingSelector.SelectedItemProperty value
            |> node.set

        [<Extension>]
        static member shouldLoop(node: VirtualNode<LoopingSelector>, value: bool) =
            createDependencyProperty shouldLoopPropertyId LoopingSelector.ShouldLoopProperty value
            |> node.set

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<LoopingSelector>, handler: SelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let loopingSelector = uiElement :?> LoopingSelector
                    let h = SelectionChangedEventHandler(fun _ e -> handler e)
                    loopingSelector.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = loopingSelector.SelectionChanged.RemoveHandler h }
                )
            })
