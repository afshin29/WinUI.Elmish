namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module SelectorMod =

    let private isSynchronizedWithCurrentItemPropertyId = getNextId ()
    let private selectedIndexPropertyId = getNextId ()
    let private selectedItemPropertyId = getNextId ()
    let private selectedValuePathPropertyId = getNextId ()
    let private selectedValuePropertyId = getNextId ()

    [<Extension>]
    type SelectorExtensions =

        [<Extension>]
        static member isSynchronizedWithCurrentItem<'m when 'm :> Selector and 'm: not struct>
            (node: VirtualNode<'m>, value: Nullable<bool>) =
            createDependencyProperty isSynchronizedWithCurrentItemPropertyId Selector.IsSynchronizedWithCurrentItemProperty value
            |> node.set

        [<Extension>]
        static member selectedIndex<'m when 'm :> Selector and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty selectedIndexPropertyId Selector.SelectedIndexProperty value
            |> node.set

        [<Extension>]
        static member selectedItem<'m when 'm :> Selector and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty selectedItemPropertyId Selector.SelectedItemProperty value
            |> node.set

        [<Extension>]
        static member selectedValuePath<'m when 'm :> Selector and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty selectedValuePathPropertyId Selector.SelectedValuePathProperty value
            |> node.set

        [<Extension>]
        static member selectedValue<'m when 'm :> Selector and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty selectedValuePropertyId Selector.SelectedValueProperty value
            |> node.set

        [<Extension>]
        static member onSelectionChanged<'m when 'm :> Selector and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> SelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let selector = uiElement :?> Selector
                    let sender = uiElement :?> 'm
                    let h = SelectionChangedEventHandler(fun _ e -> handler sender e)
                    selector.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = selector.SelectionChanged.RemoveHandler h }
                )
            })
