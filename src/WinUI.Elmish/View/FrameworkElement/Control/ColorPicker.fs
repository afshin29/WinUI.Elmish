namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices
open Windows.Foundation
open Windows.UI

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module ColorPickerMod =

    let private colorPickerNodeId = getNextId ()

    registerDefaultRenderer<ColorPicker> colorPickerNodeId

    let private colorPropertyId = getNextId ()
    let private colorSpectrumComponentsPropertyId = getNextId ()
    let private colorSpectrumShapePropertyId = getNextId ()
    let private isAlphaEnabledPropertyId = getNextId ()
    let private isAlphaSliderVisiblePropertyId = getNextId ()
    let private isAlphaTextInputVisiblePropertyId = getNextId ()
    let private isColorChannelTextInputVisiblePropertyId = getNextId ()
    let private isColorPreviewVisiblePropertyId = getNextId ()
    let private isColorSliderVisiblePropertyId = getNextId ()
    let private isColorSpectrumVisiblePropertyId = getNextId ()
    let private isHexInputVisiblePropertyId = getNextId ()
    let private isMoreButtonVisiblePropertyId = getNextId ()
    let private maxHuePropertyId = getNextId ()
    let private maxSaturationPropertyId = getNextId ()
    let private maxValuePropertyId = getNextId ()
    let private minHuePropertyId = getNextId ()
    let private minSaturationPropertyId = getNextId ()
    let private minValuePropertyId = getNextId ()
    let private orientationPropertyId = getNextId ()
    let private previousColorPropertyId = getNextId ()

    [<Extension>]
    type ColorPickerExtensions =

        [<Extension>]
        static member color(node: VirtualNode<ColorPicker>, value: Color) =
            createDependencyProperty colorPropertyId ColorPicker.ColorProperty value
            |> node.set

        [<Extension>]
        static member colorSpectrumComponents(node: VirtualNode<ColorPicker>, value: ColorSpectrumComponents) =
            createDependencyProperty colorSpectrumComponentsPropertyId ColorPicker.ColorSpectrumComponentsProperty value
            |> node.set

        [<Extension>]
        static member colorSpectrumShape(node: VirtualNode<ColorPicker>, value: ColorSpectrumShape) =
            createDependencyProperty colorSpectrumShapePropertyId ColorPicker.ColorSpectrumShapeProperty value
            |> node.set

        [<Extension>]
        static member isAlphaEnabled(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isAlphaEnabledPropertyId ColorPicker.IsAlphaEnabledProperty value
            |> node.set

        [<Extension>]
        static member isAlphaSliderVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isAlphaSliderVisiblePropertyId ColorPicker.IsAlphaSliderVisibleProperty value
            |> node.set

        [<Extension>]
        static member isAlphaTextInputVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isAlphaTextInputVisiblePropertyId ColorPicker.IsAlphaTextInputVisibleProperty value
            |> node.set

        [<Extension>]
        static member isColorChannelTextInputVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isColorChannelTextInputVisiblePropertyId ColorPicker.IsColorChannelTextInputVisibleProperty value
            |> node.set

        [<Extension>]
        static member isColorPreviewVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isColorPreviewVisiblePropertyId ColorPicker.IsColorPreviewVisibleProperty value
            |> node.set

        [<Extension>]
        static member isColorSliderVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isColorSliderVisiblePropertyId ColorPicker.IsColorSliderVisibleProperty value
            |> node.set

        [<Extension>]
        static member isColorSpectrumVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isColorSpectrumVisiblePropertyId ColorPicker.IsColorSpectrumVisibleProperty value
            |> node.set

        [<Extension>]
        static member isHexInputVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isHexInputVisiblePropertyId ColorPicker.IsHexInputVisibleProperty value
            |> node.set

        [<Extension>]
        static member isMoreButtonVisible(node: VirtualNode<ColorPicker>, value: bool) =
            createDependencyProperty isMoreButtonVisiblePropertyId ColorPicker.IsMoreButtonVisibleProperty value
            |> node.set

        [<Extension>]
        static member maxHue(node: VirtualNode<ColorPicker>, value: int) =
            createDependencyProperty maxHuePropertyId ColorPicker.MaxHueProperty value
            |> node.set

        [<Extension>]
        static member maxSaturation(node: VirtualNode<ColorPicker>, value: int) =
            createDependencyProperty maxSaturationPropertyId ColorPicker.MaxSaturationProperty value
            |> node.set

        [<Extension>]
        static member maxValue(node: VirtualNode<ColorPicker>, value: int) =
            createDependencyProperty maxValuePropertyId ColorPicker.MaxValueProperty value
            |> node.set

        [<Extension>]
        static member minHue(node: VirtualNode<ColorPicker>, value: int) =
            createDependencyProperty minHuePropertyId ColorPicker.MinHueProperty value
            |> node.set

        [<Extension>]
        static member minSaturation(node: VirtualNode<ColorPicker>, value: int) =
            createDependencyProperty minSaturationPropertyId ColorPicker.MinSaturationProperty value
            |> node.set

        [<Extension>]
        static member minValue(node: VirtualNode<ColorPicker>, value: int) =
            createDependencyProperty minValuePropertyId ColorPicker.MinValueProperty value
            |> node.set

        [<Extension>]
        static member orientation(node: VirtualNode<ColorPicker>, value: Orientation) =
            createDependencyProperty orientationPropertyId ColorPicker.OrientationProperty value
            |> node.set

        [<Extension>]
        static member previousColor(node: VirtualNode<ColorPicker>, value: Nullable<Color>) =
            createDependencyProperty previousColorPropertyId ColorPicker.PreviousColorProperty value
            |> node.set

        [<Extension>]
        static member onColorChanged(node: VirtualNode<ColorPicker>, handler: ColorChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ColorChanged"
                Subscribe = (fun uiElement ->
                    let colorPicker = uiElement :?> ColorPicker
                    let h = TypedEventHandler<ColorPicker, ColorChangedEventArgs>(fun _ e -> handler e)
                    colorPicker.add_ColorChanged h
                    { new IDisposable with
                        member _.Dispose() = colorPicker.remove_ColorChanged h }
                )
            })

    let colorPicker () =
        createNode<ColorPicker> colorPickerNodeId
