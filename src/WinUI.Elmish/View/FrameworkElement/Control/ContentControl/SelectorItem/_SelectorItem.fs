namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module SelectorItemMod =

    let private isSelectedPropertyId = getNextId ()

    [<Extension>]
    type SelectorItemExtensions =

        [<Extension>]
        static member isSelected<'m when 'm :> SelectorItem and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isSelectedPropertyId SelectorItem.IsSelectedProperty value
            |> node.set
