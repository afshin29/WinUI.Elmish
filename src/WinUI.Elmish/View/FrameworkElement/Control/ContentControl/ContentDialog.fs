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


[<AutoOpen>]
module ContentDialogMod =

    let private contentDialogNodeId = getNextId ()

    registerDefaultRenderer<ContentDialog> contentDialogNodeId

    let private closeButtonCommandParameterPropertyId = getNextId ()
    let private closeButtonCommandPropertyId = getNextId ()
    let private closeButtonStylePropertyId = getNextId ()
    let private closeButtonTextPropertyId = getNextId ()
    let private defaultButtonPropertyId = getNextId ()
    let private fullSizeDesiredPropertyId = getNextId ()
    let private isPrimaryButtonEnabledPropertyId = getNextId ()
    let private isSecondaryButtonEnabledPropertyId = getNextId ()
    let private primaryButtonCommandParameterPropertyId = getNextId ()
    let private primaryButtonCommandPropertyId = getNextId ()
    let private primaryButtonStylePropertyId = getNextId ()
    let private primaryButtonTextPropertyId = getNextId ()
    let private secondaryButtonCommandParameterPropertyId = getNextId ()
    let private secondaryButtonCommandPropertyId = getNextId ()
    let private secondaryButtonStylePropertyId = getNextId ()
    let private secondaryButtonTextPropertyId = getNextId ()
    let private str_titlePropertyId = getNextId ()
    let private vnode_titlePropertyId = getNextId ()
    let private titleTemplatePropertyId = getNextId ()

    [<Extension>]
    type ContentDialogExtensions =

        [<Extension>]
        static member closeButtonCommandParameter(node: VirtualNode<ContentDialog>, value: obj) =
            createDependencyProperty closeButtonCommandParameterPropertyId ContentDialog.CloseButtonCommandParameterProperty value
            |> node.set

        [<Extension>]
        static member closeButtonCommand(node: VirtualNode<ContentDialog>, value: ICommand) =
            createDependencyProperty closeButtonCommandPropertyId ContentDialog.CloseButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member closeButtonStyle(node: VirtualNode<ContentDialog>, value: Style) =
            createDependencyProperty closeButtonStylePropertyId ContentDialog.CloseButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member closeButtonText(node: VirtualNode<ContentDialog>, value: string) =
            createDependencyProperty closeButtonTextPropertyId ContentDialog.CloseButtonTextProperty value
            |> node.set

        [<Extension>]
        static member defaultButton(node: VirtualNode<ContentDialog>, value: ContentDialogButton) =
            createDependencyProperty defaultButtonPropertyId ContentDialog.DefaultButtonProperty value
            |> node.set

        [<Extension>]
        static member fullSizeDesired(node: VirtualNode<ContentDialog>, value: bool) =
            createDependencyProperty fullSizeDesiredPropertyId ContentDialog.FullSizeDesiredProperty value
            |> node.set

        [<Extension>]
        static member isPrimaryButtonEnabled(node: VirtualNode<ContentDialog>, value: bool) =
            createDependencyProperty isPrimaryButtonEnabledPropertyId ContentDialog.IsPrimaryButtonEnabledProperty value
            |> node.set

        [<Extension>]
        static member isSecondaryButtonEnabled(node: VirtualNode<ContentDialog>, value: bool) =
            createDependencyProperty isSecondaryButtonEnabledPropertyId ContentDialog.IsSecondaryButtonEnabledProperty value
            |> node.set

        [<Extension>]
        static member primaryButtonCommandParameter(node: VirtualNode<ContentDialog>, value: obj) =
            createDependencyProperty primaryButtonCommandParameterPropertyId ContentDialog.PrimaryButtonCommandParameterProperty value
            |> node.set

        [<Extension>]
        static member primaryButtonCommand(node: VirtualNode<ContentDialog>, value: ICommand) =
            createDependencyProperty primaryButtonCommandPropertyId ContentDialog.PrimaryButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member primaryButtonStyle(node: VirtualNode<ContentDialog>, value: Style) =
            createDependencyProperty primaryButtonStylePropertyId ContentDialog.PrimaryButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member primaryButtonText(node: VirtualNode<ContentDialog>, value: string) =
            createDependencyProperty primaryButtonTextPropertyId ContentDialog.PrimaryButtonTextProperty value
            |> node.set

        [<Extension>]
        static member secondaryButtonCommandParameter(node: VirtualNode<ContentDialog>, value: obj) =
            createDependencyProperty secondaryButtonCommandParameterPropertyId ContentDialog.SecondaryButtonCommandParameterProperty value
            |> node.set

        [<Extension>]
        static member secondaryButtonCommand(node: VirtualNode<ContentDialog>, value: ICommand) =
            createDependencyProperty secondaryButtonCommandPropertyId ContentDialog.SecondaryButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member secondaryButtonStyle(node: VirtualNode<ContentDialog>, value: Style) =
            createDependencyProperty secondaryButtonStylePropertyId ContentDialog.SecondaryButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member secondaryButtonText(node: VirtualNode<ContentDialog>, value: string) =
            createDependencyProperty secondaryButtonTextPropertyId ContentDialog.SecondaryButtonTextProperty value
            |> node.set

        [<Extension>]
        static member title(node: VirtualNode<ContentDialog>, value: string) =
            createDependencyProperty str_titlePropertyId ContentDialog.TitleProperty value
            |> node.set

        [<Extension>]
        static member title<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<ContentDialog>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_titlePropertyId
                value
                (fun element child -> (element :?> ContentDialog).Title <- child)
                (fun element -> (element :?> ContentDialog).Title :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member titleTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<ContentDialog>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty titleTemplatePropertyId ContentDialog.TitleTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member onCloseButtonClick(node: VirtualNode<ContentDialog>, handler: ContentDialog -> ContentDialogButtonClickEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CloseButtonClick"
                Subscribe = (fun uiElement ->
                    let contentDialog = uiElement :?> ContentDialog
                    let h = TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs>(fun _ e -> handler contentDialog e)
                    contentDialog.add_CloseButtonClick h
                    { new IDisposable with
                        member _.Dispose() = contentDialog.remove_CloseButtonClick h }
                )
            })

        [<Extension>]
        static member onClosed(node: VirtualNode<ContentDialog>, handler: ContentDialog -> ContentDialogClosedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closed"
                Subscribe = (fun uiElement ->
                    let contentDialog = uiElement :?> ContentDialog
                    let h = TypedEventHandler<ContentDialog, ContentDialogClosedEventArgs>(fun _ e -> handler contentDialog e)
                    contentDialog.add_Closed h
                    { new IDisposable with
                        member _.Dispose() = contentDialog.remove_Closed h }
                )
            })

        [<Extension>]
        static member onClosing(node: VirtualNode<ContentDialog>, handler: ContentDialog -> ContentDialogClosingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closing"
                Subscribe = (fun uiElement ->
                    let contentDialog = uiElement :?> ContentDialog
                    let h = TypedEventHandler<ContentDialog, ContentDialogClosingEventArgs>(fun _ e -> handler contentDialog e)
                    contentDialog.add_Closing h
                    { new IDisposable with
                        member _.Dispose() = contentDialog.remove_Closing h }
                )
            })

        [<Extension>]
        static member onOpened(node: VirtualNode<ContentDialog>, handler: ContentDialog -> ContentDialogOpenedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opened"
                Subscribe = (fun uiElement ->
                    let contentDialog = uiElement :?> ContentDialog
                    let h = TypedEventHandler<ContentDialog, ContentDialogOpenedEventArgs>(fun _ e -> handler contentDialog e)
                    contentDialog.add_Opened h
                    { new IDisposable with
                        member _.Dispose() = contentDialog.remove_Opened h }
                )
            })

        [<Extension>]
        static member onPrimaryButtonClick(node: VirtualNode<ContentDialog>, handler: ContentDialog -> ContentDialogButtonClickEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PrimaryButtonClick"
                Subscribe = (fun uiElement ->
                    let contentDialog = uiElement :?> ContentDialog
                    let h = TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs>(fun _ e -> handler contentDialog e)
                    contentDialog.add_PrimaryButtonClick h
                    { new IDisposable with
                        member _.Dispose() = contentDialog.remove_PrimaryButtonClick h }
                )
            })

        [<Extension>]
        static member onSecondaryButtonClick(node: VirtualNode<ContentDialog>, handler: ContentDialog -> ContentDialogButtonClickEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SecondaryButtonClick"
                Subscribe = (fun uiElement ->
                    let contentDialog = uiElement :?> ContentDialog
                    let h = TypedEventHandler<ContentDialog, ContentDialogButtonClickEventArgs>(fun _ e -> handler contentDialog e)
                    contentDialog.add_SecondaryButtonClick h
                    { new IDisposable with
                        member _.Dispose() = contentDialog.remove_SecondaryButtonClick h }
                )
            })

    let contentDialog () =
        createNode<ContentDialog> contentDialogNodeId
