namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open System.Windows.Input
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property TemplateSettingsProperty: read-only


[<AutoOpen>]
module TeachingTipMod =

    let private teachingTipNodeId = getNextId ()

    registerDefaultRenderer<TeachingTip> teachingTipNodeId

    let private actionButtonCommandParameterPropertyId = getNextId ()
    let private actionButtonCommandPropertyId = getNextId ()
    let private actionButtonContentPropertyId = getNextId ()
    let private actionButtonStylePropertyId = getNextId ()
    let private closeButtonCommandParameterPropertyId = getNextId ()
    let private closeButtonCommandPropertyId = getNextId ()
    let private closeButtonContentPropertyId = getNextId ()
    let private closeButtonStylePropertyId = getNextId ()
    let private heroContentPlacementPropertyId = getNextId ()
    let private heroContentPropertyId = getNextId ()
    let private iconSourcePropertyId = getNextId ()
    let private isLightDismissEnabledPropertyId = getNextId ()
    let private isOpenPropertyId = getNextId ()
    let private placementMarginPropertyId = getNextId ()
    let private preferredPlacementPropertyId = getNextId ()
    let private shouldConstrainToRootBoundsPropertyId = getNextId ()
    let private subtitlePropertyId = getNextId ()
    let private tailVisibilityPropertyId = getNextId ()
    let private targetPropertyId = getNextId ()
    let private titlePropertyId = getNextId ()

    [<Extension>]
    type TeachingTipExtensions =

        [<Extension>]
        static member actionButtonCommandParameter(node: VirtualNode<TeachingTip>, value: obj) =
            createDependencyProperty actionButtonCommandParameterPropertyId TeachingTip.ActionButtonCommandParameterProperty value
            |> node.set

        [<Extension>]
        static member actionButtonCommand(node: VirtualNode<TeachingTip>, value: ICommand) =
            createDependencyProperty actionButtonCommandPropertyId TeachingTip.ActionButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member actionButtonContent(node: VirtualNode<TeachingTip>, value: obj) =
            createDependencyProperty actionButtonContentPropertyId TeachingTip.ActionButtonContentProperty value
            |> node.set

        [<Extension>]
        static member actionButtonStyle(node: VirtualNode<TeachingTip>, value: Style) =
            createDependencyProperty actionButtonStylePropertyId TeachingTip.ActionButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member closeButtonCommandParameter(node: VirtualNode<TeachingTip>, value: obj) =
            createDependencyProperty closeButtonCommandParameterPropertyId TeachingTip.CloseButtonCommandParameterProperty value
            |> node.set

        [<Extension>]
        static member closeButtonCommand(node: VirtualNode<TeachingTip>, value: ICommand) =
            createDependencyProperty closeButtonCommandPropertyId TeachingTip.CloseButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member closeButtonContent(node: VirtualNode<TeachingTip>, value: obj) =
            createDependencyProperty closeButtonContentPropertyId TeachingTip.CloseButtonContentProperty value
            |> node.set

        [<Extension>]
        static member closeButtonStyle(node: VirtualNode<TeachingTip>, value: Style) =
            createDependencyProperty closeButtonStylePropertyId TeachingTip.CloseButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member heroContentPlacement(node: VirtualNode<TeachingTip>, value: TeachingTipHeroContentPlacementMode) =
            createDependencyProperty heroContentPlacementPropertyId TeachingTip.HeroContentPlacementProperty value
            |> node.set

        [<Extension>]
        static member heroContent(node: VirtualNode<TeachingTip>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                heroContentPropertyId
                value
                (fun element child -> (element :?> TeachingTip).HeroContent <- child)
                (fun element -> (element :?> TeachingTip).HeroContent)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member iconSource(node: VirtualNode<TeachingTip>, value: IconSource) =
            createDependencyProperty iconSourcePropertyId TeachingTip.IconSourceProperty value
            |> node.set

        [<Extension>]
        static member isLightDismissEnabled(node: VirtualNode<TeachingTip>, value: bool) =
            createDependencyProperty isLightDismissEnabledPropertyId TeachingTip.IsLightDismissEnabledProperty value
            |> node.set

        [<Extension>]
        static member isOpen(node: VirtualNode<TeachingTip>, value: bool) =
            createDependencyProperty isOpenPropertyId TeachingTip.IsOpenProperty value
            |> node.set

        [<Extension>]
        static member placementMargin(node: VirtualNode<TeachingTip>, value: Thickness) =
            createDependencyProperty placementMarginPropertyId TeachingTip.PlacementMarginProperty value
            |> node.set

        [<Extension>]
        static member preferredPlacement(node: VirtualNode<TeachingTip>, value: TeachingTipPlacementMode) =
            createDependencyProperty preferredPlacementPropertyId TeachingTip.PreferredPlacementProperty value
            |> node.set

        [<Extension>]
        static member shouldConstrainToRootBounds(node: VirtualNode<TeachingTip>, value: bool) =
            createDependencyProperty shouldConstrainToRootBoundsPropertyId TeachingTip.ShouldConstrainToRootBoundsProperty value
            |> node.set

        [<Extension>]
        static member subtitle(node: VirtualNode<TeachingTip>, value: string) =
            createDependencyProperty subtitlePropertyId TeachingTip.SubtitleProperty value
            |> node.set

        [<Extension>]
        static member tailVisibility(node: VirtualNode<TeachingTip>, value: TeachingTipTailVisibility) =
            createDependencyProperty tailVisibilityPropertyId TeachingTip.TailVisibilityProperty value
            |> node.set

        [<Extension>]
        static member target(node: VirtualNode<TeachingTip>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                targetPropertyId
                value
                (fun element child -> (element :?> TeachingTip).Target <- (child :?> FrameworkElement))
                (fun element -> (element :?> TeachingTip).Target :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member title(node: VirtualNode<TeachingTip>, value: string) =
            createDependencyProperty titlePropertyId TeachingTip.TitleProperty value
            |> node.set

        [<Extension>]
        static member onActionButtonClick(node: VirtualNode<TeachingTip>, handler: TeachingTip -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ActionButtonClick"
                Subscribe = (fun uiElement ->
                    let teachingTip = uiElement :?> TeachingTip
                    let h = TypedEventHandler<TeachingTip, obj>(fun _ e -> handler teachingTip e)
                    teachingTip.add_ActionButtonClick h
                    { new IDisposable with
                        member _.Dispose() = teachingTip.remove_ActionButtonClick h }
                )
            })

        [<Extension>]
        static member onCloseButtonClick(node: VirtualNode<TeachingTip>, handler: TeachingTip -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CloseButtonClick"
                Subscribe = (fun uiElement ->
                    let teachingTip = uiElement :?> TeachingTip
                    let h = TypedEventHandler<TeachingTip, obj>(fun _ e -> handler teachingTip e)
                    teachingTip.add_CloseButtonClick h
                    { new IDisposable with
                        member _.Dispose() = teachingTip.remove_CloseButtonClick h }
                )
            })

        [<Extension>]
        static member onClosed(node: VirtualNode<TeachingTip>, handler: TeachingTip -> TeachingTipClosedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closed"
                Subscribe = (fun uiElement ->
                    let teachingTip = uiElement :?> TeachingTip
                    let h = TypedEventHandler<TeachingTip, TeachingTipClosedEventArgs>(fun _ e -> handler teachingTip e)
                    teachingTip.add_Closed h
                    { new IDisposable with
                        member _.Dispose() = teachingTip.remove_Closed h }
                )
            })

        [<Extension>]
        static member onClosing(node: VirtualNode<TeachingTip>, handler: TeachingTip -> TeachingTipClosingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closing"
                Subscribe = (fun uiElement ->
                    let teachingTip = uiElement :?> TeachingTip
                    let h = TypedEventHandler<TeachingTip, TeachingTipClosingEventArgs>(fun _ e -> handler teachingTip e)
                    teachingTip.add_Closing h
                    { new IDisposable with
                        member _.Dispose() = teachingTip.remove_Closing h }
                )
            })

        [<Extension>]
        static member onOpened(node: VirtualNode<TeachingTip>, handler: TeachingTip -> TeachingTipOpenedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opened"
                Subscribe = (fun uiElement ->
                    let teachingTip = uiElement :?> TeachingTip
                    let h = TypedEventHandler<TeachingTip, TeachingTipOpenedEventArgs>(fun _ e -> handler teachingTip e)
                    teachingTip.add_Opened h
                    { new IDisposable with
                        member _.Dispose() = teachingTip.remove_Opened h }
                )
            })

    let teachingTip () =
        createNode<TeachingTip> teachingTipNodeId
