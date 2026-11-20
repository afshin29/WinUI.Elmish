namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open Windows.Devices.Geolocation
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module MapControlMod =

    let private mapControlNodeId = getNextId ()

    registerDefaultRenderer<MapControl> mapControlNodeId

    let private centerPropertyId = getNextId ()
    let private interactiveControlsVisiblePropertyId = getNextId ()
    let private layersPropertyId = getNextId ()
    let private mapServiceTokenPropertyId = getNextId ()
    let private zoomLevelPropertyId = getNextId ()

    [<Extension>]
    type MapControlExtensions =

        [<Extension>]
        static member center(node: VirtualNode<MapControl>, value: Geopoint) =
            createDependencyProperty centerPropertyId MapControl.CenterProperty value
            |> node.set

        [<Extension>]
        static member interactiveControlsVisible(node: VirtualNode<MapControl>, value: bool) =
            createDependencyProperty interactiveControlsVisiblePropertyId MapControl.InteractiveControlsVisibleProperty value
            |> node.set

        [<Extension>]
        static member layers(node: VirtualNode<MapControl>, value: IList<MapLayer>) =
            createDependencyProperty layersPropertyId MapControl.LayersProperty value
            |> node.set

        [<Extension>]
        static member mapServiceToken(node: VirtualNode<MapControl>, value: string) =
            createDependencyProperty mapServiceTokenPropertyId MapControl.MapServiceTokenProperty value
            |> node.set

        [<Extension>]
        static member zoomLevel(node: VirtualNode<MapControl>, value: float) =
            createDependencyProperty zoomLevelPropertyId MapControl.ZoomLevelProperty value
            |> node.set

        [<Extension>]
        static member onMapElementClick(node: VirtualNode<MapControl>, handler: MapElementClickEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "MapElementClick"
                Subscribe = (fun uiElement ->
                    let mapControl = uiElement :?> MapControl
                    let h = TypedEventHandler<MapControl, MapElementClickEventArgs>(fun _ e -> handler e)
                    mapControl.add_MapElementClick h
                    { new IDisposable with
                        member _.Dispose() = mapControl.remove_MapElementClick h }
                )
            })

        [<Extension>]
        static member onMapServiceErrorOccurred(node: VirtualNode<MapControl>, handler: MapControlMapServiceErrorOccurredEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "MapServiceErrorOccurred"
                Subscribe = (fun uiElement ->
                    let mapControl = uiElement :?> MapControl
                    let h = TypedEventHandler<MapControl, MapControlMapServiceErrorOccurredEventArgs>(fun _ e -> handler e)
                    mapControl.add_MapServiceErrorOccurred h
                    { new IDisposable with
                        member _.Dispose() = mapControl.remove_MapServiceErrorOccurred h }
                )
            })

    let mapControl () =
        createNode<MapControl> mapControlNodeId
