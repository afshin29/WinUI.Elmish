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

// skipped dependency property ItemsProperty: read-only


[<AutoOpen>]
module SelectorBarMod =

    let private selectorBarNodeId = getNextId ()

    registerDefaultRenderer<SelectorBar> selectorBarNodeId

    let private selectedItemPropertyId = getNextId ()

    [<Extension>]
    type SelectorBarExtensions =

        [<Extension>]
        static member selectedItem(node: VirtualNode<SelectorBar>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                selectedItemPropertyId
                value
                (fun element child -> (element :?> SelectorBar).SelectedItem <- (child :?> SelectorBarItem))
                (fun element -> (element :?> SelectorBar).SelectedItem :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<SelectorBar>, handler: SelectorBarSelectionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let selectorBar = uiElement :?> SelectorBar
                    let h = TypedEventHandler<SelectorBar, SelectorBarSelectionChangedEventArgs>(fun _ e -> handler e)
                    selectorBar.add_SelectionChanged h
                    { new IDisposable with
                        member _.Dispose() = selectorBar.remove_SelectionChanged h }
                )
            })

    let selectorBar () =
        createNode<SelectorBar> selectorBarNodeId
