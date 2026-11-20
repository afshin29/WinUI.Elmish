namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Documents
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module GlyphsMod =

    let private glyphsNodeId = getNextId ()

    registerDefaultRenderer<Glyphs> glyphsNodeId

    let private colorFontPaletteIndexPropertyId = getNextId ()
    let private fillPropertyId = getNextId ()
    let private fontRenderingEmSizePropertyId = getNextId ()
    let private fontUriPropertyId = getNextId ()
    let private indicesPropertyId = getNextId ()
    let private isColorFontEnabledPropertyId = getNextId ()
    let private originXPropertyId = getNextId ()
    let private originYPropertyId = getNextId ()
    let private styleSimulationsPropertyId = getNextId ()
    let private unicodeStringPropertyId = getNextId ()

    [<Extension>]
    type GlyphsExtensions =

        [<Extension>]
        static member colorFontPaletteIndex(node: VirtualNode<Glyphs>, value: int) =
            createDependencyProperty colorFontPaletteIndexPropertyId Glyphs.ColorFontPaletteIndexProperty value
            |> node.set

        [<Extension>]
        static member fill(node: VirtualNode<Glyphs>, value: Brush) =
            createDependencyProperty fillPropertyId Glyphs.FillProperty value
            |> node.set

        [<Extension>]
        static member fontRenderingEmSize(node: VirtualNode<Glyphs>, value: float) =
            createDependencyProperty fontRenderingEmSizePropertyId Glyphs.FontRenderingEmSizeProperty value
            |> node.set

        [<Extension>]
        static member fontUri(node: VirtualNode<Glyphs>, value: Uri) =
            createDependencyProperty fontUriPropertyId Glyphs.FontUriProperty value
            |> node.set

        [<Extension>]
        static member indices(node: VirtualNode<Glyphs>, value: string) =
            createDependencyProperty indicesPropertyId Glyphs.IndicesProperty value
            |> node.set

        [<Extension>]
        static member isColorFontEnabled(node: VirtualNode<Glyphs>, value: bool) =
            createDependencyProperty isColorFontEnabledPropertyId Glyphs.IsColorFontEnabledProperty value
            |> node.set

        [<Extension>]
        static member originX(node: VirtualNode<Glyphs>, value: float) =
            createDependencyProperty originXPropertyId Glyphs.OriginXProperty value
            |> node.set

        [<Extension>]
        static member originY(node: VirtualNode<Glyphs>, value: float) =
            createDependencyProperty originYPropertyId Glyphs.OriginYProperty value
            |> node.set

        [<Extension>]
        static member styleSimulations(node: VirtualNode<Glyphs>, value: StyleSimulations) =
            createDependencyProperty styleSimulationsPropertyId Glyphs.StyleSimulationsProperty value
            |> node.set

        [<Extension>]
        static member unicodeString(node: VirtualNode<Glyphs>, value: string) =
            createDependencyProperty unicodeStringPropertyId Glyphs.UnicodeStringProperty value
            |> node.set

    let glyphs () =
        createNode<Glyphs> glyphsNodeId
