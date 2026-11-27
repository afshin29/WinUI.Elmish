namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
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
module InfoBarMod =

    let private infoBarNodeId = getNextId ()

    registerDefaultRenderer<InfoBar> infoBarNodeId

    let private actionButtonPropertyId = getNextId ()
    let private closeButtonCommandParameterPropertyId = getNextId ()
    let private closeButtonCommandPropertyId = getNextId ()
    let private closeButtonStylePropertyId = getNextId ()
    let private str_contentPropertyId = getNextId ()
    let private vnode_contentPropertyId = getNextId ()
    let private contentTemplatePropertyId = getNextId ()
    let private iconSourcePropertyId = getNextId ()
    let private isClosablePropertyId = getNextId ()
    let private isIconVisiblePropertyId = getNextId ()
    let private isOpenPropertyId = getNextId ()
    let private messagePropertyId = getNextId ()
    let private severityPropertyId = getNextId ()
    let private titlePropertyId = getNextId ()

    [<Extension>]
    type InfoBarExtensions =

        [<Extension>]
        static member actionButton(node: VirtualNode<InfoBar>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                actionButtonPropertyId
                value
                (fun element child -> (element :?> InfoBar).ActionButton <- (child :?> ButtonBase))
                (fun element -> (element :?> InfoBar).ActionButton :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member closeButtonCommandParameter(node: VirtualNode<InfoBar>, value: obj) =
            createDependencyProperty closeButtonCommandParameterPropertyId InfoBar.CloseButtonCommandParameterProperty value
            |> node.set

        [<Extension>]
        static member closeButtonCommand(node: VirtualNode<InfoBar>, value: ICommand) =
            createDependencyProperty closeButtonCommandPropertyId InfoBar.CloseButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member closeButtonStyle(node: VirtualNode<InfoBar>, value: Style) =
            createDependencyProperty closeButtonStylePropertyId InfoBar.CloseButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member content(node: VirtualNode<InfoBar>, value: string) =
            createDependencyProperty str_contentPropertyId InfoBar.ContentProperty value
            |> node.set

        [<Extension>]
        static member content<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<InfoBar>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_contentPropertyId
                value
                (fun element child -> (element :?> InfoBar).Content <- child)
                (fun element -> (element :?> InfoBar).Content :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member contentTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<InfoBar>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty contentTemplatePropertyId InfoBar.ContentTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member iconSource(node: VirtualNode<InfoBar>, value: IconSource) =
            createDependencyProperty iconSourcePropertyId InfoBar.IconSourceProperty value
            |> node.set

        [<Extension>]
        static member isClosable(node: VirtualNode<InfoBar>, value: bool) =
            createDependencyProperty isClosablePropertyId InfoBar.IsClosableProperty value
            |> node.set

        [<Extension>]
        static member isIconVisible(node: VirtualNode<InfoBar>, value: bool) =
            createDependencyProperty isIconVisiblePropertyId InfoBar.IsIconVisibleProperty value
            |> node.set

        [<Extension>]
        static member isOpen(node: VirtualNode<InfoBar>, value: bool) =
            createDependencyProperty isOpenPropertyId InfoBar.IsOpenProperty value
            |> node.set

        [<Extension>]
        static member message(node: VirtualNode<InfoBar>, value: string) =
            createDependencyProperty messagePropertyId InfoBar.MessageProperty value
            |> node.set

        [<Extension>]
        static member severity(node: VirtualNode<InfoBar>, value: InfoBarSeverity) =
            createDependencyProperty severityPropertyId InfoBar.SeverityProperty value
            |> node.set

        [<Extension>]
        static member title(node: VirtualNode<InfoBar>, value: string) =
            createDependencyProperty titlePropertyId InfoBar.TitleProperty value
            |> node.set

        [<Extension>]
        static member onCloseButtonClick(node: VirtualNode<InfoBar>, handler: InfoBar -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CloseButtonClick"
                Subscribe = (fun uiElement ->
                    let infoBar = uiElement :?> InfoBar
                    let h = TypedEventHandler<InfoBar, obj>(fun _ e -> handler infoBar e)
                    infoBar.add_CloseButtonClick h
                    { new IDisposable with
                        member _.Dispose() = infoBar.remove_CloseButtonClick h }
                )
            })

        [<Extension>]
        static member onClosed(node: VirtualNode<InfoBar>, handler: InfoBar -> InfoBarClosedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closed"
                Subscribe = (fun uiElement ->
                    let infoBar = uiElement :?> InfoBar
                    let h = TypedEventHandler<InfoBar, InfoBarClosedEventArgs>(fun _ e -> handler infoBar e)
                    infoBar.add_Closed h
                    { new IDisposable with
                        member _.Dispose() = infoBar.remove_Closed h }
                )
            })

        [<Extension>]
        static member onClosing(node: VirtualNode<InfoBar>, handler: InfoBar -> InfoBarClosingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Closing"
                Subscribe = (fun uiElement ->
                    let infoBar = uiElement :?> InfoBar
                    let h = TypedEventHandler<InfoBar, InfoBarClosingEventArgs>(fun _ e -> handler infoBar e)
                    infoBar.add_Closing h
                    { new IDisposable with
                        member _.Dispose() = infoBar.remove_Closing h }
                )
            })

        [<Extension>]
        static member onOpened(node: VirtualNode<InfoBar>, handler: InfoBar -> InfoBarOpenedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Opened"
                Subscribe = (fun uiElement ->
                    let infoBar = uiElement :?> InfoBar
                    let h = TypedEventHandler<InfoBar, InfoBarOpenedEventArgs>(fun _ e -> handler infoBar e)
                    infoBar.add_Opened h
                    { new IDisposable with
                        member _.Dispose() = infoBar.remove_Opened h }
                )
            })

    let infoBar () =
        createNode<InfoBar> infoBarNodeId

    let infoBar' (title: string, message: string, severity: InfoBarSeverity) =
        infoBar()
            .title(title)
            .message(message)
            .severity(severity)
