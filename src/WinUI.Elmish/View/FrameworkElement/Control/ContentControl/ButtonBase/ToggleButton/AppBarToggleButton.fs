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
module AppBarToggleButtonMod =

    let private appBarToggleButtonNodeId = getNextId ()

    registerDefaultRenderer<AppBarToggleButton> appBarToggleButtonNodeId

    let private dynamicOverflowOrderPropertyId = getNextId ()
    let private iconPropertyId = getNextId ()
    let private isCompactPropertyId = getNextId ()
    let private keyboardAcceleratorTextOverridePropertyId = getNextId ()
    let private labelPositionPropertyId = getNextId ()
    let private labelPropertyId = getNextId ()

    [<Extension>]
    type AppBarToggleButtonExtensions =

        [<Extension>]
        static member dynamicOverflowOrder(node: VirtualNode<AppBarToggleButton>, value: int) =
            createDependencyProperty dynamicOverflowOrderPropertyId AppBarToggleButton.DynamicOverflowOrderProperty value
            |> node.set

        [<Extension>]
        static member icon(node: VirtualNode<AppBarToggleButton>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                iconPropertyId
                value
                (fun element child -> (element :?> AppBarToggleButton).Icon <- (child :?> IconElement))
                (fun element -> (element :?> AppBarToggleButton).Icon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member isCompact(node: VirtualNode<AppBarToggleButton>, value: bool) =
            createDependencyProperty isCompactPropertyId AppBarToggleButton.IsCompactProperty value
            |> node.set

        [<Extension>]
        static member keyboardAcceleratorTextOverride(node: VirtualNode<AppBarToggleButton>, value: string) =
            createDependencyProperty keyboardAcceleratorTextOverridePropertyId AppBarToggleButton.KeyboardAcceleratorTextOverrideProperty value
            |> node.set

        [<Extension>]
        static member labelPosition(node: VirtualNode<AppBarToggleButton>, value: CommandBarLabelPosition) =
            createDependencyProperty labelPositionPropertyId AppBarToggleButton.LabelPositionProperty value
            |> node.set

        [<Extension>]
        static member label(node: VirtualNode<AppBarToggleButton>, value: string) =
            createDependencyProperty labelPropertyId AppBarToggleButton.LabelProperty value
            |> node.set

    let appBarToggleButton () =
        createNode<AppBarToggleButton> appBarToggleButtonNodeId
