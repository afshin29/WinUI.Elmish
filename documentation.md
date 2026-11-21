## Prerequisites

I assume you already have a good knowledge of Elmish architecture and WinUI framework.

## Virtual Node

Everything starts with the `IVirtualNode` interface. What is a virtual node you ask? Imagine you had to redecorate your 
home furniture, moving tables and sofas around, to see if they look good is an expensive operation, but, if you had a miniature 
version of your home and its furniture, or if you had a 3D map of them in your computer, then you could decorate them there 
and only move your actual furniture once you were satisfied with how they look. Those miniature/digital decorations are 
still work, but they are much cheaper than the actual work of moving a table.

A virtual node is the same concept; it represents an actual WinUI element. Creating and destroying UI elements are 
expensive operations, but, creating and destroying their representations are relatively cheap. Let's look at the interface:

```fsharp
type IVirtualNode =
    abstract member Id: NodeId
    abstract member Props: VirtualProperty list
    abstract member Events: VirtualEvent list
    abstract member MemoizationKey: obj option
    abstract member MemoizedPropsToUnset: VirtualProperty list
    abstract member RenderCondition: bool option
```

So, a virtual node represents an actual UI element such as a `Button` or a `Grid`. These UI elements have many 
properties, so we need to represent them too. That's what `VirtualProperty` is for:

```fsharp
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
```

As you can see, there are different kinds of properties that we need to treat them differently. Let's start from top.

`Dependency`:

Properties such as `Height`, `Padding`, `Background`, etc. You should already have a good understanding of `DependencyObject` 
and `DependencyProperty` if you plan to use Microsoft's UI frameworks.

`Clr`:

Normal CLR properties such as `Border.BackgroundTransition`.

`InnerChild`, `InnerChildren`:

Now we're taking about special treatments, if we are representing a UI element with a virtual node, it means we also need 
to represent their inner UI elements with inner virtual nodes. Properties such as `Border.Child` and `Panel.Children` 
are two examples.

`CollectionItems`:

These are even more specialized properties that need special treatments. Basically anything that is a list of same items 
falls into this category, a list of products that you fetch from your server, a list of user's contacts, a list of images, 
etc. These items almost always have a unique identifier, if they don't, such as a list of `int`s or `string`s, then their 
index is their key.

## Diffing

Let's go back to our furniture example, suppose you change your current furniture state to a new state in a 3D software 
and now you want to actually apply it to your home. You would calculate the minimum amount of work to get to the new state. 
For example if only a sofa was replaced, you wouldn't take everything outside and start over, because that would be 
very expensive (which means you do a lot work and it takes more time to reach your new state).

Same concepts apply to our virtual nodes. Elmish will generate a new virtual tree every loop. We don't want to throw 
away our rendered UI elements and start over, because that would be very expensive (slow). We need to calculate the 
minimum amount of work to get from previous state to the new state.

Let's look at a virtual control:

```
Button
- Content = "Click Me"
- Foreground = Green
- Margin = 8
- OnClick = some handler
```

Now suppose on the next Elmish loop, we get this representation:

```
Button
- Content = "Click Me"
- Foreground = Black
- Margin = 8
- OnClick = some handler
```

Should we destroy the `Button` and create a new one? I hope your answer is no, because we can reach to the new state 
by only changing the foreground color. Other properties have not changed.

`Dependeny` and `Clr` diffings are simple, but things get tricky when we get to the other ones.

Let's look at a virtual `ListView`:

```
ListView
- Items = [
  { Id = 10; Name = "Banana" }
  { Id = 11; Name = "Apple" }
  { Id = 12; Name = "Peach" }
  { Id = 13; Name = "Orange" }
]
```

And the next Elmish loop:

```
ListView
- Items = [
  { Id = 10; Name = "Banana" }
  { Id = 17; Name = "BlueBerry" }
  { Id = 13; Name = "Orange" }
  { Id = 12; Name = "Peach" }
  { Id = 15; Name = "Avocado" }
]
```

So what has happened here? item 10 is unchanged, item 11 was removed, items 17 and 15 were added and items 12 and 13 
were switched. Turns out there are some complicated mathematics to figure out the cheapest way to reach to our new state.
The good news is that those unique IDs will make the collection diffing efficient.

Now let's look at a `Grid`s children:

```
Grid
- Children = [
 TitleBar
 Button
 VStack
]
```

These children don't have a unique ID. We have two options:
 1. ask user to provide a unique ID for each one
 2. ask user not to reorder them

I went with option 2. It is strongly suggested for users of WinUI.Elmish not to reorder their layout elements. Instead 
we can flag them "should this spot be rendered or not". That's the purpose of `RenderCondition` field. This means when 
we are diffing these children we assume the first child is always a `TitleBar`, the second child is always a `Button` 
and so on. But they can flag themselves if they want to be rendered or not. Other than that, WinUI elements also have 
`Visibility` property which can be used.

If for example you provide this on the next Elmish iteration:

```
Grid
- Children = [
 Border
 TitleBar
 Button
 VStack
]
```

The diff algorithm will NOT understand that the minimum work is to just add `Border` at the top, it will destroy and 
recreate all 3 previous children. The only time this behavior is acceptable is for initializing startup data which 
happens only one time. Other than that, you really should not reorder your nodes on Elmish iterations.

---

### Creating a Node

Creating a node is as simple as this:

```fsharp
button()

titleBar()

stackPanel()
```

### Setting properties

We can set properties using various extension methods:

```fsahrp
titleBar()
    .title("Title")

button()
    .content(
        fontIcon'("\uF000")
            .fontSize(16)
    )
    .height(48)
    .cornerRadius(CornerRadius(0))
    .borderThickness(Thickness(0))
    .background(SolidColorBrush(Colors.Transparent))
```

We can set conditional properties with `setIf` method:

```fsharp
button()
    .setIf(
        // bool Condition:
        model.SomeBool,

        // a function to set properties
        fun node -> node.height(48)
    )
```

It's important to know that the boolean condition is not a guard, and the setter function will always be called. Let's 
take a look at `setIf` definition:

```fsharp
[<Extension>]
static member setIf(node: VirtualNode<_>, condition: bool, setter: (VirtualNode<_> -> VirtualNode<_>)) =
    if condition then
        setter node
    else
        let existingIds =
            node.Props
            |> List.map (fun p -> p.Id)
            |> Set.ofList

        let toUnset =
            (setter node).Props
            |> List.filter (fun p -> not (isPropValueNull p) && not (existingIds.Contains p.Id))

        { node with
            MemoizedPropsToUnset = toUnset @ node.MemoizedPropsToUnset }
```

We need to keep track of conditional props for memoized nodes. This means that the following will throw exception:

```fsharp
listView()
    .setIf(
        model.SelectedContact.IsSome,
        fun node -> node.selectedItem(model.SelectedContact.Value)
    )
```

As mentioned, do not treat the condition as guard.

### 'marker Generic

But how can we limit an extension method to appear only on correct nodes? For example the `isOpen()` method on an `InfoBar` 
should not appear for a `Button`. The answer is the `'marker` generic:

```fsharp
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
```

with that marker we can limit an extension method like this:

```fsharp
[<Extension>]
static member isOpen(node: VirtualNode<InfoBar>, value: bool) =
    createDependencyProperty isOpenPropertyId InfoBar.IsOpenProperty value
    |> node.set
```

or like this:

```fsharp
[<Extension>]
static member tag<'m when 'm :> FrameworkElement and 'm: not struct>
    (node: VirtualNode<'m>, value: obj) =
    createDependencyProperty tagPropertyId FrameworkElement.TagProperty value
    |> node.set
```

### Conditional rendering

We can flag a node to be rendered conditionally like this:

```fsahrp
button()
    .content("Click Me")
    .renderIf(model.SomeCondition)
```

The definition is:

```fsharp
[<Extension>]
static member renderIf(node: VirtualNode<_>, condition: bool) =
    { node with
        RenderCondition = Some condition }
```

### Node configs

`NodeConfig` is a one time configuration of the node:

```fsharp
and NodeConfig<'marker> = 'marker -> unit
```

extension:

```fsharp
[<Extension>]
static member config(node: VirtualNode<_>, config: NodeConfig<_>) =
    { node with
        Configs = config :: node.Configs }
```

Usage example:

```fsharp
grid(
    //...
)
    .config(fun grid ->
        grid.Shadow <- ThemeShadow()
        grid.Translation <- Vector3(0f, 0f, 20f)
    )
```

### Memoization

Memoization tells the diff function, should this node be diffed or skipped. If you have a node that does not change on 
Elmish iteration then you should memoize it:

```fsharp
titleBar()
    .title("Title")
    .memoize()
```

If you know some node will only change based on a field in your model, then you can use that field as the memo key:

```fsharp
button()
    .content("Click Me")
    .memoizeOn(model.SomeField)
```

This means the diff function will skip changing this node, unless that key changes between two Elmish iterations.

### Reference to the UI element

We can ask for a reference to the rendered element like this:

```fsharp
let dialog = WeakReference<ContentDialog>(null)

// ...

contentDialog()
    .ref(dialog)
```

and a reference to main window like this:

```fsahrp
let mutable appWindow: Window ref = ref null

// ...

mainWindow(
    // ...
)
    .ref(appWindow)
```

### Event subscription

Let's take a look at `VirtualEvent`:

```fsharp
and VirtualEvent =
    internal {
        Key: string
        Subscribe: obj -> IDisposable
    }
```

Since we can't compare the `Subscribe` function, the diff function will decide if the subscription should be updated 
or not based on the provided key. This means that if you want conditional handlers for an event on a node, you need to 
provide different keys for each handler.

### Collection controls

Collection controls such as `ListView` require you to provide these:
 - some collection
 - an optional key selector for the collection (if you don't provide, then index will be key)
 - a function that turns your model into a node

```fsharp
listView()
    .itemsSource(model.Contacts, fun contact -> contact.Name)
    .itemTemplate(fun contact ->
        let favoriteStr = if contact.IsFavorite then " ⭐" else ""
        textBlock'($"Item: {contact.Name}{favoriteStr}")
    )
```

One thing you need to be aware of, is that the template you provide, will be wrapped in a `ContentControl`. This is 
because of how code-first `DataTemplate` works:

```fsharp
let internal getDataTemplate (template: 'source -> #IVirtualNode) =
    DataTemplate(fun () ->
        let host = ContentControl(
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Stretch
        )

        host.add_DataContextChanged(fun _ args ->
            host.Content <-
                match args.NewValue with
                | :? 'source as item -> item |> template |> renderNode
                | _ -> null
        )
                
        host :> UIElement
    )
```

This might cause an issue on some controls. One that I'm aware of is the `TreeView` control.

You can also provide multiple templates and a function that decides which index to choose:

```fsharp
type internal InternalTemplateSelector<'source>(templates: DataTemplate array, selector: 'source -> int) =
    inherit DataTemplateSelector()

    let select item =
        item
        |> unbox<'source>
        |> selector
        |> min (templates.Length - 1)
        |> max 0
        |> Array.get templates

    override _.SelectTemplateCore(item: obj) = select item
    override _.SelectTemplateCore(item: obj, _) = select item

let internal createTemplateSelector templates selector =
    let templates =
        templates
        |> Seq.map getDataTemplate
        |> Seq.toArray

    InternalTemplateSelector(templates, selector)
```

usage:

```fsharp
listView()
    .itemsSource(model.Contacts, fun contact -> contact.Name)
    .selectedItem(model.SelectedContact |> Option.toObj)
    .itemTemplateSelector(
        [
            fun item -> 
                let favoriteStr = if item.IsFavorite then " ⭐" else ""
                textBlock'($"Item: {item.Name}{favoriteStr}")
            fun item -> 
                let favoriteStr = if item.IsFavorite then " ❤️" else ""
                textBlock'($"Item: {item.Name}{favoriteStr}")
        ],
        (fun (contact: Contact) -> if contact.Name.Contains("John") then 0 else 1)
    )
```