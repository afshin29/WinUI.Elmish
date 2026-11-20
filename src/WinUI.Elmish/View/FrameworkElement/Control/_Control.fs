namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Input
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

// skipped dependency property DefaultStyleKeyProperty: no wrapper property and no usable attached-property accessors


[<AutoOpen>]
module ControlMod =

    let private backgroundPropertyId = getNextId ()
    let private backgroundSizingPropertyId = getNextId ()
    let private borderBrushPropertyId = getNextId ()
    let private borderThicknessPropertyId = getNextId ()
    let private characterSpacingPropertyId = getNextId ()
    let private cornerRadiusPropertyId = getNextId ()
    let private defaultStyleResourceUriPropertyId = getNextId ()
    let private elementSoundModePropertyId = getNextId ()
    let private fontFamilyPropertyId = getNextId ()
    let private fontSizePropertyId = getNextId ()
    let private fontStretchPropertyId = getNextId ()
    let private fontStylePropertyId = getNextId ()
    let private fontWeightPropertyId = getNextId ()
    let private foregroundPropertyId = getNextId ()
    let private horizontalContentAlignmentPropertyId = getNextId ()
    let private isEnabledPropertyId = getNextId ()
    let private isFocusEngagedPropertyId = getNextId ()
    let private isFocusEngagementEnabledPropertyId = getNextId ()
    let private isTemplateFocusTargetPropertyId = getNextId ()
    let private isTemplateKeyTipTargetPropertyId = getNextId ()
    let private isTextScaleFactorEnabledPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private requiresPointerPropertyId = getNextId ()
    let private tabNavigationPropertyId = getNextId ()
    let private templatePropertyId = getNextId ()
    let private verticalContentAlignmentPropertyId = getNextId ()

    [<Extension>]
    type ControlExtensions =

        [<Extension>]
        static member background<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty backgroundPropertyId Control.BackgroundProperty value
            |> node.set

        [<Extension>]
        static member backgroundSizing<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: BackgroundSizing) =
            createDependencyProperty backgroundSizingPropertyId Control.BackgroundSizingProperty value
            |> node.set

        [<Extension>]
        static member borderBrush<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty borderBrushPropertyId Control.BorderBrushProperty value
            |> node.set

        [<Extension>]
        static member borderThickness<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty borderThicknessPropertyId Control.BorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member characterSpacing<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: int) =
            createDependencyProperty characterSpacingPropertyId Control.CharacterSpacingProperty value
            |> node.set

        [<Extension>]
        static member cornerRadius<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId Control.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member defaultStyleResourceUri<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: Uri) =
            createDependencyProperty defaultStyleResourceUriPropertyId Control.DefaultStyleResourceUriProperty value
            |> node.set

        [<Extension>]
        static member elementSoundMode<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: ElementSoundMode) =
            createDependencyProperty elementSoundModePropertyId Control.ElementSoundModeProperty value
            |> node.set

        [<Extension>]
        static member fontFamily<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: FontFamily) =
            createDependencyProperty fontFamilyPropertyId Control.FontFamilyProperty value
            |> node.set

        [<Extension>]
        static member fontSize<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty fontSizePropertyId Control.FontSizeProperty value
            |> node.set

        [<Extension>]
        static member fontStretch<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: FontStretch) =
            createDependencyProperty fontStretchPropertyId Control.FontStretchProperty value
            |> node.set

        [<Extension>]
        static member fontStyle<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: FontStyle) =
            createDependencyProperty fontStylePropertyId Control.FontStyleProperty value
            |> node.set

        [<Extension>]
        static member fontWeight<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: FontWeight) =
            createDependencyProperty fontWeightPropertyId Control.FontWeightProperty value
            |> node.set

        [<Extension>]
        static member foreground<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty foregroundPropertyId Control.ForegroundProperty value
            |> node.set

        [<Extension>]
        static member horizontalContentAlignment<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: HorizontalAlignment) =
            createDependencyProperty horizontalContentAlignmentPropertyId Control.HorizontalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member isEnabled<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isEnabledPropertyId Control.IsEnabledProperty value
            |> node.set

        [<Extension>]
        static member isFocusEngaged<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isFocusEngagedPropertyId Control.IsFocusEngagedProperty value
            |> node.set

        [<Extension>]
        static member isFocusEngagementEnabled<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isFocusEngagementEnabledPropertyId Control.IsFocusEngagementEnabledProperty value
            |> node.set

        [<Extension>]
        static member control_isTemplateFocusTarget<'m when 'm :> FrameworkElement and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isTemplateFocusTargetPropertyId Control.IsTemplateFocusTargetProperty value
            |> node.set

        [<Extension>]
        static member control_isTemplateKeyTipTarget<'m when 'm :> DependencyObject and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isTemplateKeyTipTargetPropertyId Control.IsTemplateKeyTipTargetProperty value
            |> node.set

        [<Extension>]
        static member isTextScaleFactorEnabled<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isTextScaleFactorEnabledPropertyId Control.IsTextScaleFactorEnabledProperty value
            |> node.set

        [<Extension>]
        static member padding<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: Thickness) =
            createDependencyProperty paddingPropertyId Control.PaddingProperty value
            |> node.set

        [<Extension>]
        static member requiresPointer<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: RequiresPointer) =
            createDependencyProperty requiresPointerPropertyId Control.RequiresPointerProperty value
            |> node.set

        [<Extension>]
        static member tabNavigation<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: KeyboardNavigationMode) =
            createDependencyProperty tabNavigationPropertyId Control.TabNavigationProperty value
            |> node.set

        [<Extension>]
        static member template<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: ControlTemplate) =
            createDependencyProperty templatePropertyId Control.TemplateProperty value
            |> node.set

        [<Extension>]
        static member verticalContentAlignment<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, value: VerticalAlignment) =
            createDependencyProperty verticalContentAlignmentPropertyId Control.VerticalContentAlignmentProperty value
            |> node.set

        [<Extension>]
        static member onFocusDisengaged<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, handler: FocusDisengagedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "FocusDisengaged"
                Subscribe = (fun uiElement ->
                    let control = uiElement :?> Control
                    let h = TypedEventHandler<Control, FocusDisengagedEventArgs>(fun _ e -> handler e)
                    control.add_FocusDisengaged h
                    { new IDisposable with
                        member _.Dispose() = control.remove_FocusDisengaged h }
                )
            })

        [<Extension>]
        static member onFocusEngaged<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, handler: FocusEngagedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "FocusEngaged"
                Subscribe = (fun uiElement ->
                    let control = uiElement :?> Control
                    let h = TypedEventHandler<Control, FocusEngagedEventArgs>(fun _ e -> handler e)
                    control.add_FocusEngaged h
                    { new IDisposable with
                        member _.Dispose() = control.remove_FocusEngaged h }
                )
            })

        [<Extension>]
        static member onIsEnabledChanged<'m when 'm :> Control and 'm: not struct>
            (node: VirtualNode<'m>, handler: DependencyPropertyChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "IsEnabledChanged"
                Subscribe = (fun uiElement ->
                    let control = uiElement :?> Control
                    let h = DependencyPropertyChangedEventHandler(fun _ e -> handler e)
                    control.IsEnabledChanged.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = control.IsEnabledChanged.RemoveHandler h }
                )
            })
