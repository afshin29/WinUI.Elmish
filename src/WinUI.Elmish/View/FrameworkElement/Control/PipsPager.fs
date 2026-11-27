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

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module PipsPagerMod =

    let private pipsPagerNodeId = getNextId ()

    registerDefaultRenderer<PipsPager> pipsPagerNodeId

    let private maxVisiblePipsPropertyId = getNextId ()
    let private nextButtonStylePropertyId = getNextId ()
    let private nextButtonVisibilityPropertyId = getNextId ()
    let private normalPipStylePropertyId = getNextId ()
    let private numberOfPagesPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private previousButtonStylePropertyId = getNextId ()
    let private previousButtonVisibilityPropertyId = getNextId ()
    let private selectedPageIndexPropertyId = getNextId ()
    let private selectedPipStylePropertyId = getNextId ()
    let private wrapModePropertyId = getNextId ()

    [<Extension>]
    type PipsPagerExtensions =

        [<Extension>]
        static member maxVisiblePips(node: VirtualNode<PipsPager>, value: int) =
            createDependencyProperty maxVisiblePipsPropertyId PipsPager.MaxVisiblePipsProperty value
            |> node.set

        [<Extension>]
        static member nextButtonStyle(node: VirtualNode<PipsPager>, value: Style) =
            createDependencyProperty nextButtonStylePropertyId PipsPager.NextButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member nextButtonVisibility(node: VirtualNode<PipsPager>, value: PipsPagerButtonVisibility) =
            createDependencyProperty nextButtonVisibilityPropertyId PipsPager.NextButtonVisibilityProperty value
            |> node.set

        [<Extension>]
        static member normalPipStyle(node: VirtualNode<PipsPager>, value: Style) =
            createDependencyProperty normalPipStylePropertyId PipsPager.NormalPipStyleProperty value
            |> node.set

        [<Extension>]
        static member numberOfPages(node: VirtualNode<PipsPager>, value: int) =
            createDependencyProperty numberOfPagesPropertyId PipsPager.NumberOfPagesProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<PipsPager>, value: Orientation) =
            createDependencyProperty orientationPropertyId PipsPager.OrientationProperty value
            |> node.set

        [<Extension>]
        static member previousButtonStyle(node: VirtualNode<PipsPager>, value: Style) =
            createDependencyProperty previousButtonStylePropertyId PipsPager.PreviousButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member previousButtonVisibility(node: VirtualNode<PipsPager>, value: PipsPagerButtonVisibility) =
            createDependencyProperty previousButtonVisibilityPropertyId PipsPager.PreviousButtonVisibilityProperty value
            |> node.set

        [<Extension>]
        static member selectedPageIndex(node: VirtualNode<PipsPager>, value: int) =
            createDependencyProperty selectedPageIndexPropertyId PipsPager.SelectedPageIndexProperty value
            |> node.set

        [<Extension>]
        static member selectedPipStyle(node: VirtualNode<PipsPager>, value: Style) =
            createDependencyProperty selectedPipStylePropertyId PipsPager.SelectedPipStyleProperty value
            |> node.set

        [<Extension>]
        static member wrapMode(node: VirtualNode<PipsPager>, value: PipsPagerWrapMode) =
            createDependencyProperty wrapModePropertyId PipsPager.WrapModeProperty value
            |> node.set

        [<Extension>]
        static member onSelectedIndexChanged(node: VirtualNode<PipsPager>, handler: PipsPager -> PipsPagerSelectedIndexChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectedIndexChanged"
                Subscribe = (fun uiElement ->
                    let pipsPager = uiElement :?> PipsPager
                    let h = TypedEventHandler<PipsPager, PipsPagerSelectedIndexChangedEventArgs>(fun _ e -> handler pipsPager e)
                    pipsPager.add_SelectedIndexChanged h
                    { new IDisposable with
                        member _.Dispose() = pipsPager.remove_SelectedIndexChanged h }
                )
            })

    let pipsPager () =
        createNode<PipsPager> pipsPagerNodeId
