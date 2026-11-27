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

// skipped dependency property HasOverflowContentProperty: read-only
// skipped dependency property IsTextTrimmedProperty: read-only
// skipped dependency property SelectedTextProperty: read-only
// skipped property BaselineOffset: read-only
// skipped property Blocks: read-only
// skipped property ContentEnd: read-only
// skipped property ContentStart: read-only
// skipped property SelectionEnd: read-only
// skipped property SelectionStart: read-only
// skipped property TextHighlighters: read-only


[<AutoOpen>]
module RichTextBlockMod =

    let private richTextBlockNodeId = getNextId ()

    registerDefaultRenderer<RichTextBlock> richTextBlockNodeId

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
    let private overflowContentTargetPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private selectionFlyoutPropertyId = getNextId ()
    let private selectionHighlightColorPropertyId = getNextId ()
    let private textAlignmentPropertyId = getNextId ()
    let private textDecorationsPropertyId = getNextId ()
    let private textIndentPropertyId = getNextId ()
    let private textLineBoundsPropertyId = getNextId ()
    let private textReadingOrderPropertyId = getNextId ()
    let private textTrimmingPropertyId = getNextId ()
    let private textWrappingPropertyId = getNextId ()

    [<Extension>]
    type RichTextBlockExtensions =

        [<Extension>]
        static member characterSpacing(node: VirtualNode<RichTextBlock>, value: int) =
            createDependencyProperty characterSpacingPropertyId RichTextBlock.CharacterSpacingProperty value
            |> node.set

        [<Extension>]
        static member fontFamily(node: VirtualNode<RichTextBlock>, value: FontFamily) =
            createDependencyProperty fontFamilyPropertyId RichTextBlock.FontFamilyProperty value
            |> node.set

        [<Extension>]
        static member fontSize(node: VirtualNode<RichTextBlock>, value: float) =
            createDependencyProperty fontSizePropertyId RichTextBlock.FontSizeProperty value
            |> node.set

        [<Extension>]
        static member fontStretch(node: VirtualNode<RichTextBlock>, value: FontStretch) =
            createDependencyProperty fontStretchPropertyId RichTextBlock.FontStretchProperty value
            |> node.set

        [<Extension>]
        static member fontStyle(node: VirtualNode<RichTextBlock>, value: FontStyle) =
            createDependencyProperty fontStylePropertyId RichTextBlock.FontStyleProperty value
            |> node.set

        [<Extension>]
        static member fontWeight(node: VirtualNode<RichTextBlock>, value: FontWeight) =
            createDependencyProperty fontWeightPropertyId RichTextBlock.FontWeightProperty value
            |> node.set

        [<Extension>]
        static member foreground(node: VirtualNode<RichTextBlock>, value: Brush) =
            createDependencyProperty foregroundPropertyId RichTextBlock.ForegroundProperty value
            |> node.set

        [<Extension>]
        static member horizontalTextAlignment(node: VirtualNode<RichTextBlock>, value: TextAlignment) =
            createDependencyProperty horizontalTextAlignmentPropertyId RichTextBlock.HorizontalTextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member isColorFontEnabled(node: VirtualNode<RichTextBlock>, value: bool) =
            createDependencyProperty isColorFontEnabledPropertyId RichTextBlock.IsColorFontEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTextScaleFactorEnabled(node: VirtualNode<RichTextBlock>, value: bool) =
            createDependencyProperty isTextScaleFactorEnabledPropertyId RichTextBlock.IsTextScaleFactorEnabledProperty value
            |> node.set

        [<Extension>]
        static member isTextSelectionEnabled(node: VirtualNode<RichTextBlock>, value: bool) =
            createDependencyProperty isTextSelectionEnabledPropertyId RichTextBlock.IsTextSelectionEnabledProperty value
            |> node.set

        [<Extension>]
        static member lineHeight(node: VirtualNode<RichTextBlock>, value: float) =
            createDependencyProperty lineHeightPropertyId RichTextBlock.LineHeightProperty value
            |> node.set

        [<Extension>]
        static member lineStackingStrategy(node: VirtualNode<RichTextBlock>, value: LineStackingStrategy) =
            createDependencyProperty lineStackingStrategyPropertyId RichTextBlock.LineStackingStrategyProperty value
            |> node.set

        [<Extension>]
        static member maxLines(node: VirtualNode<RichTextBlock>, value: int) =
            createDependencyProperty maxLinesPropertyId RichTextBlock.MaxLinesProperty value
            |> node.set

        [<Extension>]
        static member opticalMarginAlignment(node: VirtualNode<RichTextBlock>, value: OpticalMarginAlignment) =
            createDependencyProperty opticalMarginAlignmentPropertyId RichTextBlock.OpticalMarginAlignmentProperty value
            |> node.set

        [<Extension>]
        static member overflowContentTarget(node: VirtualNode<RichTextBlock>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                overflowContentTargetPropertyId
                value
                (fun element child -> (element :?> RichTextBlock).OverflowContentTarget <- (child :?> RichTextBlockOverflow))
                (fun element -> (element :?> RichTextBlock).OverflowContentTarget :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<RichTextBlock>, value: Thickness) =
            createDependencyProperty paddingPropertyId RichTextBlock.PaddingProperty value
            |> node.set

        [<Extension>]
        static member selectionFlyout(node: VirtualNode<RichTextBlock>, value: FlyoutBase) =
            createDependencyProperty selectionFlyoutPropertyId RichTextBlock.SelectionFlyoutProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColor(node: VirtualNode<RichTextBlock>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorPropertyId RichTextBlock.SelectionHighlightColorProperty value
            |> node.set

        [<Extension>]
        static member textAlignment(node: VirtualNode<RichTextBlock>, value: TextAlignment) =
            createDependencyProperty textAlignmentPropertyId RichTextBlock.TextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member textDecorations(node: VirtualNode<RichTextBlock>, value: TextDecorations) =
            createDependencyProperty textDecorationsPropertyId RichTextBlock.TextDecorationsProperty value
            |> node.set

        [<Extension>]
        static member textIndent(node: VirtualNode<RichTextBlock>, value: float) =
            createDependencyProperty textIndentPropertyId RichTextBlock.TextIndentProperty value
            |> node.set

        [<Extension>]
        static member textLineBounds(node: VirtualNode<RichTextBlock>, value: TextLineBounds) =
            createDependencyProperty textLineBoundsPropertyId RichTextBlock.TextLineBoundsProperty value
            |> node.set

        [<Extension>]
        static member textReadingOrder(node: VirtualNode<RichTextBlock>, value: TextReadingOrder) =
            createDependencyProperty textReadingOrderPropertyId RichTextBlock.TextReadingOrderProperty value
            |> node.set

        [<Extension>]
        static member textTrimming(node: VirtualNode<RichTextBlock>, value: TextTrimming) =
            createDependencyProperty textTrimmingPropertyId RichTextBlock.TextTrimmingProperty value
            |> node.set

        [<Extension>]
        static member textWrapping(node: VirtualNode<RichTextBlock>, value: TextWrapping) =
            createDependencyProperty textWrappingPropertyId RichTextBlock.TextWrappingProperty value
            |> node.set

        [<Extension>]
        static member onContextMenuOpening(node: VirtualNode<RichTextBlock>, handler: RichTextBlock -> ContextMenuEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ContextMenuOpening"
                Subscribe = (fun uiElement ->
                    let richTextBlock = uiElement :?> RichTextBlock
                    let h = ContextMenuOpeningEventHandler(fun _ e -> handler richTextBlock e)
                    richTextBlock.ContextMenuOpening.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = richTextBlock.ContextMenuOpening.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onIsTextTrimmedChanged(node: VirtualNode<RichTextBlock>, handler: RichTextBlock -> IsTextTrimmedChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "IsTextTrimmedChanged"
                Subscribe = (fun uiElement ->
                    let richTextBlock = uiElement :?> RichTextBlock
                    let h = TypedEventHandler<RichTextBlock, IsTextTrimmedChangedEventArgs>(fun _ e -> handler richTextBlock e)
                    richTextBlock.add_IsTextTrimmedChanged h
                    { new IDisposable with
                        member _.Dispose() = richTextBlock.remove_IsTextTrimmedChanged h }
                )
            })

        [<Extension>]
        static member onSelectionChanged(node: VirtualNode<RichTextBlock>, handler: RichTextBlock -> RoutedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectionChanged"
                Subscribe = (fun uiElement ->
                    let richTextBlock = uiElement :?> RichTextBlock
                    let h = RoutedEventHandler(fun _ e -> handler richTextBlock e)
                    richTextBlock.SelectionChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = richTextBlock.SelectionChanged.RemoveHandler h }
                )
            })

    let richTextBlock () =
        createNode<RichTextBlock> richTextBlockNodeId
