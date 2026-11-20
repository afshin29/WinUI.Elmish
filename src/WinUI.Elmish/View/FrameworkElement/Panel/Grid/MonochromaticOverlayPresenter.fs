namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices
open Windows.UI

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module MonochromaticOverlayPresenterMod =

    let private monochromaticOverlayPresenterNodeId = getNextId ()

    registerDefaultRenderer<MonochromaticOverlayPresenter> monochromaticOverlayPresenterNodeId

    let private replacementColorPropertyId = getNextId ()
    let private sourceElementPropertyId = getNextId ()

    [<Extension>]
    type MonochromaticOverlayPresenterExtensions =

        [<Extension>]
        static member replacementColor(node: VirtualNode<MonochromaticOverlayPresenter>, value: Color) =
            createDependencyProperty replacementColorPropertyId MonochromaticOverlayPresenter.ReplacementColorProperty value
            |> node.set

        [<Extension>]
        static member sourceElement(node: VirtualNode<MonochromaticOverlayPresenter>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                sourceElementPropertyId
                value
                (fun element child -> (element :?> MonochromaticOverlayPresenter).SourceElement <- child)
                (fun element -> (element :?> MonochromaticOverlayPresenter).SourceElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

    let monochromaticOverlayPresenter () =
        createNode<MonochromaticOverlayPresenter> monochromaticOverlayPresenterNodeId
