namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module IconSourceElementMod =

    let private iconSourceElementNodeId = getNextId ()

    registerDefaultRenderer<IconSourceElement> iconSourceElementNodeId

    let private iconSourcePropertyId = getNextId ()

    [<Extension>]
    type IconSourceElementExtensions =

        [<Extension>]
        static member iconSource(node: VirtualNode<IconSourceElement>, value: IconSource) =
            createDependencyProperty iconSourcePropertyId IconSourceElement.IconSourceProperty value
            |> node.set

    let iconSourceElement () =
        createNode<IconSourceElement> iconSourceElementNodeId
