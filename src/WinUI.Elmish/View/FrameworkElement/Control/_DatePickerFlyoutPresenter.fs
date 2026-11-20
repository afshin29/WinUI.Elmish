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
module DatePickerFlyoutPresenterMod =

    let private isDefaultShadowEnabledPropertyId = getNextId ()

    [<Extension>]
    type DatePickerFlyoutPresenterExtensions =

        [<Extension>]
        static member isDefaultShadowEnabled(node: VirtualNode<DatePickerFlyoutPresenter>, value: bool) =
            createDependencyProperty isDefaultShadowEnabledPropertyId DatePickerFlyoutPresenter.IsDefaultShadowEnabledProperty value
            |> node.set
