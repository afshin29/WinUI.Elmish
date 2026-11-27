namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property PrimaryCommandsProperty: read-only
// skipped dependency property SecondaryCommandsProperty: read-only
// skipped property CommandBarTemplateSettings: read-only


[<AutoOpen>]
module CommandBarMod =

    let private commandBarNodeId = getNextId ()

    registerDefaultRenderer<CommandBar> commandBarNodeId

    let private commandBarOverflowPresenterStylePropertyId = getNextId ()
    let private defaultLabelPositionPropertyId = getNextId ()
    let private isDynamicOverflowEnabledPropertyId = getNextId ()
    let private overflowButtonVisibilityPropertyId = getNextId ()

    [<Extension>]
    type CommandBarExtensions =

        [<Extension>]
        static member commandBarOverflowPresenterStyle<'m when 'm :> CommandBar and 'm: not struct>
            (node: VirtualNode<'m>, value: Style) =
            createDependencyProperty commandBarOverflowPresenterStylePropertyId CommandBar.CommandBarOverflowPresenterStyleProperty value
            |> node.set

        [<Extension>]
        static member defaultLabelPosition<'m when 'm :> CommandBar and 'm: not struct>
            (node: VirtualNode<'m>, value: CommandBarDefaultLabelPosition) =
            createDependencyProperty defaultLabelPositionPropertyId CommandBar.DefaultLabelPositionProperty value
            |> node.set

        [<Extension>]
        static member isDynamicOverflowEnabled<'m when 'm :> CommandBar and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isDynamicOverflowEnabledPropertyId CommandBar.IsDynamicOverflowEnabledProperty value
            |> node.set

        [<Extension>]
        static member overflowButtonVisibility<'m when 'm :> CommandBar and 'm: not struct>
            (node: VirtualNode<'m>, value: CommandBarOverflowButtonVisibility) =
            createDependencyProperty overflowButtonVisibilityPropertyId CommandBar.OverflowButtonVisibilityProperty value
            |> node.set

        [<Extension>]
        static member onDynamicOverflowItemsChanging<'m when 'm :> CommandBar and 'm: not struct>
            (node: VirtualNode<'m>, handler: 'm -> DynamicOverflowItemsChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DynamicOverflowItemsChanging"
                Subscribe = (fun uiElement ->
                    let commandBar = uiElement :?> CommandBar
                    let sender = uiElement :?> 'm
                    let h = TypedEventHandler<CommandBar, DynamicOverflowItemsChangingEventArgs>(fun _ e -> handler sender e)
                    commandBar.add_DynamicOverflowItemsChanging h
                    { new IDisposable with
                        member _.Dispose() = commandBar.remove_DynamicOverflowItemsChanging h }
                )
            })

    let commandBar () =
        createNode<CommandBar> commandBarNodeId
