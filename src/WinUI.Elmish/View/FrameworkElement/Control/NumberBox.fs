namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Input
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation
open Windows.Globalization.NumberFormatting

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module NumberBoxMod =

    let private numberBoxNodeId = getNextId ()

    registerDefaultRenderer<NumberBox> numberBoxNodeId

    let private acceptsExpressionPropertyId = getNextId ()
    let private descriptionPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private inputScopePropertyId = getNextId ()
    let private isWrapEnabledPropertyId = getNextId ()
    let private largeChangePropertyId = getNextId ()
    let private maximumPropertyId = getNextId ()
    let private minimumPropertyId = getNextId ()
    let private numberFormatterPropertyId = getNextId ()
    let private placeholderTextPropertyId = getNextId ()
    let private preventKeyboardDisplayOnProgrammaticFocusPropertyId = getNextId ()
    let private selectionFlyoutPropertyId = getNextId ()
    let private selectionHighlightColorPropertyId = getNextId ()
    let private smallChangePropertyId = getNextId ()
    let private spinButtonPlacementModePropertyId = getNextId ()
    let private textAlignmentPropertyId = getNextId ()
    let private textPropertyId = getNextId ()
    let private textReadingOrderPropertyId = getNextId ()
    let private validationModePropertyId = getNextId ()
    let private valuePropertyId = getNextId ()

    [<Extension>]
    type NumberBoxExtensions =

        [<Extension>]
        static member acceptsExpression(node: VirtualNode<NumberBox>, value: bool) =
            createDependencyProperty acceptsExpressionPropertyId NumberBox.AcceptsExpressionProperty value
            |> node.set

        [<Extension>]
        static member description(node: VirtualNode<NumberBox>, value: obj) =
            createDependencyProperty descriptionPropertyId NumberBox.DescriptionProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<NumberBox>, value: string) =
            createDependencyProperty str_headerPropertyId NumberBox.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<NumberBox>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> NumberBox).Header <- child)
                (fun element -> (element :?> NumberBox).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<NumberBox>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId NumberBox.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member inputScope(node: VirtualNode<NumberBox>, value: InputScope) =
            createDependencyProperty inputScopePropertyId NumberBox.InputScopeProperty value
            |> node.set

        [<Extension>]
        static member isWrapEnabled(node: VirtualNode<NumberBox>, value: bool) =
            createDependencyProperty isWrapEnabledPropertyId NumberBox.IsWrapEnabledProperty value
            |> node.set

        [<Extension>]
        static member largeChange(node: VirtualNode<NumberBox>, value: float) =
            createDependencyProperty largeChangePropertyId NumberBox.LargeChangeProperty value
            |> node.set

        [<Extension>]
        static member maximum(node: VirtualNode<NumberBox>, value: float) =
            createDependencyProperty maximumPropertyId NumberBox.MaximumProperty value
            |> node.set

        [<Extension>]
        static member minimum(node: VirtualNode<NumberBox>, value: float) =
            createDependencyProperty minimumPropertyId NumberBox.MinimumProperty value
            |> node.set

        [<Extension>]
        static member numberFormatter(node: VirtualNode<NumberBox>, value: INumberFormatter2) =
            createDependencyProperty numberFormatterPropertyId NumberBox.NumberFormatterProperty value
            |> node.set

        [<Extension>]
        static member placeholderText(node: VirtualNode<NumberBox>, value: string) =
            createDependencyProperty placeholderTextPropertyId NumberBox.PlaceholderTextProperty value
            |> node.set

        [<Extension>]
        static member preventKeyboardDisplayOnProgrammaticFocus(node: VirtualNode<NumberBox>, value: bool) =
            createDependencyProperty preventKeyboardDisplayOnProgrammaticFocusPropertyId NumberBox.PreventKeyboardDisplayOnProgrammaticFocusProperty value
            |> node.set

        [<Extension>]
        static member selectionFlyout(node: VirtualNode<NumberBox>, value: FlyoutBase) =
            createDependencyProperty selectionFlyoutPropertyId NumberBox.SelectionFlyoutProperty value
            |> node.set

        [<Extension>]
        static member selectionHighlightColor(node: VirtualNode<NumberBox>, value: SolidColorBrush) =
            createDependencyProperty selectionHighlightColorPropertyId NumberBox.SelectionHighlightColorProperty value
            |> node.set

        [<Extension>]
        static member smallChange(node: VirtualNode<NumberBox>, value: float) =
            createDependencyProperty smallChangePropertyId NumberBox.SmallChangeProperty value
            |> node.set

        [<Extension>]
        static member spinButtonPlacementMode(node: VirtualNode<NumberBox>, value: NumberBoxSpinButtonPlacementMode) =
            createDependencyProperty spinButtonPlacementModePropertyId NumberBox.SpinButtonPlacementModeProperty value
            |> node.set

        [<Extension>]
        static member textAlignment(node: VirtualNode<NumberBox>, value: TextAlignment) =
            createDependencyProperty textAlignmentPropertyId NumberBox.TextAlignmentProperty value
            |> node.set

        [<Extension>]
        static member text(node: VirtualNode<NumberBox>, value: string) =
            createDependencyProperty textPropertyId NumberBox.TextProperty value
            |> node.set

        [<Extension>]
        static member textReadingOrder(node: VirtualNode<NumberBox>, value: TextReadingOrder) =
            createDependencyProperty textReadingOrderPropertyId NumberBox.TextReadingOrderProperty value
            |> node.set

        [<Extension>]
        static member validationMode(node: VirtualNode<NumberBox>, value: NumberBoxValidationMode) =
            createDependencyProperty validationModePropertyId NumberBox.ValidationModeProperty value
            |> node.set

        [<Extension>]
        static member value(node: VirtualNode<NumberBox>, value: float) =
            createDependencyProperty valuePropertyId NumberBox.ValueProperty value
            |> node.set

        [<Extension>]
        static member onValueChanged(node: VirtualNode<NumberBox>, handler: NumberBoxValueChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ValueChanged"
                Subscribe = (fun uiElement ->
                    let numberBox = uiElement :?> NumberBox
                    let h = TypedEventHandler<NumberBox, NumberBoxValueChangedEventArgs>(fun _ e -> handler e)
                    numberBox.add_ValueChanged h
                    { new IDisposable with
                        member _.Dispose() = numberBox.remove_ValueChanged h }
                )
            })

    let numberBox () =
        createNode<NumberBox> numberBoxNodeId
