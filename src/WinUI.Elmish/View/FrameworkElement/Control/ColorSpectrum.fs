namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Numerics
open System.Runtime.CompilerServices
open Windows.Foundation
open Windows.UI

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module ColorSpectrumMod =

    let private colorSpectrumNodeId = getNextId ()

    registerDefaultRenderer<ColorSpectrum> colorSpectrumNodeId

    let private colorPropertyId = getNextId ()
    let private componentsPropertyId = getNextId ()
    let private hsvColorPropertyId = getNextId ()
    let private maxHuePropertyId = getNextId ()
    let private maxSaturationPropertyId = getNextId ()
    let private maxValuePropertyId = getNextId ()
    let private minHuePropertyId = getNextId ()
    let private minSaturationPropertyId = getNextId ()
    let private minValuePropertyId = getNextId ()
    let private shapePropertyId = getNextId ()

    [<Extension>]
    type ColorSpectrumExtensions =

        [<Extension>]
        static member color(node: VirtualNode<ColorSpectrum>, value: Color) =
            createDependencyProperty colorPropertyId ColorSpectrum.ColorProperty value
            |> node.set

        [<Extension>]
        static member components(node: VirtualNode<ColorSpectrum>, value: ColorSpectrumComponents) =
            createDependencyProperty componentsPropertyId ColorSpectrum.ComponentsProperty value
            |> node.set

        [<Extension>]
        static member hsvColor(node: VirtualNode<ColorSpectrum>, value: Vector4) =
            createDependencyProperty hsvColorPropertyId ColorSpectrum.HsvColorProperty value
            |> node.set

        [<Extension>]
        static member maxHue(node: VirtualNode<ColorSpectrum>, value: int) =
            createDependencyProperty maxHuePropertyId ColorSpectrum.MaxHueProperty value
            |> node.set

        [<Extension>]
        static member maxSaturation(node: VirtualNode<ColorSpectrum>, value: int) =
            createDependencyProperty maxSaturationPropertyId ColorSpectrum.MaxSaturationProperty value
            |> node.set

        [<Extension>]
        static member maxValue(node: VirtualNode<ColorSpectrum>, value: int) =
            createDependencyProperty maxValuePropertyId ColorSpectrum.MaxValueProperty value
            |> node.set

        [<Extension>]
        static member minHue(node: VirtualNode<ColorSpectrum>, value: int) =
            createDependencyProperty minHuePropertyId ColorSpectrum.MinHueProperty value
            |> node.set

        [<Extension>]
        static member minSaturation(node: VirtualNode<ColorSpectrum>, value: int) =
            createDependencyProperty minSaturationPropertyId ColorSpectrum.MinSaturationProperty value
            |> node.set

        [<Extension>]
        static member minValue(node: VirtualNode<ColorSpectrum>, value: int) =
            createDependencyProperty minValuePropertyId ColorSpectrum.MinValueProperty value
            |> node.set

        [<Extension>]
        static member shape(node: VirtualNode<ColorSpectrum>, value: ColorSpectrumShape) =
            createDependencyProperty shapePropertyId ColorSpectrum.ShapeProperty value
            |> node.set

        [<Extension>]
        static member onColorChanged(node: VirtualNode<ColorSpectrum>, handler: ColorChangedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ColorChanged"
                Subscribe = (fun uiElement ->
                    let colorSpectrum = uiElement :?> ColorSpectrum
                    let h = TypedEventHandler<ColorSpectrum, ColorChangedEventArgs>(fun _ e -> handler e)
                    colorSpectrum.add_ColorChanged h
                    { new IDisposable with
                        member _.Dispose() = colorSpectrum.remove_ColorChanged h }
                )
            })

    let colorSpectrum () =
        createNode<ColorSpectrum> colorSpectrumNodeId
