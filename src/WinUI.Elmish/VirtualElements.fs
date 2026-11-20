[<AutoOpen>]
module WinUI.Elmish.VirtualElements

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Collections.ObjectModel

open IdGenerator


type IVirtualNode =
    abstract member Id: NodeId
    abstract member Props: VirtualProperty list
    abstract member Events: VirtualEvent list
    abstract member MemoizationKey: obj option
    abstract member MemoizedPropsToUnset: VirtualProperty list
    abstract member RenderCondition: bool option

and PropertyKind =
    internal
    | Dependency of DependencyProperty * value: obj
    | Clr of set: (UIElement -> obj -> unit) * clear: (UIElement -> unit) * value: obj
    | InnerChild of IVirtualNode * set: (UIElement -> UIElement -> unit) * get: (UIElement -> UIElement) * contentReplaceKey: obj
    | InnerChildren of IVirtualNode list * set: (UIElement -> UIElement list -> unit) * get: (UIElement -> IList<UIElement>)
    | CollectionItems of items: obj list * keySelector: (obj -> obj) option * set: (UIElement -> obj seq -> unit) * get: (UIElement -> ObservableCollection<obj>)

and VirtualProperty =
    internal {
        Id: PropertyId
        Prop: PropertyKind
        DiffKey: obj option
    }

and VirtualEvent =
    internal {
        Key: string
        Subscribe: obj -> IDisposable
    }

and NodeConfig<'marker> = 'marker -> unit

type VirtualNode<'marker when 'marker: not struct> =
    internal {
        Id: NodeId
        Props: VirtualProperty list
        Events: VirtualEvent list
        Configs: NodeConfig<'marker> list
        MemoizationKey: obj option
        MemoizedPropsToUnset: VirtualProperty list
        RenderCondition: bool option
        Ref: WeakReference<'marker> option
    }

    interface IVirtualNode with
        member this.Id = this.Id
        member this.Props = this.Props
        member this.Events = this.Events
        member this.MemoizationKey = this.MemoizationKey
        member this.MemoizedPropsToUnset = this.MemoizedPropsToUnset
        member this.RenderCondition = this.RenderCondition

type WindowConfig = Window -> unit

type WindowBackdrop =
    | Acrylic
    | Mica
    | MicaAlt

type VirtualWindow =
    internal {
        Content: IVirtualNode
        TreatFirstElementAsTitleBar: bool
        Title: string
        Size: Windows.Graphics.SizeInt32 option
        RequestedTheme: ElementTheme option
        Backdrop: WindowBackdrop option
        Configs: WindowConfig list
        Ref: Window ref option
    }


let internal createProperty id prop =
    {
        Id = id
        Prop = prop
        DiffKey = None
    }

let internal createDependencyProperty id prop value =
    Dependency (prop, value)
    |> createProperty id

let internal createClrProperty id set clear value =
    Clr (set, clear, value)
    |> createProperty id

let internal createInnerChildProperty id node set get replaceKey =
    InnerChild (node, set, get, replaceKey)
    |> createProperty id

let internal createInnerChildrenProperty id nodes set get =
    InnerChildren (nodes, set, get)
    |> createProperty id

let internal createCollectionItemsProperty<'source> id (items: 'source seq) (keySelector: ('source -> obj) option) set get =
    CollectionItems (
        items |> Seq.cast<obj> |> Seq.toList,
        keySelector |> Option.map (fun f -> unbox<'source> >> f),
        set,
        get)
    |> createProperty id

let internal createNode<'marker when 'marker: not struct> id : VirtualNode<'marker> =
    {
        Id = id
        Props = []
        Events = []
        Configs = []
        MemoizationKey = None
        MemoizedPropsToUnset = []
        RenderCondition = None
        Ref = None
    }