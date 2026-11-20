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

// skipped property MenuKind: read-only


[<AutoOpen>]
module InkToolbarMenuButtonMod =

    let private isExtensionGlyphShownPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarMenuButtonExtensions =

        [<Extension>]
        static member isExtensionGlyphShown<'m when 'm :> InkToolbarMenuButton and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isExtensionGlyphShownPropertyId InkToolbarMenuButton.IsExtensionGlyphShownProperty value
            |> node.set
