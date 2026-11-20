namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Primitives
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module ColorPickerSliderMod =

    let private colorPickerSliderNodeId = getNextId ()

    registerDefaultRenderer<ColorPickerSlider> colorPickerSliderNodeId

    let private colorChannelPropertyId = getNextId ()

    [<Extension>]
    type ColorPickerSliderExtensions =

        [<Extension>]
        static member colorChannel(node: VirtualNode<ColorPickerSlider>, value: ColorPickerHsvChannel) =
            createDependencyProperty colorChannelPropertyId ColorPickerSlider.ColorChannelProperty value
            |> node.set

    let colorPickerSlider () =
        createNode<ColorPickerSlider> colorPickerSliderNodeId
