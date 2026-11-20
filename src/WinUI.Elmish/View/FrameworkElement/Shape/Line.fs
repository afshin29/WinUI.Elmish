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
module LineMod =

    let private lineNodeId = getNextId ()

    registerDefaultRenderer<Line> lineNodeId

    let private x1PropertyId = getNextId ()
    let private x2PropertyId = getNextId ()
    let private y1PropertyId = getNextId ()
    let private y2PropertyId = getNextId ()

    [<Extension>]
    type LineExtensions =

        [<Extension>]
        static member x1(node: VirtualNode<Line>, value: float) =
            createDependencyProperty x1PropertyId Line.X1Property value
            |> node.set

        [<Extension>]
        static member x2(node: VirtualNode<Line>, value: float) =
            createDependencyProperty x2PropertyId Line.X2Property value
            |> node.set

        [<Extension>]
        static member y1(node: VirtualNode<Line>, value: float) =
            createDependencyProperty y1PropertyId Line.Y1Property value
            |> node.set

        [<Extension>]
        static member y2(node: VirtualNode<Line>, value: float) =
            createDependencyProperty y2PropertyId Line.Y2Property value
            |> node.set

    let line () =
        createNode<Line> lineNodeId
