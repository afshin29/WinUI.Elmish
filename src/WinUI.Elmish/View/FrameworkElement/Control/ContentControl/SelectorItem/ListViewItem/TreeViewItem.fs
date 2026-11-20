namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Collections.ObjectModel
open System.Runtime.CompilerServices

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped dependency property TreeViewItemTemplateSettingsProperty: read-only


[<AutoOpen>]
module TreeViewItemMod =

    let private treeViewItemNodeId = getNextId ()

    registerDefaultRenderer<TreeViewItem> treeViewItemNodeId

    let private collapsedGlyphPropertyId = getNextId ()
    let private expandedGlyphPropertyId = getNextId ()
    let private glyphBrushPropertyId = getNextId ()
    let private glyphOpacityPropertyId = getNextId ()
    let private glyphSizePropertyId = getNextId ()
    let private hasUnrealizedChildrenPropertyId = getNextId ()
    let private isExpandedPropertyId = getNextId ()
    let private itemsSourcePropertyId = getNextId ()

    [<Extension>]
    type TreeViewItemExtensions =

        [<Extension>]
        static member collapsedGlyph(node: VirtualNode<TreeViewItem>, value: string) =
            createDependencyProperty collapsedGlyphPropertyId TreeViewItem.CollapsedGlyphProperty value
            |> node.set

        [<Extension>]
        static member expandedGlyph(node: VirtualNode<TreeViewItem>, value: string) =
            createDependencyProperty expandedGlyphPropertyId TreeViewItem.ExpandedGlyphProperty value
            |> node.set

        [<Extension>]
        static member glyphBrush(node: VirtualNode<TreeViewItem>, value: Brush) =
            createDependencyProperty glyphBrushPropertyId TreeViewItem.GlyphBrushProperty value
            |> node.set

        [<Extension>]
        static member glyphOpacity(node: VirtualNode<TreeViewItem>, value: float) =
            createDependencyProperty glyphOpacityPropertyId TreeViewItem.GlyphOpacityProperty value
            |> node.set

        [<Extension>]
        static member glyphSize(node: VirtualNode<TreeViewItem>, value: float) =
            createDependencyProperty glyphSizePropertyId TreeViewItem.GlyphSizeProperty value
            |> node.set

        [<Extension>]
        static member hasUnrealizedChildren(node: VirtualNode<TreeViewItem>, value: bool) =
            createDependencyProperty hasUnrealizedChildrenPropertyId TreeViewItem.HasUnrealizedChildrenProperty value
            |> node.set

        [<Extension>]
        static member isExpanded(node: VirtualNode<TreeViewItem>, value: bool) =
            createDependencyProperty isExpandedPropertyId TreeViewItem.IsExpandedProperty value
            |> node.set

        [<Extension>]
        static member itemsSource<'source, 'key>
            (node: VirtualNode<TreeViewItem>, items: 'source seq, ?keySelector: ('source -> 'key)) =
            createCollectionItemsProperty
                itemsSourcePropertyId
                items
                (keySelector |> Option.map (fun f -> f >> box))
                (fun element items -> (element :?> TreeViewItem).ItemsSource <- ObservableCollection(items))
                (fun element -> (element :?> TreeViewItem).ItemsSource :?> ObservableCollection<obj>)
            |> node.set

    let treeViewItem () =
        createNode<TreeViewItem> treeViewItemNodeId
