namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Controls.Tabular
open System
open System.Runtime.CompilerServices
open Windows.Foundation

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler


[<AutoOpen>]
module TableViewGroupHeaderMod =

    let private tableViewGroupHeaderNodeId = getNextId ()

    registerDefaultRenderer<TableViewGroupHeader> tableViewGroupHeaderNodeId

    let private isExpandablePropertyId = getNextId ()
    let private isExpandedPropertyId = getNextId ()

    [<Extension>]
    type TableViewGroupHeaderExtensions =

        [<Extension>]
        static member isExpandable(node: VirtualNode<TableViewGroupHeader>, value: bool) =
            createDependencyProperty isExpandablePropertyId TableViewGroupHeader.IsExpandableProperty value
            |> node.set

        [<Extension>]
        static member isExpanded(node: VirtualNode<TableViewGroupHeader>, value: bool) =
            createDependencyProperty isExpandedPropertyId TableViewGroupHeader.IsExpandedProperty value
            |> node.set

        [<Extension>]
        static member onToggleRequested(node: VirtualNode<TableViewGroupHeader>, handler: TableViewGroupHeader -> TableViewGroupHeaderToggleRequestedEventArgs -> unit, ?key: string) =
            node.handle({
                Key = key |> Option.defaultValue "ToggleRequested"
                Subscribe = (fun uiElement ->
                    let tableViewGroupHeader = uiElement :?> TableViewGroupHeader
                    let h = TypedEventHandler<TableViewGroupHeader, TableViewGroupHeaderToggleRequestedEventArgs>(fun _ e -> handler tableViewGroupHeader e)
                    tableViewGroupHeader.add_ToggleRequested h
                    { new IDisposable with
                        member _.Dispose() = tableViewGroupHeader.remove_ToggleRequested h }
                )
            })

    let tableViewGroupHeader () =
        createNode<TableViewGroupHeader> tableViewGroupHeaderNodeId
