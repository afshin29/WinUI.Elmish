namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property TemplateSettingsProperty: read-only


[<AutoOpen>]
module InfoBadgeMod =

    let private infoBadgeNodeId = getNextId ()

    registerDefaultRenderer<InfoBadge> infoBadgeNodeId

    let private iconSourcePropertyId = getNextId ()
    let private valuePropertyId = getNextId ()

    [<Extension>]
    type InfoBadgeExtensions =

        [<Extension>]
        static member iconSource(node: VirtualNode<InfoBadge>, value: IconSource) =
            createDependencyProperty iconSourcePropertyId InfoBadge.IconSourceProperty value
            |> node.set

        [<Extension>]
        static member value(node: VirtualNode<InfoBadge>, value: int) =
            createDependencyProperty valuePropertyId InfoBadge.ValueProperty value
            |> node.set

    let infoBadge () =
        createNode<InfoBadge> infoBadgeNodeId
