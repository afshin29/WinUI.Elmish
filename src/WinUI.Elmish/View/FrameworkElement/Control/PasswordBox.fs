namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Input
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property CanPasteClipboardContentProperty: read-only


[<AutoOpen>]
module PasswordBoxMod =

    let private passwordBoxNodeId = getNextId ()

    registerDefaultRenderer<PasswordBox> passwordBoxNodeId

    let private descriptionPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private inputScopePropertyId = getNextId ()
    let private isPasswordRevealButtonEnabledPropertyId = getNextId ()
    let private maxLengthPropertyId = getNextId ()
    let private passwordCharPropertyId = getNextId ()
    let private passwordPropertyId = getNextId ()
    let private passwordRevealModePropertyId = getNextId ()
    let private placeholderTextPropertyId = getNextId ()
    let private preventKeyboardDisplayOnProgrammaticFocusPropertyId = getNextId ()
    let private selectionFlyoutPropertyId = getNextId ()
    let private selectionHighlightColorPropertyId = getNextId ()
    let private textReadingOrderPropertyId = getNextId ()

    [<Extension>]
    type PasswordBoxExtensions =

        [<Extension>]
        static member description(node: VirtualNode<PasswordBox>, value: obj) =
            createDependencyProperty descriptionPropertyId PasswordBox.DescriptionProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<PasswordBox>, value: string) =
            createDependencyProperty str_headerPropertyId PasswordBox.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<PasswordBox>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> PasswordBox).Header <- child)
                (fun element -> (element :?> PasswordBox).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<PasswordBox>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId PasswordBox.HeaderTemplateProperty
            |> node.set

        [<Extension>]
        static member inputScope(node: VirtualNode<PasswordBox>, value: InputScope) =
            createDependencyProperty inputScopePropertyId PasswordBox.InputScopeProperty value
            |> node.set

        [<Extension>]
        static member isPasswordRevealButtonEnabled(node: VirtualNode<PasswordBox>, value: bool) =
            createDependencyProperty isPasswordRevealButtonEnabledPropertyId PasswordBox.IsPasswordRevealButtonEnabledProperty value
            |> node.set

        [<Extension>]
        static member maxLength(node: VirtualNode<PasswordBox>, value: int) =
            createDependencyProperty maxLengthPropertyId PasswordBox.MaxLengthProperty value
            |> node.set

        [<Extension>]
        static member passwordChar(node: VirtualNode<PasswordBox>, value: string) =
            createDependencyProperty passwordCharPropertyId PasswordBox.PasswordCharProperty value
            |> node.set

        [<Extension>]
        static member password(node: VirtualNode<PasswordBox>, value: string) =
            createDependencyProperty passwordPropertyId PasswordBox.PasswordProperty value
            |> node.set

        [<Extension>]
        static member passwordRevealMode(node: VirtualNode<PasswordBox>, value: PasswordRevealMode) =
            createDependencyProperty passwordRevealModePropertyId PasswordBox.PasswordRevealModeProperty value
            |> node.set

        [<Extension>]
        static member placeholderText(node: VirtualNode<PasswordBox>, value: string) =
            createDependencyProperty placeholderTextPropertyId PasswordBox.PlaceholderTextProperty value
            |> node.set

        [<Extension>]
        static member preventKeyboardDisplayOnProgrammaticFocus(node: VirtualNode<PasswordBox>, value: bool) =
            createDependencyProperty preventKeyboardDisplayOnProgrammaticFocusPropertyId PasswordBox.PreventKeyboardDisplayOnProgrammaticFocusProperty value
            |> node.set

        [<Extension>]
        static member selectionFlyout(node: VirtualNode<PasswordBox>, value: FlyoutBase) =
            createDependencyProperty selectionFlyoutPropertyId PasswordBox.SelectionFlyoutProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColor(node: VirtualNode<PasswordBox>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorPropertyId PasswordBox.SelectionHighlightColorProperty value
            |> node.set

        [<Extension>]
        static member textReadingOrder(node: VirtualNode<PasswordBox>, value: TextReadingOrder) =
            createDependencyProperty textReadingOrderPropertyId PasswordBox.TextReadingOrderProperty value
            |> node.set

        [<Extension>]
        static member onContextMenuOpening(node: VirtualNode<PasswordBox>, handler: ContextMenuEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContextMenuOpening"
                Subscribe = (fun uiElement ->
                    let passwordBox = uiElement :?> PasswordBox
                    let h = ContextMenuOpeningEventHandler(fun _ e -> handler e)
                    passwordBox.ContextMenuOpening.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = passwordBox.ContextMenuOpening.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPasswordChanged(node: VirtualNode<PasswordBox>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PasswordChanged"
                Subscribe = (fun uiElement ->
                    let passwordBox = uiElement :?> PasswordBox
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    passwordBox.PasswordChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = passwordBox.PasswordChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onPasswordChanging(node: VirtualNode<PasswordBox>, handler: PasswordBoxPasswordChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "PasswordChanging"
                Subscribe = (fun uiElement ->
                    let passwordBox = uiElement :?> PasswordBox
                    let h = TypedEventHandler<PasswordBox, PasswordBoxPasswordChangingEventArgs>(fun _ e -> handler e)
                    passwordBox.add_PasswordChanging h
                    { new IDisposable with
                        member _.Dispose() = passwordBox.remove_PasswordChanging h }
                )
            })

        [<Extension>]
        static member onPaste(node: VirtualNode<PasswordBox>, handler: TextControlPasteEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Paste"
                Subscribe = (fun uiElement ->
                    let passwordBox = uiElement :?> PasswordBox
                    let h = TextControlPasteEventHandler(fun _ e -> handler e)
                    passwordBox.Paste.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = passwordBox.Paste.RemoveHandler h }
                )
            })

    let passwordBox () =
        createNode<PasswordBox> passwordBoxNodeId
