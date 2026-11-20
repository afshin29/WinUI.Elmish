namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property ChildrenProperty: read-only
// skipped dependency property InkDrawingAttributesProperty: read-only


[<AutoOpen>]
module InkToolbarMod =

    let private inkToolbarNodeId = getNextId ()

    registerDefaultRenderer<InkToolbar> inkToolbarNodeId

    let private activeToolPropertyId = getNextId ()
    let private buttonFlyoutPlacementPropertyId = getNextId ()
    let private initialControlsPropertyId = getNextId ()
    let private isRulerButtonCheckedPropertyId = getNextId ()
    let private isStencilButtonCheckedPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private targetInkCanvasPropertyId = getNextId ()
    let private targetInkPresenterPropertyId = getNextId ()

    [<Extension>]
    type InkToolbarExtensions =

        [<Extension>]
        static member activeTool(node: VirtualNode<InkToolbar>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                activeToolPropertyId
                value
                (fun element child -> (element :?> InkToolbar).ActiveTool <- (child :?> InkToolbarToolButton))
                (fun element -> (element :?> InkToolbar).ActiveTool :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member buttonFlyoutPlacement(node: VirtualNode<InkToolbar>, value: InkToolbarButtonFlyoutPlacement) =
            createDependencyProperty buttonFlyoutPlacementPropertyId InkToolbar.ButtonFlyoutPlacementProperty value
            |> node.set

        [<Extension>]
        static member initialControls(node: VirtualNode<InkToolbar>, value: InkToolbarInitialControls) =
            createDependencyProperty initialControlsPropertyId InkToolbar.InitialControlsProperty value
            |> node.set

        [<Extension>]
        static member isRulerButtonChecked(node: VirtualNode<InkToolbar>, value: bool) =
            createDependencyProperty isRulerButtonCheckedPropertyId InkToolbar.IsRulerButtonCheckedProperty value
            |> node.set

        [<Extension>]
        static member isStencilButtonChecked(node: VirtualNode<InkToolbar>, value: bool) =
            createDependencyProperty isStencilButtonCheckedPropertyId InkToolbar.IsStencilButtonCheckedProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<InkToolbar>, value: Orientation) =
            createDependencyProperty orientationPropertyId InkToolbar.OrientationProperty value
            |> node.set

        [<Extension>]
        static member targetInkCanvas(node: VirtualNode<InkToolbar>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                targetInkCanvasPropertyId
                value
                (fun element child -> (element :?> InkToolbar).TargetInkCanvas <- (child :?> InkCanvas))
                (fun element -> (element :?> InkToolbar).TargetInkCanvas :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member targetInkPresenter(node: VirtualNode<InkToolbar>, value: InkPresenter) =
            createDependencyProperty targetInkPresenterPropertyId InkToolbar.TargetInkPresenterProperty value
            |> node.set

        [<Extension>]
        static member onActiveToolChanged(node: VirtualNode<InkToolbar>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ActiveToolChanged"
                Subscribe = (fun uiElement ->
                    let inkToolbar = uiElement :?> InkToolbar
                    let h = TypedEventHandler<InkToolbar, obj>(fun _ e -> handler e)
                    inkToolbar.add_ActiveToolChanged h
                    { new IDisposable with
                        member _.Dispose() = inkToolbar.remove_ActiveToolChanged h }
                )
            })

        [<Extension>]
        static member onEraseAllClicked(node: VirtualNode<InkToolbar>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "EraseAllClicked"
                Subscribe = (fun uiElement ->
                    let inkToolbar = uiElement :?> InkToolbar
                    let h = TypedEventHandler<InkToolbar, obj>(fun _ e -> handler e)
                    inkToolbar.add_EraseAllClicked h
                    { new IDisposable with
                        member _.Dispose() = inkToolbar.remove_EraseAllClicked h }
                )
            })

        [<Extension>]
        static member onInkDrawingAttributesChanged(node: VirtualNode<InkToolbar>, handler: obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "InkDrawingAttributesChanged"
                Subscribe = (fun uiElement ->
                    let inkToolbar = uiElement :?> InkToolbar
                    let h = TypedEventHandler<InkToolbar, obj>(fun _ e -> handler e)
                    inkToolbar.add_InkDrawingAttributesChanged h
                    { new IDisposable with
                        member _.Dispose() = inkToolbar.remove_InkDrawingAttributesChanged h }
                )
            })

        [<Extension>]
        static member onIsStencilButtonCheckedChanged(node: VirtualNode<InkToolbar>, handler: InkToolbarIsStencilButtonCheckedChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "IsStencilButtonCheckedChanged"
                Subscribe = (fun uiElement ->
                    let inkToolbar = uiElement :?> InkToolbar
                    let h = TypedEventHandler<InkToolbar, InkToolbarIsStencilButtonCheckedChangedEventArgs>(fun _ e -> handler e)
                    inkToolbar.add_IsStencilButtonCheckedChanged h
                    { new IDisposable with
                        member _.Dispose() = inkToolbar.remove_IsStencilButtonCheckedChanged h }
                )
            })

    let inkToolbar () =
        createNode<InkToolbar> inkToolbarNodeId
