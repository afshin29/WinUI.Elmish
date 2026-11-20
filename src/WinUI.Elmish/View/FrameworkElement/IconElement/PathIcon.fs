namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module PathIconMod =

    let private pathIconNodeId = getNextId ()

    registerDefaultRenderer<PathIcon> pathIconNodeId

    let private dataPropertyId = getNextId ()

    [<Extension>]
    type PathIconExtensions =

        [<Extension>]
        static member data(node: VirtualNode<PathIcon>, value: Geometry) =
            createDependencyProperty dataPropertyId PathIcon.DataProperty value
            |> node.set

    let pathIcon () =
        createNode<PathIcon> pathIconNodeId
