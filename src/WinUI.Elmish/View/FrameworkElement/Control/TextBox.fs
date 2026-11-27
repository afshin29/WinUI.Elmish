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
// skipped dependency property CanRedoProperty: read-only
// skipped dependency property CanUndoProperty: read-only
// skipped dependency property ProofingMenuFlyoutProperty: read-only


[<AutoOpen>]
module TextBoxMod =

    let private textBoxNodeId = getNextId ()

    registerDefaultRenderer<TextBox> textBoxNodeId

    let private acceptsReturnPropertyId = getNextId ()
    let private characterCasingPropertyId = getNextId ()
    let private descriptionPropertyId = getNextId ()
    let private desiredCandidateWindowAlignmentPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private horizontalTextAlignmentPropertyId = getNextId ()
    let private inputScopePropertyId = getNextId ()
    let private isColorFontEnabledPropertyId = getNextId ()
    let private isReadOnlyPropertyId = getNextId ()
    let private isSpellCheckEnabledPropertyId = getNextId ()
    let private isTextPredictionEnabledPropertyId = getNextId ()
    let private maxLengthPropertyId = getNextId ()
    let private placeholderForegroundPropertyId = getNextId ()
    let private placeholderTextPropertyId = getNextId ()
    let private preventKeyboardDisplayOnProgrammaticFocusPropertyId = getNextId ()
    let private selectionFlyoutPropertyId = getNextId ()
    let private selectionHighlightColorPropertyId = getNextId ()
    let private selectionHighlightColorWhenNotFocusedPropertyId = getNextId ()
    let private textAlignmentPropertyId = getNextId ()
    let private textPropertyId = getNextId ()
    let private textReadingOrderPropertyId = getNextId ()
    let private textWrappingPropertyId = getNextId ()
    let private selectedTextPropertyId = getNextId ()
    let private selectionLengthPropertyId = getNextId ()
    let private selectionStartPropertyId = getNextId ()

    [<Extension>]
    type TextBoxExtensions =

        [<Extension>]
        static member acceptsReturn(node: VirtualNode<TextBox>, value: bool) =
            createDependencyProperty acceptsReturnPropertyId TextBox.AcceptsReturnProperty value
            |> node.set

        [<Extension>]
        static member characterCasing(node: VirtualNode<TextBox>, value: CharacterCasing) =
            createDependencyProperty characterCasingPropertyId TextBox.CharacterCasingProperty value
            |> node.set

        [<Extension>]
        static member description(node: VirtualNode<TextBox>, value: obj) =
            createDependencyProperty descriptionPropertyId TextBox.DescriptionProperty value
            |> node.set

        [<Extension>]
        static member desiredCandidateWindowAlignment(node: VirtualNode<TextBox>, value: CandidateWindowAlignment) =
            createDependencyProperty desiredCandidateWindowAlignmentPropertyId TextBox.DesiredCandidateWindowAlignmentProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<TextBox>, value: string) =
            createDependencyProperty str_headerPropertyId TextBox.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<TextBox>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> TextBox).Header <- child)
                (fun element -> (element :?> TextBox).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<TextBox>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId TextBox.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member horizontalTextAlignment(node: VirtualNode<TextBox>, value: TextAlignment) =
            createDependencyProperty horizontalTextAlignmentPropertyId TextBox.HorizontalTextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member inputScope(node: VirtualNode<TextBox>, value: InputScope) =
            createDependencyProperty inputScopePropertyId TextBox.InputScopeProperty value
            |> node.set

        [<Extension>]
        static member isColorFontEnabled(node: VirtualNode<TextBox>, value: bool) =
            createDependencyProperty isColorFontEnabledPropertyId TextBox.IsColorFontEnabledProperty value
            |> node.set

        [<Extension>]
        static member isReadOnly(node: VirtualNode<TextBox>, value: bool) =
            createDependencyProperty isReadOnlyPropertyId TextBox.IsReadOnlyProperty value
            |> node.set

        [<Extension>]
        static member isSpellCheckEnabled(node: VirtualNode<TextBox>, value: bool) =
            createDependencyProperty isSpellCheckEnabledPropertyId TextBox.IsSpellCheckEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTextPredictionEnabled(node: VirtualNode<TextBox>, value: bool) =
            createDependencyProperty isTextPredictionEnabledPropertyId TextBox.IsTextPredictionEnabledProperty value
            |> node.set

        [<Extension>]
        static member maxLength(node: VirtualNode<TextBox>, value: int) =
            createDependencyProperty maxLengthPropertyId TextBox.MaxLengthProperty value
            |> node.set

        [<Extension>]
        static member placeholderForeground(node: VirtualNode<TextBox>, value: Brush) =
            createDependencyProperty placeholderForegroundPropertyId TextBox.PlaceholderForegroundProperty value
            |> node.set

        [<Extension>]
        static member placeholderText(node: VirtualNode<TextBox>, value: string) =
            createDependencyProperty placeholderTextPropertyId TextBox.PlaceholderTextProperty value
            |> node.set

        [<Extension>]
        static member preventKeyboardDisplayOnProgrammaticFocus(node: VirtualNode<TextBox>, value: bool) =
            createDependencyProperty preventKeyboardDisplayOnProgrammaticFocusPropertyId TextBox.PreventKeyboardDisplayOnProgrammaticFocusProperty value
            |> node.set

        [<Extension>]
        static member selectionFlyout(node: VirtualNode<TextBox>, value: FlyoutBase) =
            createDependencyProperty selectionFlyoutPropertyId TextBox.SelectionFlyoutProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColor(node: VirtualNode<TextBox>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorPropertyId TextBox.SelectionHighlightColorProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColorWhenNotFocused(node: VirtualNode<TextBox>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorWhenNotFocusedPropertyId TextBox.SelectionHighlightColorWhenNotFocusedProperty value
            |> node.set

        [<Extension>]
        static member textAlignment(node: VirtualNode<TextBox>, value: TextAlignment) =
            createDependencyProperty textAlignmentPropertyId TextBox.TextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member text(node: VirtualNode<TextBox>, value: string) =
            createDependencyProperty textPropertyId TextBox.TextProperty value
            |> node.set

        [<Extension>]
        static member textReadingOrder(node: VirtualNode<TextBox>, value: TextReadingOrder) =
            createDependencyProperty textReadingOrderPropertyId TextBox.TextReadingOrderProperty value
            |> node.set

        [<Extension>]
        static member textWrapping(node: VirtualNode<TextBox>, value: TextWrapping) =
            createDependencyProperty textWrappingPropertyId TextBox.TextWrappingProperty value
            |> node.set

        [<Extension>]
        static member selectedText(node: VirtualNode<TextBox>, value: string) =
            createClrProperty
                selectedTextPropertyId
                (fun uiElement _ -> (uiElement :?> TextBox).SelectedText <- value)
                (fun uiElement -> (uiElement :?> TextBox).SelectedText <- Unchecked.defaultof<string>)
                value
            |> node.set

        [<Extension>]
        static member selectionLength(node: VirtualNode<TextBox>, value: int) =
            createClrProperty
                selectionLengthPropertyId
                (fun uiElement _ -> (uiElement :?> TextBox).SelectionLength <- value)
                (fun uiElement -> (uiElement :?> TextBox).SelectionLength <- Unchecked.defaultof<int>)
                value
            |> node.set

        [<Extension>]
        static member selectionStart(node: VirtualNode<TextBox>, value: int) =
            createClrProperty
                selectionStartPropertyId
                (fun uiElement _ -> (uiElement :?> TextBox).SelectionStart <- value)
                (fun uiElement -> (uiElement :?> TextBox).SelectionStart <- Unchecked.defaultof<int>)
                value
            |> node.set

        [<Extension>]
        static member onBeforeTextChanging(node: VirtualNode<TextBox>, handler: TextBox -> TextBoxBeforeTextChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "BeforeTextChanging"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextBoxBeforeTextChangingEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_BeforeTextChanging h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_BeforeTextChanging h }
                )
            })

        [<Extension>]
        static member onCandidateWindowBoundsChanged(node: VirtualNode<TextBox>, handler: TextBox -> CandidateWindowBoundsChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CandidateWindowBoundsChanged"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, CandidateWindowBoundsChangedEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_CandidateWindowBoundsChanged h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_CandidateWindowBoundsChanged h }
                )
            })

        [<Extension>]
        static member onContextMenuOpening(node: VirtualNode<TextBox>, handler: TextBox -> ContextMenuEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContextMenuOpening"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = ContextMenuOpeningEventHandler(fun _ e -> handler textBox e)
                    textBox.ContextMenuOpening.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = textBox.ContextMenuOpening.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onCopyingToClipboard(node: VirtualNode<TextBox>, handler: TextBox -> TextControlCopyingToClipboardEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CopyingToClipboard"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextControlCopyingToClipboardEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_CopyingToClipboard h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_CopyingToClipboard h }
                )
            })

        [<Extension>]
        static member onCuttingToClipboard(node: VirtualNode<TextBox>, handler: TextBox -> TextControlCuttingToClipboardEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CuttingToClipboard"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextControlCuttingToClipboardEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_CuttingToClipboard h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_CuttingToClipboard h }
                )
            })

        [<Extension>]
        static member onPaste(node: VirtualNode<TextBox>, handler: TextBox -> TextControlPasteEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Paste"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TextControlPasteEventHandler(fun _ e -> handler textBox e)
                    textBox.Paste.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = textBox.Paste.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<TextBox>, handler: TextBox -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = RoutedEventHandler(fun _ e -> handler textBox e)
                    textBox.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = textBox.SelectionChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onSelectionChanging(node: VirtualNode<TextBox>, handler: TextBox -> TextBoxSelectionChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanging"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextBoxSelectionChangingEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_SelectionChanging h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_SelectionChanging h }
                )
            })

        [<Extension>]
        static member onTextChanged(node: VirtualNode<TextBox>, handler: TextBox -> TextChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextChanged"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TextChangedEventHandler(fun _ e -> handler textBox e)
                    textBox.TextChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = textBox.TextChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onTextChanging(node: VirtualNode<TextBox>, handler: TextBox -> TextBoxTextChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextChanging"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextBoxTextChangingEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_TextChanging h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_TextChanging h }
                )
            })

        [<Extension>]
        static member onTextCompositionChanged(node: VirtualNode<TextBox>, handler: TextBox -> TextCompositionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextCompositionChanged"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextCompositionChangedEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_TextCompositionChanged h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_TextCompositionChanged h }
                )
            })

        [<Extension>]
        static member onTextCompositionEnded(node: VirtualNode<TextBox>, handler: TextBox -> TextCompositionEndedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextCompositionEnded"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextCompositionEndedEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_TextCompositionEnded h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_TextCompositionEnded h }
                )
            })

        [<Extension>]
        static member onTextCompositionStarted(node: VirtualNode<TextBox>, handler: TextBox -> TextCompositionStartedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextCompositionStarted"
                Subscribe = (fun uiElement ->
                    let textBox = uiElement :?> TextBox
                    let h = TypedEventHandler<TextBox, TextCompositionStartedEventArgs>(fun _ e -> handler textBox e)
                    textBox.add_TextCompositionStarted h
                    { new IDisposable with
                        member _.Dispose() = textBox.remove_TextCompositionStarted h }
                )
            })

    let textBox () =
        createNode<TextBox> textBoxNodeId
