namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Shapes
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module RectangleMod =

    let private rectangleNodeId = getNextId ()

    registerDefaultRenderer<Rectangle> rectangleNodeId

    let private radiusXPropertyId = getNextId ()
    let private radiusYPropertyId = getNextId ()

    [<Extension>]
    type RectangleExtensions =

        [<Extension>]
        static member radiusX(node: VirtualNode<Rectangle>, value: float) =
            createDependencyProperty radiusXPropertyId Rectangle.RadiusXProperty value
            |> node.set

        [<Extension>]
        static member radiusY(node: VirtualNode<Rectangle>, value: float) =
            createDependencyProperty radiusYPropertyId Rectangle.RadiusYProperty value
            |> node.set

    let rectangle () =
        createNode<Rectangle> rectangleNodeId
