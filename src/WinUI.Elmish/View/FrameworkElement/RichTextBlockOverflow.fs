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

// skipped dependency property HasOverflowContentProperty: read-only
// skipped dependency property IsTextTrimmedProperty: read-only
// skipped property BaselineOffset: read-only
// skipped property ContentEnd: read-only
// skipped property ContentSource: read-only
// skipped property ContentStart: read-only


[<AutoOpen>]
module RichTextBlockOverflowMod =

    let private richTextBlockOverflowNodeId = getNextId ()

    registerDefaultRenderer<RichTextBlockOverflow> richTextBlockOverflowNodeId

    let private maxLinesPropertyId = getNextId ()
    let private overflowContentTargetPropertyId = getNextId ()
    let private paddingPropertyId = getNextId ()

    [<Extension>]
    type RichTextBlockOverflowExtensions =

        [<Extension>]
        static member maxLines(node: VirtualNode<RichTextBlockOverflow>, value: int) =
            createDependencyProperty maxLinesPropertyId RichTextBlockOverflow.MaxLinesProperty value
            |> node.set

        [<Extension>]
        static member overflowContentTarget(node: VirtualNode<RichTextBlockOverflow>, value: IVirtualNode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                overflowContentTargetPropertyId
                value
                (fun element child -> (element :?> RichTextBlockOverflow).OverflowContentTarget <- (child :?> RichTextBlockOverflow))
                (fun element -> (element :?> RichTextBlockOverflow).OverflowContentTarget :> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member padding(node: VirtualNode<RichTextBlockOverflow>, value: Thickness) =
            createDependencyProperty paddingPropertyId RichTextBlockOverflow.PaddingProperty value
            |> node.set

        [<Extension>]
        static member onIsTextTrimmedChanged(node: VirtualNode<RichTextBlockOverflow>, handler: IsTextTrimmedChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "IsTextTrimmedChanged"
                Subscribe = (fun uiElement ->
                    let richTextBlockOverflow = uiElement :?> RichTextBlockOverflow
                    let h = TypedEventHandler<RichTextBlockOverflow, IsTextTrimmedChangedEventArgs>(fun _ e -> handler e)
                    richTextBlockOverflow.add_IsTextTrimmedChanged h
                    { new IDisposable with
                        member _.Dispose() = richTextBlockOverflow.remove_IsTextTrimmedChanged h }
                )
            })

    let richTextBlockOverflow () =
        createNode<RichTextBlockOverflow> richTextBlockOverflowNodeId
