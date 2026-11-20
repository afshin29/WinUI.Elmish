namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open Microsoft.UI.Xaml.Media.Animation
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property IsItemsHostProperty: read-only


[<AutoOpen>]
module PanelMod =

    let private backgroundPropertyId = getNextId ()
    let private childrenTransitionsPropertyId = getNextId ()
    let private backgroundTransitionPropertyId = getNextId ()
    let private childrenPropertyId = getNextId ()

    [<Extension>]
    type PanelExtensions =

        [<Extension>]
        static member background<'m when 'm :> Panel and 'm: not struct>
            (node: VirtualNode<'m>, value: Brush) =
            createDependencyProperty backgroundPropertyId Panel.BackgroundProperty value
            |> node.set

        [<Extension>]
        static member childrenTransitions<'m when 'm :> Panel and 'm: not struct>
            (node: VirtualNode<'m>, value: TransitionCollection) =
            createDependencyProperty childrenTransitionsPropertyId Panel.ChildrenTransitionsProperty value
            |> node.set

        [<Extension>]
        static member backgroundTransition<'m when 'm :> Panel and 'm: not struct>
            (node: VirtualNode<'m>, value: BrushTransition) =
            createClrProperty
                backgroundTransitionPropertyId
                (fun uiElement _ -> (uiElement :?> Panel).BackgroundTransition <- value)
                (fun uiElement -> (uiElement :?> Panel).BackgroundTransition <- Unchecked.defaultof<BrushTransition>)
                value
            |> node.set

        [<Extension>]
        static member children<'m when 'm :> Panel and 'm: not struct>
            (node: VirtualNode<'m>, value: IVirtualNode list) =
            createInnerChildrenProperty
                childrenPropertyId
                value
                (fun element children -> children |> List.iter (element :?> Panel).Children.Add)
                (fun element -> (element :?> Panel).Children)
            |> node.set
