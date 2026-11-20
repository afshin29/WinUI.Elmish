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
module PathMod =

    let private pathNodeId = getNextId ()

    registerDefaultRenderer<Path> pathNodeId

    let private dataPropertyId = getNextId ()

    [<Extension>]
    type PathExtensions =

        [<Extension>]
        static member data(node: VirtualNode<Path>, value: Geometry) =
            createDependencyProperty dataPropertyId Path.DataProperty value
            |> node.set

    let path () =
        createNode<Path> pathNodeId
