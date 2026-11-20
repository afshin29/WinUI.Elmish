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

// skipped dependency property ProtractorProperty: read-only
// skipped dependency property RulerProperty: read-only


[<AutoOpen>]
module InkToolbarStencilButtonMod =

    let private inkToolbarStencilButtonNodeId = getNextId ()

    registerDefaultRenderer<InkToolbarStencilButton> inkToolbarStencilButtonNodeId

    let private isProtractorItemVisiblePropertyId = getNextId ()
    let private isRulerItemVisiblePropertyId = getNextId ()
    let private selectedStencilPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarStencilButtonExtensions =

        [<Extension>]
        static member isProtractorItemVisible(node: VirtualNode<InkToolbarStencilButton>, value: bool) =
            createDependencyProperty isProtractorItemVisiblePropertyId InkToolbarStencilButton.IsProtractorItemVisibleProperty value
            |> node.set

        [<Extension>]
        static member isRulerItemVisible(node: VirtualNode<InkToolbarStencilButton>, value: bool) =
            createDependencyProperty isRulerItemVisiblePropertyId InkToolbarStencilButton.IsRulerItemVisibleProperty value
            |> node.set

        [<Extension>]
        static member selectedStencil(node: VirtualNode<InkToolbarStencilButton>, value: InkToolbarStencilKind) =
            createDependencyProperty selectedStencilPropertyId InkToolbarStencilButton.SelectedStencilProperty value
            |> node.set

    let inkToolbarStencilButton () =
        createNode<InkToolbarStencilButton> inkToolbarStencilButtonNodeId
