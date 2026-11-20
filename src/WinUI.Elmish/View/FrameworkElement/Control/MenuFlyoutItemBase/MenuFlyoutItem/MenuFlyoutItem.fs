namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open System.Windows.Input

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module MenuFlyoutItemMod =

    let private menuFlyoutItemNodeId = getNextId ()

    registerDefaultRenderer<MenuFlyoutItem> menuFlyoutItemNodeId

    let private commandParameterPropertyId = getNextId ()
    let private commandPropertyId = getNextId ()
    let private iconPropertyId = getNextId ()
    let private keyboardAcceleratorTextOverridePropertyId = getNextId ()
    let private textPropertyId = getNextId ()

    [<Extension>]
    type MenuFlyoutItemExtensions =

        [<Extension>]
        static member commandParameter<'m when 'm :> MenuFlyoutItem and 'm: not struct>
            (node: VirtualNode<'m>, value: obj) =
            createDependencyProperty commandParameterPropertyId MenuFlyoutItem.CommandParameterProperty value
            |> node.set

        [<Extension>]
        static member command<'m when 'm :> MenuFlyoutItem and 'm: not struct>
            (node: VirtualNode<'m>, value: ICommand) =
            createDependencyProperty commandPropertyId MenuFlyoutItem.CommandProperty value
            |> node.set

        [<Extension>]
        static member icon<'m when 'm :> MenuFlyoutItem and 'm: not struct>
            (node: VirtualNode<'m>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                iconPropertyId
                value
                (fun element child -> (element :?> MenuFlyoutItem).Icon <- (child :?> IconElement))
                (fun element -> (element :?> MenuFlyoutItem).Icon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member keyboardAcceleratorTextOverride<'m when 'm :> MenuFlyoutItem and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty keyboardAcceleratorTextOverridePropertyId MenuFlyoutItem.KeyboardAcceleratorTextOverrideProperty value
            |> node.set

        [<Extension>]
        static member text<'m when 'm :> MenuFlyoutItem and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty textPropertyId MenuFlyoutItem.TextProperty value
            |> node.set

        [<Extension>]
        static member onClick<'m when 'm :> MenuFlyoutItem and 'm: not struct>
            (node: VirtualNode<'m>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Click"
                Subscribe = (fun uiElement ->
                    let menuFlyoutItem = uiElement :?> MenuFlyoutItem
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    menuFlyoutItem.Click.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = menuFlyoutItem.Click.RemoveHandler h }
                )
            })

    let menuFlyoutItem () =
        createNode<MenuFlyoutItem> menuFlyoutItemNodeId
