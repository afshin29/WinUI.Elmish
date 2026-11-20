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

// skipped property ExtentHeight: read-only
// skipped property ExtentWidth: read-only
// skipped property HorizontalOffset: read-only
// skipped property VerticalOffset: read-only
// skipped property ViewportHeight: read-only
// skipped property ViewportWidth: read-only


[<AutoOpen>]
module ScrollContentPresenterMod =

    let private scrollContentPresenterNodeId = getNextId ()

    registerDefaultRenderer<ScrollContentPresenter> scrollContentPresenterNodeId

    let private canContentRenderOutsideBoundsPropertyId = getNextId ()
    let private sizesContentToTemplatedParentPropertyId = getNextId ()
    let private canHorizontallyScrollPropertyId = getNextId ()
    let private canVerticallyScrollPropertyId = getNextId ()
    let private scrollOwnerPropertyId = getNextId ()

    [<Extension>]
    type ScrollContentPresenterExtensions =

        [<Extension>]
        static member canContentRenderOutsideBounds(node: VirtualNode<ScrollContentPresenter>, value: bool) =
            createDependencyProperty canContentRenderOutsideBoundsPropertyId ScrollContentPresenter.CanContentRenderOutsideBoundsProperty value
            |> node.set

        [<Extension>]
        static member sizesContentToTemplatedParent(node: VirtualNode<ScrollContentPresenter>, value: bool) =
            createDependencyProperty sizesContentToTemplatedParentPropertyId ScrollContentPresenter.SizesContentToTemplatedParentProperty value
            |> node.set

        [<Extension>]
        static member canHorizontallyScroll(node: VirtualNode<ScrollContentPresenter>, value: bool) =
            createClrProperty
                canHorizontallyScrollPropertyId
                (fun uiElement _ -> (uiElement :?> ScrollContentPresenter).CanHorizontallyScroll <- value)
                (fun uiElement -> (uiElement :?> ScrollContentPresenter).CanHorizontallyScroll <- Unchecked.defaultof<bool>)
                value
            |> node.set

        [<Extension>]
        static member canVerticallyScroll(node: VirtualNode<ScrollContentPresenter>, value: bool) =
            createClrProperty
                canVerticallyScrollPropertyId
                (fun uiElement _ -> (uiElement :?> ScrollContentPresenter).CanVerticallyScroll <- value)
                (fun uiElement -> (uiElement :?> ScrollContentPresenter).CanVerticallyScroll <- Unchecked.defaultof<bool>)
                value
            |> node.set

        [<Extension>]
        static member scrollOwner(node: VirtualNode<ScrollContentPresenter>, value: obj) =
            createClrProperty
                scrollOwnerPropertyId
                (fun uiElement _ -> (uiElement :?> ScrollContentPresenter).ScrollOwner <- value)
                (fun uiElement -> (uiElement :?> ScrollContentPresenter).ScrollOwner <- Unchecked.defaultof<obj>)
                value
            |> node.set

    let scrollContentPresenter () =
        createNode<ScrollContentPresenter> scrollContentPresenterNodeId
