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


[<AutoOpen>]
module AutoSuggestBoxMod =

    let private autoSuggestBoxNodeId = getNextId ()

    registerDefaultRenderer<AutoSuggestBox> autoSuggestBoxNodeId

    let private autoMaximizeSuggestionAreaPropertyId = getNextId ()
    let private descriptionPropertyId = getNextId ()
    let private headerPropertyId = getNextId ()
    let private isSuggestionListOpenPropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()
    let private maxSuggestionListHeightPropertyId = getNextId ()
    let private placeholderTextPropertyId = getNextId ()
    let private queryIconPropertyId = getNextId ()
    let private textBoxStylePropertyId = getNextId ()
    let private textMemberPathPropertyId = getNextId ()
    let private textPropertyId = getNextId ()
    let private updateTextOnSelectPropertyId = getNextId ()

    [<Extension>]
    type AutoSuggestBoxExtensions =

        [<Extension>]
        static member autoMaximizeSuggestionArea(node: VirtualNode<AutoSuggestBox>, value: bool) =
            createDependencyProperty autoMaximizeSuggestionAreaPropertyId AutoSuggestBox.AutoMaximizeSuggestionAreaProperty value
            |> node.set

        [<Extension>]
        static member description(node: VirtualNode<AutoSuggestBox>, value: obj) =
            createDependencyProperty descriptionPropertyId AutoSuggestBox.DescriptionProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<AutoSuggestBox>, value: obj) =
            createDependencyProperty headerPropertyId AutoSuggestBox.HeaderProperty value
            |> node.set

        [<Extension>]
        static member isSuggestionListOpen(node: VirtualNode<AutoSuggestBox>, value: bool) =
            createDependencyProperty isSuggestionListOpenPropertyId AutoSuggestBox.IsSuggestionListOpenProperty value
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode(node: VirtualNode<AutoSuggestBox>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId AutoSuggestBox.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member maxSuggestionListHeight(node: VirtualNode<AutoSuggestBox>, value: float) =
            createDependencyProperty maxSuggestionListHeightPropertyId AutoSuggestBox.MaxSuggestionListHeightProperty value
            |> node.set

        [<Extension>]
        static member placeholderText(node: VirtualNode<AutoSuggestBox>, value: string) =
            createDependencyProperty placeholderTextPropertyId AutoSuggestBox.PlaceholderTextProperty value
            |> node.set

        [<Extension>]
        static member queryIcon(node: VirtualNode<AutoSuggestBox>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                queryIconPropertyId
                value
                (fun element child -> (element :?> AutoSuggestBox).QueryIcon <- (child :?> IconElement))
                (fun element -> (element :?> AutoSuggestBox).QueryIcon :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member textBoxStyle(node: VirtualNode<AutoSuggestBox>, value: Style) =
            createDependencyProperty textBoxStylePropertyId AutoSuggestBox.TextBoxStyleProperty value
            |> node.set

        [<Extension>]
        static member textMemberPath(node: VirtualNode<AutoSuggestBox>, value: string) =
            createDependencyProperty textMemberPathPropertyId AutoSuggestBox.TextMemberPathProperty value
            |> node.set

        [<Extension>]
        static member text(node: VirtualNode<AutoSuggestBox>, value: string) =
            createDependencyProperty textPropertyId AutoSuggestBox.TextProperty value
            |> node.set

        [<Extension>]
        static member updateTextOnSelect(node: VirtualNode<AutoSuggestBox>, value: bool) =
            createDependencyProperty updateTextOnSelectPropertyId AutoSuggestBox.UpdateTextOnSelectProperty value
            |> node.set

        [<Extension>]
        static member onQuerySubmitted(node: VirtualNode<AutoSuggestBox>, handler: AutoSuggestBoxQuerySubmittedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "QuerySubmitted"
                Subscribe = (fun uiElement ->
                    let autoSuggestBox = uiElement :?> AutoSuggestBox
                    let h = TypedEventHandler<AutoSuggestBox, AutoSuggestBoxQuerySubmittedEventArgs>(fun _ e -> handler e)
                    autoSuggestBox.add_QuerySubmitted h
                    { new IDisposable with
                        member _.Dispose() = autoSuggestBox.remove_QuerySubmitted h }
                )
            })

        [<Extension>]
        static member onSuggestionChosen(node: VirtualNode<AutoSuggestBox>, handler: AutoSuggestBoxSuggestionChosenEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "SuggestionChosen"
                Subscribe = (fun uiElement ->
                    let autoSuggestBox = uiElement :?> AutoSuggestBox
                    let h = TypedEventHandler<AutoSuggestBox, AutoSuggestBoxSuggestionChosenEventArgs>(fun _ e -> handler e)
                    autoSuggestBox.add_SuggestionChosen h
                    { new IDisposable with
                        member _.Dispose() = autoSuggestBox.remove_SuggestionChosen h }
                )
            })

        [<Extension>]
        static member onTextChanged(node: VirtualNode<AutoSuggestBox>, handler: AutoSuggestBoxTextChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextChanged"
                Subscribe = (fun uiElement ->
                    let autoSuggestBox = uiElement :?> AutoSuggestBox
                    let h = TypedEventHandler<AutoSuggestBox, AutoSuggestBoxTextChangedEventArgs>(fun _ e -> handler e)
                    autoSuggestBox.add_TextChanged h
                    { new IDisposable with
                        member _.Dispose() = autoSuggestBox.remove_TextChanged h }
                )
            })

    let autoSuggestBox () =
        createNode<AutoSuggestBox> autoSuggestBoxNodeId
