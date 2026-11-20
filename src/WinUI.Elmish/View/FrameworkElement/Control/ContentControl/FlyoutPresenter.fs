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
module FlyoutPresenterMod =

    let private flyoutPresenterNodeId = getNextId ()

    registerDefaultRenderer<FlyoutPresenter> flyoutPresenterNodeId

    let private isDefaultShadowEnabledPropertyId = getNextId ()

    [<Extension>]
    type FlyoutPresenterExtensions =

        [<Extension>]
        static member isDefaultShadowEnabled(node: VirtualNode<FlyoutPresenter>, value: bool) =
            createDependencyProperty isDefaultShadowEnabledPropertyId FlyoutPresenter.IsDefaultShadowEnabledProperty value
            |> node.set

    let flyoutPresenter () =
        createNode<FlyoutPresenter> flyoutPresenterNodeId
