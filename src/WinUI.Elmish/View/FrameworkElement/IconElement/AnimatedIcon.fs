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


[<AutoOpen>]
module AnimatedIconMod =

    let private animatedIconNodeId = getNextId ()

    registerDefaultRenderer<AnimatedIcon> animatedIconNodeId

    let private fallbackIconSourcePropertyId = getNextId ()
    let private mirroredWhenRightToLeftPropertyId = getNextId ()
    let private sourcePropertyId = getNextId ()
    let private statePropertyId = getNextId ()

    [<Extension>]
    type AnimatedIconExtensions =

        [<Extension>]
        static member fallbackIconSource(node: VirtualNode<AnimatedIcon>, value: IconSource) =
            createDependencyProperty fallbackIconSourcePropertyId AnimatedIcon.FallbackIconSourceProperty value
            |> node.set

        [<Extension>]
        static member mirroredWhenRightToLeft(node: VirtualNode<AnimatedIcon>, value: bool) =
            createDependencyProperty mirroredWhenRightToLeftPropertyId AnimatedIcon.MirroredWhenRightToLeftProperty value
            |> node.set

        [<Extension>]
        static member source(node: VirtualNode<AnimatedIcon>, value: IAnimatedVisualSource2) =
            createDependencyProperty sourcePropertyId AnimatedIcon.SourceProperty value
            |> node.set

        [<Extension>]
        static member animatedIcon_state<'m when 'm :> DependencyObject and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty statePropertyId AnimatedIcon.StateProperty value
            |> node.set

    let animatedIcon () =
        createNode<AnimatedIcon> animatedIconNodeId
