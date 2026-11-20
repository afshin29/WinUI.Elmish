namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property ToolKind: read-only


[<AutoOpen>]
module InkToolbarToolButtonMod =

    let private isExtensionGlyphShownPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarToolButtonExtensions =

        [<Extension>]
        static member isExtensionGlyphShown<'m when 'm :> InkToolbarToolButton and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isExtensionGlyphShownPropertyId InkToolbarToolButton.IsExtensionGlyphShownProperty value
            |> node.set
