namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation
open Windows.UI.Text

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property IsTextTrimmedProperty: read-only
// skipped dependency property SelectedTextProperty: read-only
// skipped property BaselineOffset: read-only
// skipped property ContentEnd: read-only
// skipped property ContentStart: read-only
// skipped property Inlines: read-only
// skipped property SelectionEnd: read-only
// skipped property SelectionStart: read-only
// skipped property TextHighlighters: read-only


[<AutoOpen>]
module TextBlockMod =

    let private textBlockNodeId = getNextId ()

    registerDefaultRenderer<TextBlock> textBlockNodeId

    let private characterSpacingPropertyId = getNextId ()
    let private fontFamilyPropertyId = getNextId ()
    let private fontSizePropertyId = getNextId ()
    let private fontStretchPropertyId = getNextId ()
    let private fontStylePropertyId = getNextId ()
    let private fontWeightPropertyId = getNextId ()
    let private foregroundPropertyId = getNextId ()
    let private horizontalTextAlignmentPropertyId = getNextId ()
    let private isColorFontEnabledPropertyId = getNextId ()
    let private isTextScaleFactorEnabledPropertyId = getNextId ()
    let private isTextSelectionEnabledPropertyId = getNextId ()
    let private lineHeightPropertyId = getNextId ()
    let private lineStackingStrategyPropertyId = getNextId ()
    let private maxLinesPropertyId = getNextId ()
    let private opticalMarginAlignmentPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private selectionFlyoutPropertyId = getNextId ()
    let private selectionHighlightColorPropertyId = getNextId ()
    let private textAlignmentPropertyId = getNextId ()
    let private textDecorationsPropertyId = getNextId ()
    let private textLineBoundsPropertyId = getNextId ()
    let private textPropertyId = getNextId ()
    let private textReadingOrderPropertyId = getNextId ()
    let private textTrimmingPropertyId = getNextId ()
    let private textWrappingPropertyId = getNextId ()

    [<Extension>]
    type TextBlockExtensions =

        [<Extension>]
        static member characterSpacing(node: VirtualNode<TextBlock>, value: int) =
            createDependencyProperty characterSpacingPropertyId TextBlock.CharacterSpacingProperty value
            |> node.set

        [<Extension>]
        static member fontFamily(node: VirtualNode<TextBlock>, value: FontFamily) =
            createDependencyProperty fontFamilyPropertyId TextBlock.FontFamilyProperty value
            |> node.set

        [<Extension>]
        static member fontSize(node: VirtualNode<TextBlock>, value: float) =
            createDependencyProperty fontSizePropertyId TextBlock.FontSizeProperty value
            |> node.set

        [<Extension>]
        static member fontStretch(node: VirtualNode<TextBlock>, value: FontStretch) =
            createDependencyProperty fontStretchPropertyId TextBlock.FontStretchProperty value
            |> node.set

        [<Extension>]
        static member fontStyle(node: VirtualNode<TextBlock>, value: FontStyle) =
            createDependencyProperty fontStylePropertyId TextBlock.FontStyleProperty value
            |> node.set

        [<Extension>]
        static member fontWeight(node: VirtualNode<TextBlock>, value: FontWeight) =
            createDependencyProperty fontWeightPropertyId TextBlock.FontWeightProperty value
            |> node.set

        [<Extension>]
        static member foreground(node: VirtualNode<TextBlock>, value: Brush) =
            createDependencyProperty foregroundPropertyId TextBlock.ForegroundProperty value
            |> node.set

        [<Extension>]
        static member horizontalTextAlignment(node: VirtualNode<TextBlock>, value: TextAlignment) =
            createDependencyProperty horizontalTextAlignmentPropertyId TextBlock.HorizontalTextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member isColorFontEnabled(node: VirtualNode<TextBlock>, value: bool) =
            createDependencyProperty isColorFontEnabledPropertyId TextBlock.IsColorFontEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTextScaleFactorEnabled(node: VirtualNode<TextBlock>, value: bool) =
            createDependencyProperty isTextScaleFactorEnabledPropertyId TextBlock.IsTextScaleFactorEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTextSelectionEnabled(node: VirtualNode<TextBlock>, value: bool) =
            createDependencyProperty isTextSelectionEnabledPropertyId TextBlock.IsTextSelectionEnabledProperty value
            |> node.set

        [<Extension>]
        static member lineHeight(node: VirtualNode<TextBlock>, value: float) =
            createDependencyProperty lineHeightPropertyId TextBlock.LineHeightProperty value
            |> node.set

        [<Extension>]
        static member lineStackingStrategy(node: VirtualNode<TextBlock>, value: LineStackingStrategy) =
            createDependencyProperty lineStackingStrategyPropertyId TextBlock.LineStackingStrategyProperty value
            |> node.set

        [<Extension>]
        static member maxLines(node: VirtualNode<TextBlock>, value: int) =
            createDependencyProperty maxLinesPropertyId TextBlock.MaxLinesProperty value
            |> node.set

        [<Extension>]
        static member opticalMarginAlignment(node: VirtualNode<TextBlock>, value: OpticalMarginAlignment) =
            createDependencyProperty opticalMarginAlignmentPropertyId TextBlock.OpticalMarginAlignmentProperty value
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<TextBlock>, value: Thickness) =
            createDependencyProperty paddingPropertyId TextBlock.PaddingProperty value
            |> node.set

        [<Extension>]
        static member selectionFlyout(node: VirtualNode<TextBlock>, value: FlyoutBase) =
            createDependencyProperty selectionFlyoutPropertyId TextBlock.SelectionFlyoutProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColor(node: VirtualNode<TextBlock>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorPropertyId TextBlock.SelectionHighlightColorProperty value
            |> node.set

        [<Extension>]
        static member textAlignment(node: VirtualNode<TextBlock>, value: TextAlignment) =
            createDependencyProperty textAlignmentPropertyId TextBlock.TextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member textDecorations(node: VirtualNode<TextBlock>, value: TextDecorations) =
            createDependencyProperty textDecorationsPropertyId TextBlock.TextDecorationsProperty value
            |> node.set

        [<Extension>]
        static member textLineBounds(node: VirtualNode<TextBlock>, value: TextLineBounds) =
            createDependencyProperty textLineBoundsPropertyId TextBlock.TextLineBoundsProperty value
            |> node.set

        [<Extension>]
        static member text(node: VirtualNode<TextBlock>, value: string) =
            createDependencyProperty textPropertyId TextBlock.TextProperty value
            |> node.set

        [<Extension>]
        static member textReadingOrder(node: VirtualNode<TextBlock>, value: TextReadingOrder) =
            createDependencyProperty textReadingOrderPropertyId TextBlock.TextReadingOrderProperty value
            |> node.set

        [<Extension>]
        static member textTrimming(node: VirtualNode<TextBlock>, value: TextTrimming) =
            createDependencyProperty textTrimmingPropertyId TextBlock.TextTrimmingProperty value
            |> node.set

        [<Extension>]
        static member textWrapping(node: VirtualNode<TextBlock>, value: TextWrapping) =
            createDependencyProperty textWrappingPropertyId TextBlock.TextWrappingProperty value
            |> node.set

        [<Extension>]
        static member onContextMenuOpening(node: VirtualNode<TextBlock>, handler: ContextMenuEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContextMenuOpening"
                Subscribe = (fun uiElement ->
                    let textBlock = uiElement :?> TextBlock
                    let h = ContextMenuOpeningEventHandler(fun _ e -> handler e)
                    textBlock.ContextMenuOpening.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = textBlock.ContextMenuOpening.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onIsTextTrimmedChanged(node: VirtualNode<TextBlock>, handler: IsTextTrimmedChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "IsTextTrimmedChanged"
                Subscribe = (fun uiElement ->
                    let textBlock = uiElement :?> TextBlock
                    let h = TypedEventHandler<TextBlock, IsTextTrimmedChangedEventArgs>(fun _ e -> handler e)
                    textBlock.add_IsTextTrimmedChanged h
                    { new IDisposable with
                        member _.Dispose() = textBlock.remove_IsTextTrimmedChanged h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<TextBlock>, handler: RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let textBlock = uiElement :?> TextBlock
                    let h = RoutedEventHandler(fun _ e -> handler e)
                    textBlock.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = textBlock.SelectionChanged.RemoveHandler h }
                )
            })

    let textBlock () =
        createNode<TextBlock> textBlockNodeId

    let textBlock' (text: string) =
        textBlock()
            .text(text)
    
    let textBlock_caption (text: string) =
        textBlock'(text)
            .style(getStaticResource "CaptionTextBlockStyle")
    
    let textBlock_body (text: string) =
        textBlock'(text)
            .style(getStaticResource "BodyTextBlockStyle")
    
    let textBlock_bodyStrong (text: string) =
        textBlock'(text)
            .style(getStaticResource "BodyStrongTextBlockStyle")
    
    let textBlock_bodyLarge (text: string) =
        textBlock'(text)
            .style(getStaticResource "BodyLargeTextBlockStyle")
    
    let textBlock_bodyLargeStrong (text: string) =
        textBlock'(text)
            .style(getStaticResource "BodyLargeStrongTextBlockStyle")
    
    let textBlock_subtitle (text: string) =
        textBlock'(text)
            .style(getStaticResource "SubtitleTextBlockStyle")
    
    let textBlock_title (text: string) =
        textBlock'(text)
            .style(getStaticResource "TitleTextBlockStyle")
    
    let textBlock_titleLarge (text: string) =
        textBlock'(text)
            .style(getStaticResource "TitleLargeTextBlockStyle")
    
    let textBlock_display (text: string) =
        textBlock'(text)
            .style(getStaticResource "DisplayTextBlockStyle")