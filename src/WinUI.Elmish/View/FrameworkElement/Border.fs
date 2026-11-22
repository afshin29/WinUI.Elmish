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


[<AutoOpen>]
module BorderMod =

    let private borderNodeId = getNextId ()

    registerDefaultRenderer<Border> borderNodeId

    let private backgroundPropertyId = getNextId ()
    let private backgroundSizingPropertyId = getNextId ()
    let private borderBrushPropertyId = getNextId ()
    let private borderThicknessPropertyId = getNextId ()
    let private childTransitionsPropertyId = getNextId ()
    let private cornerRadiusPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()
    let private backgroundTransitionPropertyId = getNextId ()
    let private childPropertyId = getNextId ()

    [<Extension>]
    type BorderExtensions =

        [<Extension>]
        static member background(node: VirtualNode<Border>, value: Brush) =
            createDependencyProperty backgroundPropertyId Border.BackgroundProperty value
            |> node.set

        [<Extension>]
        static member backgroundSizing(node: VirtualNode<Border>, value: BackgroundSizing) =
            createDependencyProperty backgroundSizingPropertyId Border.BackgroundSizingProperty value
            |> node.set

        [<Extension>]
        static member borderBrush(node: VirtualNode<Border>, value: Brush) =
            createDependencyProperty borderBrushPropertyId Border.BorderBrushProperty value
            |> node.set

        [<Extension>]
        static member borderThickness(node: VirtualNode<Border>, value: Thickness) =
            createDependencyProperty borderThicknessPropertyId Border.BorderThicknessProperty value
            |> node.set

        [<Extension>]
        static member childTransitions(node: VirtualNode<Border>, value: TransitionCollection) =
            createDependencyProperty childTransitionsPropertyId Border.ChildTransitionsProperty value
            |> node.set

        [<Extension>]
        static member childTransitions(node: VirtualNode<Border>, transitions: Transition seq, ?diffKey: obj) =
            let transitionCollection = TransitionCollection()
            transitions
            |> Seq.iter transitionCollection.Add

            createDependencyProperty childTransitionsPropertyId Border.ChildTransitionsProperty transitionCollection
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set


        [<Extension>]
        static member cornerRadius(node: VirtualNode<Border>, value: CornerRadius) =
            createDependencyProperty cornerRadiusPropertyId Border.CornerRadiusProperty value
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<Border>, value: Thickness) =
            createDependencyProperty paddingPropertyId Border.PaddingProperty value
            |> node.set

        [<Extension>]
        static member backgroundTransition(node: VirtualNode<Border>, value: BrushTransition) =
            createClrProperty
                backgroundTransitionPropertyId
                (fun uiElement _ -> (uiElement :?> Border).BackgroundTransition <- value)
                (fun uiElement -> (uiElement :?> Border).BackgroundTransition <- Unchecked.defaultof<BrushTransition>)
                value
            |> node.set

        [<Extension>]
        static member child(node: VirtualNode<Border>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                childPropertyId
                value
                (fun element child -> (element :?> Border).Child <- child)
                (fun element -> (element :?> Border).Child)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

    let border () =
        createNode<Border> borderNodeId
