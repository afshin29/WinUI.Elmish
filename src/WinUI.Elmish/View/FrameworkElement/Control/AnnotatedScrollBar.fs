namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property ScrollController: read-only


[<AutoOpen>]
module AnnotatedScrollBarMod =

    let private annotatedScrollBarNodeId = getNextId ()

    registerDefaultRenderer<AnnotatedScrollBar> annotatedScrollBarNodeId

    let private detailLabelTemplatePropertyId = getNextId ()
    let private labelTemplatePropertyId = getNextId ()
    let private labelsPropertyId = getNextId ()
    let private smallChangePropertyId = getNextId ()

    [<Extension>]
    type AnnotatedScrollBarExtensions =

        [<Extension>]
        static member detailLabelTemplate(node: VirtualNode<AnnotatedScrollBar>, value: IElementFactory) =
            createDependencyProperty detailLabelTemplatePropertyId AnnotatedScrollBar.DetailLabelTemplateProperty value
            |> node.set

        [<Extension>]
        static member labelTemplate(node: VirtualNode<AnnotatedScrollBar>, value: IElementFactory) =
            createDependencyProperty labelTemplatePropertyId AnnotatedScrollBar.LabelTemplateProperty value
            |> node.set

        [<Extension>]
        static member labels(node: VirtualNode<AnnotatedScrollBar>, value: IList<AnnotatedScrollBarLabel>) =
            createDependencyProperty labelsPropertyId AnnotatedScrollBar.LabelsProperty value
            |> node.set

        [<Extension>]
        static member smallChange(node: VirtualNode<AnnotatedScrollBar>, value: float) =
            createDependencyProperty smallChangePropertyId AnnotatedScrollBar.SmallChangeProperty value
            |> node.set

        [<Extension>]
        static member onDetailLabelRequested(node: VirtualNode<AnnotatedScrollBar>, handler: AnnotatedScrollBarDetailLabelRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "DetailLabelRequested"
                Subscribe = (fun uiElement ->
                    let annotatedScrollBar = uiElement :?> AnnotatedScrollBar
                    let h = TypedEventHandler<AnnotatedScrollBar, AnnotatedScrollBarDetailLabelRequestedEventArgs>(fun _ e -> handler e)
                    annotatedScrollBar.add_DetailLabelRequested h
                    { new IDisposable with
                        member _.Dispose() = annotatedScrollBar.remove_DetailLabelRequested h }
                )
            })

        [<Extension>]
        static member onScrolling(node: VirtualNode<AnnotatedScrollBar>, handler: AnnotatedScrollBarScrollingEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "Scrolling"
                Subscribe = (fun uiElement ->
                    let annotatedScrollBar = uiElement :?> AnnotatedScrollBar
                    let h = TypedEventHandler<AnnotatedScrollBar, AnnotatedScrollBarScrollingEventArgs>(fun _ e -> handler e)
                    annotatedScrollBar.add_Scrolling h
                    { new IDisposable with
                        member _.Dispose() = annotatedScrollBar.remove_Scrolling h }
                )
            })

    let annotatedScrollBar () =
        createNode<AnnotatedScrollBar> annotatedScrollBarNodeId
