namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.UI.Text

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module FontIconMod =

    let private fontIconNodeId = getNextId ()

    registerDefaultRenderer<FontIcon> fontIconNodeId

    let private fontFamilyPropertyId = getNextId ()
    let private fontSizePropertyId = getNextId ()
    let private fontStylePropertyId = getNextId ()
    let private fontWeightPropertyId = getNextId ()
    let private glyphPropertyId = getNextId ()
    let private isTextScaleFactorEnabledPropertyId = getNextId ()
    let private mirroredWhenRightToLeftPropertyId = getNextId ()

    [<Extension>]
    type FontIconExtensions =

        [<Extension>]
        static member fontFamily(node: VirtualNode<FontIcon>, value: FontFamily) =
            createDependencyProperty fontFamilyPropertyId FontIcon.FontFamilyProperty value
            |> node.set

        [<Extension>]
        static member fontSize(node: VirtualNode<FontIcon>, value: float) =
            createDependencyProperty fontSizePropertyId FontIcon.FontSizeProperty value
            |> node.set

        [<Extension>]
        static member fontStyle(node: VirtualNode<FontIcon>, value: FontStyle) =
            createDependencyProperty fontStylePropertyId FontIcon.FontStyleProperty value
            |> node.set

        [<Extension>]
        static member fontWeight(node: VirtualNode<FontIcon>, value: FontWeight) =
            createDependencyProperty fontWeightPropertyId FontIcon.FontWeightProperty value
            |> node.set

        [<Extension>]
        static member glyph(node: VirtualNode<FontIcon>, value: string) =
            createDependencyProperty glyphPropertyId FontIcon.GlyphProperty value
            |> node.set

        [<Extension>]
        static member isTextScaleFactorEnabled(node: VirtualNode<FontIcon>, value: bool) =
            createDependencyProperty isTextScaleFactorEnabledPropertyId FontIcon.IsTextScaleFactorEnabledProperty value
            |> node.set

        [<Extension>]
        static member mirroredWhenRightToLeft(node: VirtualNode<FontIcon>, value: bool) =
            createDependencyProperty mirroredWhenRightToLeftPropertyId FontIcon.MirroredWhenRightToLeftProperty value
            |> node.set

    let fontIcon () =
        createNode<FontIcon> fontIconNodeId

    let fontIcon' (glyph: string) =
        fontIcon()
            .glyph(glyph)
