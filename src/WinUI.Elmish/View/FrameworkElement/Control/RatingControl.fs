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


[<AutoOpen>]
module RatingControlMod =

    let private ratingControlNodeId = getNextId ()

    registerDefaultRenderer<RatingControl> ratingControlNodeId

    let private captionPropertyId = getNextId ()
    let private initialSetValuePropertyId = getNextId ()
    let private isClearEnabledPropertyId = getNextId ()
    let private isReadOnlyPropertyId = getNextId ()
    let private itemInfoPropertyId = getNextId ()
    let private maxRatingPropertyId = getNextId ()
    let private placeholderValuePropertyId = getNextId ()
    let private valuePropertyId = getNextId ()

    [<Extension>]
    type RatingControlExtensions =

        [<Extension>]
        static member caption(node: VirtualNode<RatingControl>, value: string) =
            createDependencyProperty captionPropertyId RatingControl.CaptionProperty value
            |> node.set

        [<Extension>]
        static member initialSetValue(node: VirtualNode<RatingControl>, value: int) =
            createDependencyProperty initialSetValuePropertyId RatingControl.InitialSetValueProperty value
            |> node.set

        [<Extension>]
        static member isClearEnabled(node: VirtualNode<RatingControl>, value: bool) =
            createDependencyProperty isClearEnabledPropertyId RatingControl.IsClearEnabledProperty value
            |> node.set

        [<Extension>]
        static member isReadOnly(node: VirtualNode<RatingControl>, value: bool) =
            createDependencyProperty isReadOnlyPropertyId RatingControl.IsReadOnlyProperty value
            |> node.set

        [<Extension>]
        static member itemInfo(node: VirtualNode<RatingControl>, value: RatingItemInfo) =
            createDependencyProperty itemInfoPropertyId RatingControl.ItemInfoProperty value
            |> node.set

        [<Extension>]
        static member maxRating(node: VirtualNode<RatingControl>, value: int) =
            createDependencyProperty maxRatingPropertyId RatingControl.MaxRatingProperty value
            |> node.set

        [<Extension>]
        static member placeholderValue(node: VirtualNode<RatingControl>, value: float) =
            createDependencyProperty placeholderValuePropertyId RatingControl.PlaceholderValueProperty value
            |> node.set

        [<Extension>]
        static member value(node: VirtualNode<RatingControl>, value: float) =
            createDependencyProperty valuePropertyId RatingControl.ValueProperty value
            |> node.set

        [<Extension>]
        static member onValueChanged(node: VirtualNode<RatingControl>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ValueChanged"
                Subscribe = (fun uiElement ->
                    let ratingControl = uiElement :?> RatingControl
                    let h = TypedEventHandler<RatingControl, obj>(fun _ e -> handler e)
                    ratingControl.add_ValueChanged h
                    { new IDisposable with
                        member _.Dispose() = ratingControl.remove_ValueChanged h }
                )
            })

    let ratingControl () =
        createNode<RatingControl> ratingControlNodeId
