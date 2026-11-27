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

```fsharp
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

```fsharp
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
    .height(model.BtnHeight)
    .memoizeOn(model.BtnHeight)
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

```fsharp
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

In version `2.6.6-preview.1`, the template you provide, would be wrapped in a `ContentControl`. This is 
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

However, it turned out that some controls have issues with this approach and they would wrap our `ContentControl` yet another 
time. One example that I encountered is the `TreeView` control.

So, in version `2.6.6-preview.2`, the data template creation changed to this:

```fsharp
let private refreshDiff (previous: IVirtualNode option) (node: IVirtualNode) : NodeDiff =
    // ...

let internal getDataTemplate<'source, 'el when 'el :> FrameworkElement and 'el: (new: unit -> 'el) and 'el: not struct>
    (template: 'source -> VirtualNode<'el>) =
    DataTemplate(fun () ->
        let element = new 'el()
        let frameworkElement = element :> FrameworkElement
        let uiElement = frameworkElement :> UIElement
        let previous: IVirtualNode option ref = ref None

        frameworkElement.add_DataContextChanged(fun _ args ->
            args.Handled <- true

            match args.NewValue with
            | :? 'source as item ->
                let node = template item

                if previous.Value.IsNone then
                    applyConfigs element node.Configs

                node.Ref
                |> Option.iter (fun weakRef -> weakRef.SetTarget(element))

                refreshDiff previous.Value node
                |> applyDiff uiElement
                |> ignore

                previous.Value <- Some (node :> IVirtualNode)

            | _ -> ()
        )

        uiElement
    )
```

If you find that even with this approach, your data template is being wrapped again, it most likely means a bug in 
WinUI itself. Again, the `TreeView` control had this issue (as of version 2.6.6).

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

### Content replacement key

If we look at `InnerChild` properties, we see a `contentReplaceKey` value:

```fsharp
    | InnerChild of IVirtualNode * set: (UIElement -> UIElement -> unit) * get: (UIElement -> UIElement) * contentReplaceKey: obj
```

this value tells the diff function that should this child element be diffed or replaced. Meaning, if the key is same 
between two Elmish iteration, the inner child will be diffed, but, if the key changes between two iterations, then 
the old child will be destroyed and a new one will be created. This key is necessary for implementing navigation. 
You can use `ContentControl` or any of its sub-controls (including `Frame`) as the navigation host. And you need to 
provide your navigation state as the key, which means when the page/view changes, the inner child will be replaced.

Let's look at the `content` property of `ContentControl`:

```fsharp
[<Extension>]
static member content<'m, 'vnode when 'm :> ContentControl and 'm: not struct and 'vnode :> IVirtualNode>
    (node: VirtualNode<'m>, value: 'vnode, ?contentReplaceKey: obj) =
    createInnerChildProperty
        vnode_contentPropertyId
        value
        (fun element child -> (element :?> ContentControl).Content <- child)
        (fun element -> (element :?> ContentControl).Content :?> UIElement)
        (contentReplaceKey |> Option.defaultValue 1)
    |> node.set
```

One of suitable controls for implementing navigation is `NavigationView`, which is derived from `ContentControl`:

```fsharp
navigationView()
    .content(
        (match model.CurrentPage with
        | CounterPageStateful -> Pages.Counter.view model.CounterModelStateful (CounterMsg >> dispatch) :> IVirtualNode
        | CounterPageStateless -> Pages.Counter.view model.CounterModelStateless.Value (CounterMsg >> dispatch)
        | ListPage -> Pages.List.view model.ListModel (ListMsg >> dispatch)
        | SettingsPage -> Pages.Settings.view model.SettingsModel (SettingsMsg >> dispatch)),
        model.CurrentPage
    )
```

Here, we are providing different views based on our current page, which is tracked in our model in the `CurrentPage`. 
We should use that state as the replacement key and replace our inner UI element when `model.CurrentPage` changes.

### Extending Title Bar

WinUI has a modern title bar control that can replace the legacy one. Using it, needs some specific settings. 
In WinUI.Elmish, there is an extension method on `VirtualWindow` type that facilitates this (including theme update):

```fsharp
type VirtualWindow with

    member window.treatFirstElementAsTitleBar () =
        { window with
            TreatFirstElementAsTitleBar = true }
```

Let's see how the reconciler uses this flag:

```fsharp
let inline private tryGetFirstLayoutChild (element: UIElement) =
    match element with
    | :? Panel as panel -> panel.Children |> Seq.tryHead
    | _ -> None

// ...

if window.TreatFirstElementAsTitleBar then
    content
    |> tryGetFirstLayoutChild
    |> Option.iter (fun titleBar ->
        mainWindow.ExtendsContentIntoTitleBar <- true
        mainWindow.SetTitleBar(titleBar)
        (content :?> FrameworkElement).add_ActualThemeChanged(fun sender args ->
            mainWindow.AppWindow.TitleBar.PreferredTheme <-
                match sender.ActualTheme with
                | ElementTheme.Light -> TitleBarTheme.Light
                | ElementTheme.Dark -> TitleBarTheme.Dark
                | _ -> TitleBarTheme.UseDefaultAppMode)
        )
```

So the rule is, if your main content of the window is a panel control (`Grid`, `StackPanel`, etc.) then, it's first element 
will be treated as the title bar. You are also responsible for the correct layout of that panel. The usage is like this:

```fsharp
mainWindow(
    grid(
        [GridLength.Auto; GridLength(1, GridUnitType.Star)], // <- we create an Auto row for the titleBar
        [GridLength(1, GridUnitType.Star)]
    )
        .children([
            titleBar() // <- this is the first element
            //    .grid_columnSpan(<number-of-columns>) // <- in case you have more than one column, make sure to span this

            // ...
            // other children
        ])
)
    .treatFirstElementAsTitleBar() // <- this is necessary
```

### Capturing values in your handlers

Capturing values in your event handlers is not a good idea, because those handlers will not change unless their key 
changes. And even worse if those controls are in a data template, those templates also won't change unless their 
`diffKey` changes. So if you capture your model in your handlers, make sure to use that model (or the part that changes) 
as the key.

### Theme changing

WinUI controls have the `ResquestedTheme` property that you can use to update your theme. There is one additional setting 
that you need to apply for title bar buttons. Those three buttons are managed by OS and we need to inform the OS to update 
their theme as well. That's why the `VirtualWindow` also has that property:

```fsharp
type VirtualWindow with

    member window.requestedTheme(theme) =
        { window with
            RequestedTheme = Some theme }
```

The reconciler uses it for the first render:

```fsharp
if window.RequestedTheme.IsSome then
    mainWindow.AppWindow.TitleBar.PreferredTheme <-
        match window.RequestedTheme.Value with
        | ElementTheme.Light -> TitleBarTheme.Light
        | ElementTheme.Dark -> TitleBarTheme.Dark
        | _ -> TitleBarTheme.UseDefaultAppMode
```

So make sure to provide it (you need to keep track of the current theme in your model):

```fsharp
mainWindow(
    //...
)
    .requestedTheme(model.UserTheme)
```

### Dynamic Theme Binding

As of version 2.6.6 of the experimental release of WinUI, it's possible to use dynamic theme binding without XAML. 
`FrameworkElement` has a method called `SetThemeResourceBinding`. In WinUI.Elmish, `VirtualNode`s also provide an 
extension method to facilitate this:

```fsharp
[<Extension>]
static member bindThemeResource<'m when 'm :> FrameworkElement and 'm: not struct>
    (node: VirtualNode<'m>, property: DependencyProperty, resourceName: string) =
    node.config(fun node -> node.SetThemeResourceBinding(property, resourceName))
```

Example sage:

```fsharp
border()
    .bindThemeResource(Border.BackgroundProperty, "CardBackgroundFillColorDefaultBrush")
```