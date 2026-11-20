namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open Microsoft.UI.Xaml.Shapes
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property GeometryTransform: read-only


[<AutoOpen>]
module ShapeMod =

    let private fillPropertyId = getNextId ()
    let private stretchPropertyId = getNextId ()
    let private strokeDashArrayPropertyId = getNextId ()
    let private strokeDashCapPropertyId = getNextId ()
    let private strokeDashOffsetPropertyId = getNextId ()
    let private strokeEndLineCapPropertyId = getNextId ()
    let private strokeLineJoinPropertyId = getNextId ()
    let private strokeMiterLimitPropertyId = getNextId ()
    let private strokePropertyId = getNextId ()
    let private strokeStartLineCapPropertyId = getNextId ()
    let private strokeThicknessPropertyId = getNextId ()

    [<Extension>]
    type ShapeExtensions =

        [<Extension>]
        static member fill<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty fillPropertyId Shape.FillProperty value
            |> node.set

        [<Extension>]
        static member stretch<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: Stretch) =
            createDependencyProperty stretchPropertyId Shape.StretchProperty value
            |> node.set

        [<Extension>]
        static member strokeDashArray<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: DoubleCollection) =
            createDependencyProperty strokeDashArrayPropertyId Shape.StrokeDashArrayProperty value
            |> node.set

        [<Extension>]
        static member strokeDashCap<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: PenLineCap) =
            createDependencyProperty strokeDashCapPropertyId Shape.StrokeDashCapProperty value
            |> node.set

        [<Extension>]
        static member strokeDashOffset<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty strokeDashOffsetPropertyId Shape.StrokeDashOffsetProperty value
            |> node.set

        [<Extension>]
        static member strokeEndLineCap<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: PenLineCap) =
            createDependencyProperty strokeEndLineCapPropertyId Shape.StrokeEndLineCapProperty value
            |> node.set

        [<Extension>]
        static member strokeLineJoin<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: PenLineJoin) =
            createDependencyProperty strokeLineJoinPropertyId Shape.StrokeLineJoinProperty value
            |> node.set

        [<Extension>]
        static member strokeMiterLimit<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty strokeMiterLimitPropertyId Shape.StrokeMiterLimitProperty value
            |> node.set

        [<Extension>]
        static member stroke<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty strokePropertyId Shape.StrokeProperty value
            |> node.set

        [<Extension>]
        static member strokeStartLineCap<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: PenLineCap) =
            createDependencyProperty strokeStartLineCapPropertyId Shape.StrokeStartLineCapProperty value
            |> node.set

        [<Extension>]
        static member strokeThickness<'m when 'm :> Shape and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty strokeThicknessPropertyId Shape.StrokeThicknessProperty value
            |> node.set
