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

// skipped dependency property ModeProperty: read-only


[<AutoOpen>]
module TwoPaneViewMod =

    let private twoPaneViewNodeId = getNextId ()

    registerDefaultRenderer<TwoPaneView> twoPaneViewNodeId

    let private minTallModeHeightPropertyId = getNextId ()
    let private minWideModeWidthPropertyId = getNextId ()
    let private pane1LengthPropertyId = getNextId ()
    let private pane1PropertyId = getNextId ()
    let private pane2LengthPropertyId = getNextId ()
    let private pane2PropertyId = getNextId ()
    let private panePriorityPropertyId = getNextId ()
    let private tallModeConfigurationPropertyId = getNextId ()
    let private wideModeConfigurationPropertyId = getNextId ()

    [<Extension>]
    type TwoPaneViewExtensions =

        [<Extension>]
        static member minTallModeHeight(node: VirtualNode<TwoPaneView>, value: float) =
            createDependencyProperty minTallModeHeightPropertyId TwoPaneView.MinTallModeHeightProperty value
            |> node.set

        [<Extension>]
        static member minWideModeWidth(node: VirtualNode<TwoPaneView>, value: float) =
            createDependencyProperty minWideModeWidthPropertyId TwoPaneView.MinWideModeWidthProperty value
            |> node.set

        [<Extension>]
        static member pane1Length(node: VirtualNode<TwoPaneView>, value: GridLength) =
            createDependencyProperty pane1LengthPropertyId TwoPaneView.Pane1LengthProperty value
            |> node.set

        [<Extension>]
        static member pane1(node: VirtualNode<TwoPaneView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                pane1PropertyId
                value
                (fun element child -> (element :?> TwoPaneView).Pane1 <- child)
                (fun element -> (element :?> TwoPaneView).Pane1)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member pane2Length(node: VirtualNode<TwoPaneView>, value: GridLength) =
            createDependencyProperty pane2LengthPropertyId TwoPaneView.Pane2LengthProperty value
            |> node.set

        [<Extension>]
        static member pane2(node: VirtualNode<TwoPaneView>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                pane2PropertyId
                value
                (fun element child -> (element :?> TwoPaneView).Pane2 <- child)
                (fun element -> (element :?> TwoPaneView).Pane2)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member panePriority(node: VirtualNode<TwoPaneView>, value: TwoPaneViewPriority) =
            createDependencyProperty panePriorityPropertyId TwoPaneView.PanePriorityProperty value
            |> node.set

        [<Extension>]
        static member tallModeConfiguration(node: VirtualNode<TwoPaneView>, value: TwoPaneViewTallModeConfiguration) =
            createDependencyProperty tallModeConfigurationPropertyId TwoPaneView.TallModeConfigurationProperty value
            |> node.set

        [<Extension>]
        static member wideModeConfiguration(node: VirtualNode<TwoPaneView>, value: TwoPaneViewWideModeConfiguration) =
            createDependencyProperty wideModeConfigurationPropertyId TwoPaneView.WideModeConfigurationProperty value
            |> node.set

        [<Extension>]
        static member onModeChanged(node: VirtualNode<TwoPaneView>, handler: TwoPaneView -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ModeChanged"
                Subscribe = (fun uiElement ->
                    let twoPaneView = uiElement :?> TwoPaneView
                    let h = TypedEventHandler<TwoPaneView, obj>(fun _ e -> handler twoPaneView e)
                    twoPaneView.add_ModeChanged h
                    { new IDisposable with
                        member _.Dispose() = twoPaneView.remove_ModeChanged h }
                )
            })

    let twoPaneView () =
        createNode<TwoPaneView> twoPaneViewNodeId
