namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Charts
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property Axes: read-only
// skipped property Data: read-only
// skipped property Series: read-only


[<AutoOpen>]
module ChartMod =

    let private chartNodeId = getNextId ()

    registerDefaultRenderer<Chart> chartNodeId

    let private legendTitlePropertyId = getNextId ()
    let private showLegendPropertyId = getNextId ()

    [<Extension>]
    type ChartExtensions =

        [<Extension>]
        static member legendTitle(node: VirtualNode<Chart>, value: string) =
            createDependencyProperty legendTitlePropertyId Chart.LegendTitleProperty value
            |> node.set

        [<Extension>]
        static member showLegend(node: VirtualNode<Chart>, value: bool) =
            createDependencyProperty showLegendPropertyId Chart.ShowLegendProperty value
            |> node.set

    let chart () =
        createNode<Chart> chartNodeId
