namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property FlyoutTemplateSettings: read-only


[<AutoOpen>]
module CommandBarFlyoutCommandBarMod =

    let private commandBarFlyoutCommandBarNodeId = getNextId ()

    registerDefaultRenderer<CommandBarFlyoutCommandBar> commandBarFlyoutCommandBarNodeId

    let private systemBackdropPropertyId = getNextId ()

    [<Extension>]
    type CommandBarFlyoutCommandBarExtensions =

        [<Extension>]
        static member systemBackdrop(node: VirtualNode<CommandBarFlyoutCommandBar>, value: SystemBackdrop) =
            createDependencyProperty systemBackdropPropertyId CommandBarFlyoutCommandBar.SystemBackdropProperty value
            |> node.set

    let commandBarFlyoutCommandBar () =
        createNode<CommandBarFlyoutCommandBar> commandBarFlyoutCommandBarNodeId
