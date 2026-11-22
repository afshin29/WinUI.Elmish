namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Runtime.CompilerServices
open Windows.UI.Text

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module ContentPresenterMod =

    let private contentPresenterNodeId = getNextId ()

    registerDefaultRenderer<ContentPresenter> contentPresenterNodeId

    let private backgroundPropertyId = getNextId ()
    let private backgroundSizingPropertyId = getNextId ()
    let private borderBrushPropertyId = getNextId ()
    let private borderThicknessPropertyId = getNextId ()
    let private characterSpacingPropertyId = getNextId ()
    let private str_contentPropertyId = getNextId ()
    let private vnode_contentPropertyId = getNextId ()
    let private contentTemplatePropertyId = getNextId ()
    let private contentTemplateSelectorPropertyId = getNextId ()
    let private contentTransitionsPropertyId = getNextId ()
    let private cornerRadiusPropertyId = getNextId ()
    let private fontFamilyPropertyId = getNextId ()
    let private fontSizePropertyId = getNextId ()
    let private fontStretchPropertyId = getNextId ()
    let private fontStylePropertyId = getNextId ()
    let private fontWeightPropertyId = getNextId ()
    let private foregroundPropertyId = getNextId ()
    let private horizontalContentAlignmentPropertyId = getNextId ()
    let private isTextScaleFactorEnabledPropertyId = getNextId ()
    let private lineHeightPropertyId = getNextId ()
    let private lineStackingStrategyPropertyId = getNextId ()
    let private maxLinesPropertyId = getNextId ()
    let private opticalMarginAlignmentPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private textLineBoundsPropertyId = getNextId ()
    let private textWrappingPropertyId = getNextId ()
    let private verticalContentAlignmentPropertyId = getNextId ()
    let private backgroundTransitionPropertyId = getNextId ()

    [<Extension>]
    type ContentPresenterExtensions =

        [<Extension>]
        static member background<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty backgroundPropertyId ContentPresenter.BackgroundProperty value
            |> node.set

        [<Extension>]
        static member backgroundSizing<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: BackgroundSizing) =
            createDependencyProperty backgroundSizingPropertyId ContentPresenter.BackgroundSizingProperty value
            |> node.set

        [<Extension>]
        static member borderBrush<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty borderBrushPropertyId ContentPresenter.BorderBrushProperty value
            |> node.set

        [<Extension>]
        static member borderThickness<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty borderThicknessPropertyId ContentPresenter.BorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member characterSpacing<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty characterSpacingPropertyId ContentPresenter.CharacterSpacingProperty value
            |> node.set

        [<Extension>]
        static member content<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty str_contentPropertyId ContentPresenter.ContentProperty value
            |> node.set

        [<Extension>]
        static member content<'m, 'vnode when 'm :> ContentPresenter and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_contentPropertyId
                value
                (fun element child -> (element :?> ContentPresenter).Content <- child)
                (fun element -> (element :?> ContentPresenter).Content :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member contentTemplate<'m, 'source, 'vnode when 'm :> ContentPresenter and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, template: ('source -> 'vnode)) =
            getDataTemplate template
            |> createDependencyProperty contentTemplatePropertyId ContentPresenter.ContentTemplateProperty
            |> node.set

        [<Extension>]
        static member contentTemplateSelector<'m, 'source, 'vnode when 'm :> ContentPresenter and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, templates: ('source -> 'vnode) seq, selector: ('source -> int)) =
            createTemplateSelector templates selector
            |> createDependencyProperty contentTemplateSelectorPropertyId ContentPresenter.ContentTemplateSelectorProperty
            |> node.set

        [<Extension>]
        static member contentTransitions<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty contentTransitionsPropertyId ContentPresenter.ContentTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member cornerRadius<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId ContentPresenter.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member fontFamily<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: FontFamily) =
            createDependencyProperty fontFamilyPropertyId ContentPresenter.FontFamilyProperty value
            |> node.set

        [<Extension>]
        static member fontSize<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty fontSizePropertyId ContentPresenter.FontSizeProperty value
            |> node.set

        [<Extension>]
        static member fontStretch<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: FontStretch) =
            createDependencyProperty fontStretchPropertyId ContentPresenter.FontStretchProperty value
            |> node.set

        [<Extension>]
        static member fontStyle<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: FontStyle) =
            createDependencyProperty fontStylePropertyId ContentPresenter.FontStyleProperty value
            |> node.set

        [<Extension>]
        static member fontWeight<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: FontWeight) =
            createDependencyProperty fontWeightPropertyId ContentPresenter.FontWeightProperty value
            |> node.set

        [<Extension>]
        static member foreground<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty foregroundPropertyId ContentPresenter.ForegroundProperty value
            |> node.set

        [<Extension>]
        static member horizontalContentAlignment<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: HorizontalAlignment) =
            createDependencyProperty horizontalContentAlignmentPropertyId ContentPresenter.HorizontalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member isTextScaleFactorEnabled<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isTextScaleFactorEnabledPropertyId ContentPresenter.IsTextScaleFactorEnabledProperty value
            |> node.set

        [<Extension>]
        static member lineHeight<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty lineHeightPropertyId ContentPresenter.LineHeightProperty value
            |> node.set

        [<Extension>]
        static member lineStackingStrategy<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: LineStackingStrategy) =
            createDependencyProperty lineStackingStrategyPropertyId ContentPresenter.LineStackingStrategyProperty value
            |> node.set

        [<Extension>]
        static member maxLines<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty maxLinesPropertyId ContentPresenter.MaxLinesProperty value
            |> node.set

        [<Extension>]
        static member opticalMarginAlignment<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: OpticalMarginAlignment) =
            createDependencyProperty opticalMarginAlignmentPropertyId ContentPresenter.OpticalMarginAlignmentProperty value
            |> node.set

        [<Extension>]
        static member padding<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty paddingPropertyId ContentPresenter.PaddingProperty value
            |> node.set

        [<Extension>]
        static member textLineBounds<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: TextLineBounds) =
            createDependencyProperty textLineBoundsPropertyId ContentPresenter.TextLineBoundsProperty value
            |> node.set

        [<Extension>]
        static member textWrapping<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: TextWrapping) =
            createDependencyProperty textWrappingPropertyId ContentPresenter.TextWrappingProperty value
            |> node.set

        [<Extension>]
        static member verticalContentAlignment<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: VerticalAlignment) =
            createDependencyProperty verticalContentAlignmentPropertyId ContentPresenter.VerticalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member backgroundTransition<'m when 'm :> ContentPresenter and 'm: not struct>
            (node: VirtualNode<'m>, value: BrushTransition) =
            createClrProperty
                backgroundTransitionPropertyId
                (fun uiElement _ -> (uiElement :?> ContentPresenter).BackgroundTransition <- value)
                (fun uiElement -> (uiElement :?> ContentPresenter).BackgroundTransition <- Unchecked.defaultof<BrushTransition>)
                value
            |> node.set

    let contentPresenter () =
        createNode<ContentPresenter> contentPresenterNodeId
