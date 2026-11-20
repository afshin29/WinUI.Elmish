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

// skipped dependency property TemplateSettingsProperty: read-only


[<AutoOpen>]
module NavigationViewItemPresenterMod =

    let private navigationViewItemPresenterNodeId = getNextId ()

    registerDefaultRenderer<NavigationViewItemPresenter> navigationViewItemPresenterNodeId

    let private iconPropertyId = getNextId ()
    let private infoBadgePropertyId = getNextId ()

    [<Extension>]
    type NavigationViewItemPresenterExtensions =

        [<Extension>]
        static member icon(node: VirtualNode<NavigationViewItemPresenter>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                iconPropertyId
                value
                (fun element child -> (element :?> NavigationViewItemPresenter).Icon <- (child :?> IconElement))
                (fun element -> (element :?> NavigationViewItemPresenter).Icon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member infoBadge(node: VirtualNode<NavigationViewItemPresenter>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                infoBadgePropertyId
                value
                (fun element child -> (element :?> NavigationViewItemPresenter).InfoBadge <- (child :?> InfoBadge))
                (fun element -> (element :?> NavigationViewItemPresenter).InfoBadge :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

    let navigationViewItemPresenter () =
        createNode<NavigationViewItemPresenter> navigationViewItemPresenterNodeId
