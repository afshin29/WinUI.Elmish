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

