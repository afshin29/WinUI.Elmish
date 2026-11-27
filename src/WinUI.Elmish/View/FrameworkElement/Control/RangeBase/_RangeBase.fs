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
module RangeBaseMod =

    let private largeChangePropertyId = getNextId ()
    let private maximumPropertyId = getNextId ()
    let private minimumPropertyId = getNextId ()
    let private smallChangePropertyId = getNextId ()
    let private valuePropertyId = getNextId ()

    [<Extension>]
    type RangeBaseExtensions =

        [<Extension>]
        static member largeChange<'m when 'm :> RangeBase and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty largeChangePropertyId RangeBase.LargeChangeProperty value
            |> node.set

        [<Extension>]
        static member maximum<'m when 'm :> RangeBase and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty maximumPropertyId RangeBase.MaximumProperty value
            |> node.set

        [<Extension>]
        static member minimum<'m when 'm :> RangeBase and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty minimumPropertyId RangeBase.MinimumProperty value
            |> node.set

        [<Extension>]
        static member smallChange<'m when 'm :> RangeBase and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty smallChangePropertyId RangeBase.SmallChangeProperty value
            |> node.set

        [<Extension>]
        static member value<'m when 'm :> RangeBase and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty valuePropertyId RangeBase.ValueProperty value
            |> node.set

        [<Extension>]
        static member onValueChanged<'m when 'm :> RangeBase and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> RangeBaseValueChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ValueChanged"
                Subscribe = (fun uiElement ->
                    let rangeBase = uiElement :?> RangeBase
                    let sender = uiElement :?> 'm
                    let h = RangeBaseValueChangedEventHandler(fun _ e -> handler sender e)
                    rangeBase.ValueChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = rangeBase.ValueChanged.RemoveHandler h }
                )
            })
