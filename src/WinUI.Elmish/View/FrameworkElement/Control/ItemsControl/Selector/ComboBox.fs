namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property IsSelectionBoxHighlighted: read-only
// skipped property SelectionBoxItem: read-only
// skipped property SelectionBoxItemTemplate: read-only
// skipped property TemplateSettings: read-only


[<AutoOpen>]
module ComboBoxMod =

    let private comboBoxNodeId = getNextId ()

    registerDefaultRenderer<ComboBox> comboBoxNodeId

    let private descriptionPropertyId = getNextId ()
    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private isDropDownOpenPropertyId = getNextId ()
    let private isEditablePropertyId = getNextId ()
    let private isTextSearchEnabledPropertyId = getNextId ()
    let private lightDismissOverlayModePropertyId = getNextId ()
    let private maxDropDownHeightPropertyId = getNextId ()
    let private placeholderForegroundPropertyId = getNextId ()
    let private placeholderTextPropertyId = getNextId ()
    let private selectionChangedTriggerPropertyId = getNextId ()
    let private textBoxStylePropertyId = getNextId ()
    let private textPropertyId = getNextId ()

    [<Extension>]
    type ComboBoxExtensions =

        [<Extension>]
        static member description(node: VirtualNode<ComboBox>, value: obj) =
            createDependencyProperty descriptionPropertyId ComboBox.DescriptionProperty value
            |> node.set

        [<Extension>]
        static member header(node: VirtualNode<ComboBox>, value: string) =
            createDependencyProperty str_headerPropertyId ComboBox.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'vnode when 'vnode :> IVirtualNode>
            (node: VirtualNode<ComboBox>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> ComboBox).Header <- child)
                (fun element -> (element :?> ComboBox).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<ComboBox>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId ComboBox.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member isDropDownOpen(node: VirtualNode<ComboBox>, value: bool) =
            createDependencyProperty isDropDownOpenPropertyId ComboBox.IsDropDownOpenProperty value
            |> node.set

        [<Extension>]
        static member isEditable(node: VirtualNode<ComboBox>, value: bool) =
            createDependencyProperty isEditablePropertyId ComboBox.IsEditableProperty value
            |> node.set

        [<Extension>]
        static member isTextSearchEnabled(node: VirtualNode<ComboBox>, value: bool) =
            createDependencyProperty isTextSearchEnabledPropertyId ComboBox.IsTextSearchEnabledProperty value
            |> node.set

        [<Extension>]
        static member lightDismissOverlayMode(node: VirtualNode<ComboBox>, value: LightDismissOverlayMode) =
            createDependencyProperty lightDismissOverlayModePropertyId ComboBox.LightDismissOverlayModeProperty value
            |> node.set

        [<Extension>]
        static member maxDropDownHeight(node: VirtualNode<ComboBox>, value: float) =
            createDependencyProperty maxDropDownHeightPropertyId ComboBox.MaxDropDownHeightProperty value
            |> node.set

        [<Extension>]
        static member placeholderForeground(node: VirtualNode<ComboBox>, value: Brush) =
            createDependencyProperty placeholderForegroundPropertyId ComboBox.PlaceholderForegroundProperty value
            |> node.set

        [<Extension>]
        static member placeholderText(node: VirtualNode<ComboBox>, value: string) =
            createDependencyProperty placeholderTextPropertyId ComboBox.PlaceholderTextProperty value
            |> node.set

        [<Extension>]
        static member selectionChangedTrigger(node: VirtualNode<ComboBox>, value: ComboBoxSelectionChangedTrigger) =
            createDependencyProperty selectionChangedTriggerPropertyId ComboBox.SelectionChangedTriggerProperty value
            |> node.set

        [<Extension>]
        static member textBoxStyle(node: VirtualNode<ComboBox>, value: Style) =
            createDependencyProperty textBoxStylePropertyId ComboBox.TextBoxStyleProperty value
            |> node.set

        [<Extension>]
        static member text(node: VirtualNode<ComboBox>, value: string) =
            createDependencyProperty textPropertyId ComboBox.TextProperty value
            |> node.set

        [<Extension>]
        static member onDropDownClosed(node: VirtualNode<ComboBox>, handler: ComboBox -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DropDownClosed"
                Subscribe = (fun uiElement ->
                    let comboBox = uiElement :?> ComboBox
                    let h = EventHandler<obj>(fun _ e -> handler comboBox e)
                    comboBox.DropDownClosed.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = comboBox.DropDownClosed.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onDropDownOpened(node: VirtualNode<ComboBox>, handler: ComboBox -> obj -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DropDownOpened"
                Subscribe = (fun uiElement ->
                    let comboBox = uiElement :?> ComboBox
                    let h = EventHandler<obj>(fun _ e -> handler comboBox e)
                    comboBox.DropDownOpened.AddHandler h
                    { new IDisposable with
                        member _.Dispose() = comboBox.DropDownOpened.RemoveHandler h }
                )
            })

        [<Extension>]
        static member onTextSubmitted(node: VirtualNode<ComboBox>, handler: ComboBox -> ComboBoxTextSubmittedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "TextSubmitted"
                Subscribe = (fun uiElement ->
                    let comboBox = uiElement :?> ComboBox
                    let h = TypedEventHandler<ComboBox, ComboBoxTextSubmittedEventArgs>(fun _ e -> handler comboBox e)
                    comboBox.add_TextSubmitted h
                    { new IDisposable with
                        member _.Dispose() = comboBox.remove_TextSubmitted h }
                )
            })

    let comboBox () =
        createNode<ComboBox> comboBoxNodeId
