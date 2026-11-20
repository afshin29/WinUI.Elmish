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
module PolylineMod =

    let private polylineNodeId = getNextId ()

    registerDefaultRenderer<Polyline> polylineNodeId

    let private fillRulePropertyId = getNextId ()
    let private pointsPropertyId = getNextId ()

    [<Extension>]
    type PolylineExtensions =

        [<Extension>]
        static member fillRule(node: VirtualNode<Polyline>, value: FillRule) =
            createDependencyProperty fillRulePropertyId Polyline.FillRuleProperty value
            |> node.set

        [<Extension>]
        static member points(node: VirtualNode<Polyline>, value: PointCollection) =
            createDependencyProperty pointsPropertyId Polyline.PointsProperty value
            |> node.set

    let polyline () =
        createNode<Polyline> polylineNodeId
