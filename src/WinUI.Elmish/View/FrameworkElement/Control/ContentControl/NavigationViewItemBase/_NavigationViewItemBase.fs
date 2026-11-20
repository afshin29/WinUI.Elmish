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
module NavigationViewItemBaseMod =

    let private isSelectedPropertyId = getNextId ()

    [<Extension>]
    type NavigationViewItemBaseExtensions =

        [<Extension>]
        static member isSelected<'m when 'm :> NavigationViewItemBase and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isSelectedPropertyId NavigationViewItemBase.IsSelectedProperty value
            |> node.set
