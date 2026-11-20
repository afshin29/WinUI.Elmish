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

// skipped dependency property DateProperty: read-only


[<AutoOpen>]
module CalendarViewDayItemMod =

    let private calendarViewDayItemNodeId = getNextId ()

    registerDefaultRenderer<CalendarViewDayItem> calendarViewDayItemNodeId

    let private isBlackoutPropertyId = getNextId ()

    [<Extension>]
    type CalendarViewDayItemExtensions =

        [<Extension>]
        static member isBlackout(node: VirtualNode<CalendarViewDayItem>, value: bool) =
            createDependencyProperty isBlackoutPropertyId CalendarViewDayItem.IsBlackoutProperty value
            |> node.set

    let calendarViewDayItem () =
        createNode<CalendarViewDayItem> calendarViewDayItemNodeId
