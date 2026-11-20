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
module TimePickerFlyoutPresenterMod =

    let private isDefaultShadowEnabledPropertyId = getNextId ()

    [<Extension>]
    type TimePickerFlyoutPresenterExtensions =

        [<Extension>]
        static member isDefaultShadowEnabled(node: VirtualNode<TimePickerFlyoutPresenter>, value: bool) =
            createDependencyProperty isDefaultShadowEnabledPropertyId TimePickerFlyoutPresenter.IsDefaultShadowEnabledProperty value
            |> node.set
