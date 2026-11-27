namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open System.Windows.Input
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module PagerControlMod =

    let private pagerControlNodeId = getNextId ()

    registerDefaultRenderer<PagerControl> pagerControlNodeId

    let private buttonPanelAlwaysShowFirstLastPageIndexPropertyId = getNextId ()
    let private displayModePropertyId = getNextId ()
    let private firstButtonCommandPropertyId = getNextId ()
    let private firstButtonStylePropertyId = getNextId ()
    let private firstButtonVisibilityPropertyId = getNextId ()
    let private lastButtonCommandPropertyId = getNextId ()
    let private lastButtonStylePropertyId = getNextId ()
    let private lastButtonVisibilityPropertyId = getNextId ()
    let private nextButtonCommandPropertyId = getNextId ()
    let private nextButtonStylePropertyId = getNextId ()
    let private nextButtonVisibilityPropertyId = getNextId ()
    let private numberOfPagesPropertyId = getNextId ()
    let private pagerInputCommandPropertyId = getNextId ()
    let private prefixTextPropertyId = getNextId ()
    let private previousButtonCommandPropertyId = getNextId ()
    let private previousButtonStylePropertyId = getNextId ()
    let private previousButtonVisibilityPropertyId = getNextId ()
    let private selectedPageIndexPropertyId = getNextId ()
    let private suffixTextPropertyId = getNextId ()

    [<Extension>]
    type PagerControlExtensions =

        [<Extension>]
        static member buttonPanelAlwaysShowFirstLastPageIndex(node: VirtualNode<PagerControl>, value: bool) =
            createDependencyProperty buttonPanelAlwaysShowFirstLastPageIndexPropertyId PagerControl.ButtonPanelAlwaysShowFirstLastPageIndexProperty value
            |> node.set

        [<Extension>]
        static member displayMode(node: VirtualNode<PagerControl>, value: PagerControlDisplayMode) =
            createDependencyProperty displayModePropertyId PagerControl.DisplayModeProperty value
            |> node.set

        [<Extension>]
        static member firstButtonCommand(node: VirtualNode<PagerControl>, value: ICommand) =
            createDependencyProperty firstButtonCommandPropertyId PagerControl.FirstButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member firstButtonStyle(node: VirtualNode<PagerControl>, value: Style) =
            createDependencyProperty firstButtonStylePropertyId PagerControl.FirstButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member firstButtonVisibility(node: VirtualNode<PagerControl>, value: PagerControlButtonVisibility) =
            createDependencyProperty firstButtonVisibilityPropertyId PagerControl.FirstButtonVisibilityProperty value
            |> node.set

        [<Extension>]
        static member lastButtonCommand(node: VirtualNode<PagerControl>, value: ICommand) =
            createDependencyProperty lastButtonCommandPropertyId PagerControl.LastButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member lastButtonStyle(node: VirtualNode<PagerControl>, value: Style) =
            createDependencyProperty lastButtonStylePropertyId PagerControl.LastButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member lastButtonVisibility(node: VirtualNode<PagerControl>, value: PagerControlButtonVisibility) =
            createDependencyProperty lastButtonVisibilityPropertyId PagerControl.LastButtonVisibilityProperty value
            |> node.set

        [<Extension>]
        static member nextButtonCommand(node: VirtualNode<PagerControl>, value: ICommand) =
            createDependencyProperty nextButtonCommandPropertyId PagerControl.NextButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member nextButtonStyle(node: VirtualNode<PagerControl>, value: Style) =
            createDependencyProperty nextButtonStylePropertyId PagerControl.NextButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member nextButtonVisibility(node: VirtualNode<PagerControl>, value: PagerControlButtonVisibility) =
            createDependencyProperty nextButtonVisibilityPropertyId PagerControl.NextButtonVisibilityProperty value
            |> node.set

        [<Extension>]
        static member numberOfPages(node: VirtualNode<PagerControl>, value: int) =
            createDependencyProperty numberOfPagesPropertyId PagerControl.NumberOfPagesProperty value
            |> node.set

        [<Extension>]
        static member pagerInputCommand(node: VirtualNode<PagerControl>, value: ICommand) =
            createDependencyProperty pagerInputCommandPropertyId PagerControl.PagerInputCommandProperty value
            |> node.set

        [<Extension>]
        static member prefixText(node: VirtualNode<PagerControl>, value: string) =
            createDependencyProperty prefixTextPropertyId PagerControl.PrefixTextProperty value
            |> node.set

        [<Extension>]
        static member previousButtonCommand(node: VirtualNode<PagerControl>, value: ICommand) =
            createDependencyProperty previousButtonCommandPropertyId PagerControl.PreviousButtonCommandProperty value
            |> node.set

        [<Extension>]
        static member previousButtonStyle(node: VirtualNode<PagerControl>, value: Style) =
            createDependencyProperty previousButtonStylePropertyId PagerControl.PreviousButtonStyleProperty value
            |> node.set

        [<Extension>]
        static member previousButtonVisibility(node: VirtualNode<PagerControl>, value: PagerControlButtonVisibility) =
            createDependencyProperty previousButtonVisibilityPropertyId PagerControl.PreviousButtonVisibilityProperty value
            |> node.set

        [<Extension>]
        static member selectedPageIndex(node: VirtualNode<PagerControl>, value: int) =
            createDependencyProperty selectedPageIndexPropertyId PagerControl.SelectedPageIndexProperty value
            |> node.set

        [<Extension>]
        static member suffixText(node: VirtualNode<PagerControl>, value: string) =
            createDependencyProperty suffixTextPropertyId PagerControl.SuffixTextProperty value
            |> node.set

        [<Extension>]
        static member onSelectedIndexChanged(node: VirtualNode<PagerControl>, handler: PagerControl -> PagerControlSelectedIndexChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SelectedIndexChanged"
                Subscribe = (fun uiElement ->
                    let pagerControl = uiElement :?> PagerControl
                    let h = TypedEventHandler<PagerControl, PagerControlSelectedIndexChangedEventArgs>(fun _ e -> handler pagerControl e)
                    pagerControl.add_SelectedIndexChanged h
                    { new IDisposable with
                        member _.Dispose() = pagerControl.remove_SelectedIndexChanged h }
                )
            })

    let pagerControl () =
        createNode<PagerControl> pagerControlNodeId
