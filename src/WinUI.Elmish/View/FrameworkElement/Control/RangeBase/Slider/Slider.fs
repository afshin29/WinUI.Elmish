namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open Microsoft.UI.Xaml.Data
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module SliderMod =

    let private sliderNodeId = getNextId ()

    registerDefaultRenderer<Slider> sliderNodeId

    let private str_headerPropertyId = getNextId ()
    let private vnode_headerPropertyId = getNextId ()
    let private headerTemplatePropertyId = getNextId ()
    let private intermediateValuePropertyId = getNextId ()
    let private isDirectionReversedPropertyId = getNextId ()
    let private isThumbToolTipEnabledPropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private snapsToPropertyId = getNextId ()
    let private stepFrequencyPropertyId = getNextId ()
    let private thumbToolTipValueConverterPropertyId = getNextId ()
    let private tickFrequencyPropertyId = getNextId ()
    let private tickPlacementPropertyId = getNextId ()

    [<Extension>]
    type SliderExtensions =

        [<Extension>]
        static member header<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: string) =
            createDependencyProperty str_headerPropertyId Slider.HeaderProperty value
            |> node.set

        [<Extension>]
        static member header<'m, 'vnode when 'm :> Slider and 'm: not struct and 'vnode :> IVirtualNode>
            (node: VirtualNode<'m>, value: 'vnode, ?contentReplaceKey: obj) =
            createInnerChildProperty
                vnode_headerPropertyId
                value
                (fun element child -> (element :?> Slider).Header <- child)
                (fun element -> (element :?> Slider).Header :?> UIElement)
                (contentReplaceKey |> Option.defaultValue 1)
            |> node.set

        [<Extension>]
        static member headerTemplate<'m, 'source, 'el when 'm :> Slider and 'm: not struct and 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
            (node: VirtualNode<'m>, template: ('source -> VirtualNode<'el>), ?diffKey: obj) =
            getDataTemplate template
            |> createDependencyProperty headerTemplatePropertyId Slider.HeaderTemplateProperty
            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))
            |> node.set

        [<Extension>]
        static member intermediateValue<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty intermediateValuePropertyId Slider.IntermediateValueProperty value
            |> node.set

        [<Extension>]
        static member isDirectionReversed<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isDirectionReversedPropertyId Slider.IsDirectionReversedProperty value
            |> node.set

        [<Extension>]
        static member isThumbToolTipEnabled<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: bool) =
            createDependencyProperty isThumbToolTipEnabledPropertyId Slider.IsThumbToolTipEnabledProperty value
            |> node.set

        [<Extension>]
        static member orientation<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: Orientation) =
            createDependencyProperty orientationPropertyId Slider.OrientationProperty value
            |> node.set

        [<Extension>]
        static member snapsTo<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: SliderSnapsTo) =
            createDependencyProperty snapsToPropertyId Slider.SnapsToProperty value
            |> node.set

        [<Extension>]
        static member stepFrequency<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty stepFrequencyPropertyId Slider.StepFrequencyProperty value
            |> node.set

        [<Extension>]
        static member thumbToolTipValueConverter<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: IValueConverter) =
            createDependencyProperty thumbToolTipValueConverterPropertyId Slider.ThumbToolTipValueConverterProperty value
            |> node.set

        [<Extension>]
        static member tickFrequency<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: float) =
            createDependencyProperty tickFrequencyPropertyId Slider.TickFrequencyProperty value
            |> node.set

        [<Extension>]
        static member tickPlacement<'m when 'm :> Slider and 'm: not struct>
            (node: VirtualNode<'m>, value: TickPlacement) =
            createDependencyProperty tickPlacementPropertyId Slider.TickPlacementProperty value
            |> node.set

    let slider () =
        createNode<Slider> sliderNodeId
