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


[<AutoOpen>]
module PolygonMod =

    let private polygonNodeId = getNextId ()

    registerDefaultRenderer<Polygon> polygonNodeId

    let private fillRulePropertyId = getNextId ()
    let private pointsPropertyId = getNextId ()

    [<Extension>]
    type PolygonExtensions =

        [<Extension>]
        static member fillRule(node: VirtualNode<Polygon>, value: FillRule) =
            createDependencyProperty fillRulePropertyId Polygon.FillRuleProperty value
            |> node.set

        [<Extension>]
        static member points(node: VirtualNode<Polygon>, value: PointCollection) =
            createDependencyProperty pointsPropertyId Polygon.PointsProperty value
            |> node.set

    let polygon () =
        createNode<Polygon> polygonNodeId
