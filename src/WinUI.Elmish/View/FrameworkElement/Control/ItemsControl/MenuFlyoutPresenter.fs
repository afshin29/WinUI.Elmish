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

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module MenuFlyoutPresenterMod =

    let private menuFlyoutPresenterNodeId = getNextId ()

    registerDefaultRenderer<MenuFlyoutPresenter> menuFlyoutPresenterNodeId

    let private isDefaultShadowEnabledPropertyId = getNextId ()
    let private systemBackdropPropertyId = getNextId ()

    [<Extension>]
    type MenuFlyoutPresenterExtensions =

        [<Extension>]
        static member isDefaultShadowEnabled(node: VirtualNode<MenuFlyoutPresenter>, value: bool) =
            createDependencyProperty isDefaultShadowEnabledPropertyId MenuFlyoutPresenter.IsDefaultShadowEnabledProperty value
            |> node.set

        [<Extension>]
        static member systemBackdrop(node: VirtualNode<MenuFlyoutPresenter>, value: SystemBackdrop) =
            createDependencyProperty systemBackdropPropertyId MenuFlyoutPresenter.SystemBackdropProperty value
            |> node.set

    let menuFlyoutPresenter () =
        createNode<MenuFlyoutPresenter> menuFlyoutPresenterNodeId
