namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module IconElementMod =

    let private foregroundPropertyId = getNextId ()

    [<Extension>]
    type IconElementExtensions =

        [<Extension>]
        static member foreground<'m when 'm :> IconElement and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty foregroundPropertyId IconElement.ForegroundProperty value
            |> node.set
