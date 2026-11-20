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

// skipped dependency property IsInOverflowProperty: read-only
// skipped property TemplateSettings: read-only


[<AutoOpen>]
module AppBarButtonMod =

    let private appBarButtonNodeId = getNextId ()

    registerDefaultRenderer<AppBarButton> appBarButtonNodeId

    let private dynamicOverflowOrderPropertyId = getNextId ()
    let private iconPropertyId = getNextId ()
    let private isCompactPropertyId = getNextId ()
    let private keyboardAcceleratorTextOverridePropertyId = getNextId ()
    let private labelPositionPropertyId = getNextId ()
    let private labelPropertyId = getNextId ()

    [<Extension>]
    type AppBarButtonExtensions =

        [<Extension>]
        static member dynamicOverflowOrder(node: VirtualNode<AppBarButton>, value: int) =
            createDependencyProperty dynamicOverflowOrderPropertyId AppBarButton.DynamicOverflowOrderProperty value
            |> node.set

        [<Extension>]
        static member icon(node: VirtualNode<AppBarButton>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                iconPropertyId
                value
                (fun element child -> (element :?> AppBarButton).Icon <- (child :?> IconElement))
                (fun element -> (element :?> AppBarButton).Icon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member isCompact(node: VirtualNode<AppBarButton>, value: bool) =
            createDependencyProperty isCompactPropertyId AppBarButton.IsCompactProperty value
            |> node.set

        [<Extension>]
        static member keyboardAcceleratorTextOverride(node: VirtualNode<AppBarButton>, value: string) =
            createDependencyProperty keyboardAcceleratorTextOverridePropertyId AppBarButton.KeyboardAcceleratorTextOverrideProperty value
            |> node.set

        [<Extension>]
        static member labelPosition(node: VirtualNode<AppBarButton>, value: CommandBarLabelPosition) =
            createDependencyProperty labelPositionPropertyId AppBarButton.LabelPositionProperty value
            |> node.set

        [<Extension>]
        static member label(node: VirtualNode<AppBarButton>, value: string) =
            createDependencyProperty labelPropertyId AppBarButton.LabelProperty value
            |> node.set

    let appBarButton () =
        createNode<AppBarButton> appBarButtonNodeId
