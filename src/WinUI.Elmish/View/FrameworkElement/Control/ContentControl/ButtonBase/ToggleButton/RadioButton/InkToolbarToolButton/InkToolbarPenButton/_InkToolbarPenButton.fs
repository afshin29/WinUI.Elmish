namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Collections.Generic
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property SelectedBrushProperty: read-only


[<AutoOpen>]
module InkToolbarPenButtonMod =

    let private maxStrokeWidthPropertyId = getNextId ()
    let private minStrokeWidthPropertyId = getNextId ()
    let private palettePropertyId = getNextId ()
    let private selectedBrushIndexPropertyId = getNextId ()
    let private selectedStrokeWidthPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarPenButtonExtensions =

        [<Extension>]
        static member maxStrokeWidth<'m when 'm :> InkToolbarPenButton and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty maxStrokeWidthPropertyId InkToolbarPenButton.MaxStrokeWidthProperty value
            |> node.set

        [<Extension>]
        static member minStrokeWidth<'m when 'm :> InkToolbarPenButton and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty minStrokeWidthPropertyId InkToolbarPenButton.MinStrokeWidthProperty value
            |> node.set

        [<Extension>]
        static member palette<'m when 'm :> InkToolbarPenButton and 'm: not struct>
            (node: VirtualNode<'m>, value: IList<Brush>) =
            createDependencyProperty palettePropertyId InkToolbarPenButton.PaletteProperty value
            |> node.set

        [<Extension>]
        static member selectedBrushIndex<'m when 'm :> InkToolbarPenButton and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty selectedBrushIndexPropertyId InkToolbarPenButton.SelectedBrushIndexProperty value
            |> node.set

        [<Extension>]
        static member selectedStrokeWidth<'m when 'm :> InkToolbarPenButton and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty selectedStrokeWidthPropertyId InkToolbarPenButton.SelectedStrokeWidthProperty value
            |> node.set
