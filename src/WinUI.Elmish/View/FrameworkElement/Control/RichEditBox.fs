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

// skipped dependency property ProofingMenuFlyoutProperty: read-only
// skipped property Document: read-only
// skipped property TextDocument: read-only


[<AutoOpen>]
module RichEditBoxMod =

    let private richEditBoxNodeId = getNextId ()

    registerDefaultRenderer<RichEditBox> richEditBoxNodeId

    let private acceptsReturnPropertyId = getNextId ()
    let private characterCasingPropertyId = getNextId ()
    let private clipboardCopyFormatPropertyId = getNextId ()
    let private descriptionPropertyId = getNextId ()
    let private desiredCandidateWindowAlignmentPropertyId = getNextId ()
    let private disabledFormattingAcceleratorsPropertyId = getNextId ()
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
    let private placeholderTextPropertyId = getNextId ()
    let private preventKeyboardDisplayOnProgrammaticFocusPropertyId = getNextId ()
    let private selectionFlyoutPropertyId = getNextId ()
    let private selectionHighlightColorPropertyId = getNextId ()
    let private selectionHighlightColorWhenNotFocusedPropertyId = getNextId ()
    let private textAlignmentPropertyId = getNextId ()
    let private textReadingOrderPropertyId = getNextId ()
    let private textWrappingPropertyId = getNextId ()

    [<Extension>]
    type RichEditBoxExtensions =

        [<Extension>]
        static member acceptsReturn(node: VirtualNode<RichEditBox>, value: bool) =
            createDependencyProperty acceptsReturnPropertyId RichEditBox.AcceptsReturnProperty value
            |> node.set

        [<Extension>]
        static member characterCasing(node: VirtualNode<RichEditBox>, value: CharacterCasing) =
            createDependencyProperty characterCasingPropertyId RichEditBox.CharacterCasingProperty value
            |> node.set

        [<Extension>]
        static member clipboardCopyFormat(node: VirtualNode<RichEditBox>, value: RichEditClipboardFormat) =
            createDependencyProperty clipboardCopyFormatPropertyId RichEditBox.ClipboardCopyFormatProperty value
            |> node.set

        [<Extension>]
        static member description(node: VirtualNode<RichEditBox>, value: obj) =
            createDependencyProperty descriptionPropertyId RichEditBox.DescriptionProperty value
            |> node.set

        [<Extension>]
        static member desiredCandidateWindowAlignment(node: VirtualNode<RichEditBox>, value: CandidateWindowAlignment) =
            createDependencyProperty desiredCandidateWindowAlignmentPropertyId RichEditBox.DesiredCandidateWindowAlignmentProperty value
            |> node.set

        [<Extension>]
        static member disabledFormattingAccelerators(node: VirtualNode<RichEditBox>, value: DisabledFormattingAccelerators) =
            createDependencyProperty disabledFormattingAcceleratorsPropertyId RichEditBox.DisabledFormattingAcceleratorsProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<RichEditBox>, value: string) =
            createDependencyProperty str_headerPropertyId RichEditBox.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<RichEditBox>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> RichEditBox).Header <- child)
                (fun element -> (element :?> RichEditBox).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<RichEditBox>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId RichEditBox.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member horizontalTextAlignment(node: VirtualNode<RichEditBox>, value: TextAlignment) =
            createDependencyProperty horizontalTextAlignmentPropertyId RichEditBox.HorizontalTextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member inputScope(node: VirtualNode<RichEditBox>, value: InputScope) =
            createDependencyProperty inputScopePropertyId RichEditBox.InputScopeProperty value
            |> node.set

        [<Extension>]
        static member isColorFontEnabled(node: VirtualNode<RichEditBox>, value: bool) =
            createDependencyProperty isColorFontEnabledPropertyId RichEditBox.IsColorFontEnabledProperty value
            |> node.set

        [<Extension>]
        static member isReadOnly(node: VirtualNode<RichEditBox>, value: bool) =
            createDependencyProperty isReadOnlyPropertyId RichEditBox.IsReadOnlyProperty value
            |> node.set

        [<Extension>]
        static member isSpellCheckEnabled(node: VirtualNode<RichEditBox>, value: bool) =
            createDependencyProperty isSpellCheckEnabledPropertyId RichEditBox.IsSpellCheckEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTextPredictionEnabled(node: VirtualNode<RichEditBox>, value: bool) =
            createDependencyProperty isTextPredictionEnabledPropertyId RichEditBox.IsTextPredictionEnabledProperty value
            |> node.set

        [<Extension>]
        static member maxLength(node: VirtualNode<RichEditBox>, value: int) =
            createDependencyProperty maxLengthPropertyId RichEditBox.MaxLengthProperty value
            |> node.set

        [<Extension>]
        static member placeholderText(node: VirtualNode<RichEditBox>, value: string) =
            createDependencyProperty placeholderTextPropertyId RichEditBox.PlaceholderTextProperty value
            |> node.set

        [<Extension>]
        static member preventKeyboardDisplayOnProgrammaticFocus(node: VirtualNode<RichEditBox>, value: bool) =
            createDependencyProperty preventKeyboardDisplayOnProgrammaticFocusPropertyId RichEditBox.PreventKeyboardDisplayOnProgrammaticFocusProperty value
            |> node.set

        [<Extension>]
        static member selectionFlyout(node: VirtualNode<RichEditBox>, value: FlyoutBase) =
            createDependencyProperty selectionFlyoutPropertyId RichEditBox.SelectionFlyoutProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColor(node: VirtualNode<RichEditBox>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorPropertyId RichEditBox.SelectionHighlightColorProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColorWhenNotFocused(node: VirtualNode<RichEditBox>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorWhenNotFocusedPropertyId RichEditBox.SelectionHighlightColorWhenNotFocusedProperty value
            |> node.set

        [<Extension>]
        static member textAlignment(node: VirtualNode<RichEditBox>, value: TextAlignment) =
            createDependencyProperty textAlignmentPropertyId RichEditBox.TextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member textReadingOrder(node: VirtualNode<RichEditBox>, value: TextReadingOrder) =
            createDependencyProperty textReadingOrderPropertyId RichEditBox.TextReadingOrderProperty value
            |> node.set

        [<Extension>]
        static member textWrapping(node: VirtualNode<RichEditBox>, value: TextWrapping) =
            createDependencyProperty textWrappingPropertyId RichEditBox.TextWrappingProperty value
            |> node.set

        [<Extension>]
        static member onCandidateWindowBoundsChanged(node: VirtualNode<RichEditBox>, handler: CandidateWindowBoundsChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CandidateWindowBoundsChanged"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, CandidateWindowBoundsChangedEventArgs>(fun _ e -> handler e)
                    richEditBox.add_CandidateWindowBoundsChanged h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_CandidateWindowBoundsChanged h }
                )
            })

        [<Extension>]
        static member onContextMenuOpening(node: VirtualNode<RichEditBox>, handler: ContextMenuEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContextMenuOpening"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = ContextMenuOpeningEventHandler(fun _ e -> handler e)
                    richEditBox.ContextMenuOpening.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.ContextMenuOpening.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onCopyingToClipboard(node: VirtualNode<RichEditBox>, handler: TextControlCopyingToClipboardEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CopyingToClipboard"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, TextControlCopyingToClipboardEventArgs>(fun _ e -> handler e)
                    richEditBox.add_CopyingToClipboard h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_CopyingToClipboard h }
                )
            })

        [<Extension>]
        static member onCuttingToClipboard(node: VirtualNode<RichEditBox>, handler: TextControlCuttingToClipboardEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "CuttingToClipboard"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, TextControlCuttingToClipboardEventArgs>(fun _ e -> handler e)
                    richEditBox.add_CuttingToClipboard h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_CuttingToClipboard h }
                )
            })

        [<Extension>]
        static member onPaste(node: VirtualNode<RichEditBox>, handler: TextControlPasteEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Paste"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TextControlPasteEventHandler(fun _ e -> handler e)
                    richEditBox.Paste.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.Paste.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<RichEditBox>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    richEditBox.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.SelectionChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onSelectionChanging(node: VirtualNode<RichEditBox>, handler: RichEditBoxSelectionChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanging"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, RichEditBoxSelectionChangingEventArgs>(fun _ e -> handler e)
                    richEditBox.add_SelectionChanging h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_SelectionChanging h }
                )
            })

        [<Extension>]
        static member onTextChanged(node: VirtualNode<RichEditBox>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextChanged"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    richEditBox.TextChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.TextChanged.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onTextChanging(node: VirtualNode<RichEditBox>, handler: RichEditBoxTextChangingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextChanging"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, RichEditBoxTextChangingEventArgs>(fun _ e -> handler e)
                    richEditBox.add_TextChanging h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_TextChanging h }
                )
            })

        [<Extension>]
        static member onTextCompositionChanged(node: VirtualNode<RichEditBox>, handler: TextCompositionChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextCompositionChanged"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, TextCompositionChangedEventArgs>(fun _ e -> handler e)
                    richEditBox.add_TextCompositionChanged h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_TextCompositionChanged h }
                )
            })

        [<Extension>]
        static member onTextCompositionEnded(node: VirtualNode<RichEditBox>, handler: TextCompositionEndedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextCompositionEnded"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, TextCompositionEndedEventArgs>(fun _ e -> handler e)
                    richEditBox.add_TextCompositionEnded h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_TextCompositionEnded h }
                )
            })

        [<Extension>]
        static member onTextCompositionStarted(node: VirtualNode<RichEditBox>, handler: TextCompositionStartedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextCompositionStarted"
                Subscribe = (fun uiElement ->
                    let richEditBox = uiElement :?> RichEditBox
                    let h = TypedEventHandler<RichEditBox, TextCompositionStartedEventArgs>(fun _ e -> handler e)
                    richEditBox.add_TextCompositionStarted h
                    { new IDisposable with
                        member _.Dispose() = richEditBox.remove_TextCompositionStarted h }
                )
            })

    let richEditBox () =
        createNode<RichEditBox> richEditBoxNodeId
