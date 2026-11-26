module SourceGenerator.CodeGenerator

open System
open System.Collections.Generic
open System.Reflection
open System.Text
open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls

module private Impl =

    // =========================================================================
    // Model
    // =========================================================================

    type PropKind =
        | DpPlain                       // createDependencyProperty id Class.XProperty value
        | DpAttached of target: Type    // same, but named _x and constrained to the accessor's element type
        | DpContent                     // obj DP with an XTemplate sibling: string overload + child-element overload
        | DpTemplate                    // DataTemplate DP
        | DpTemplateSelector            // DataTemplateSelector DP
        | DpStyleSelector               // StyleSelector DP
        | DpTransitions                 // TransitionCollection DP: takes a Transition seq
        | DpItems                       // obj DP named *ItemsSource: createCollectionItemsProperty
        | ChildrenProp                  // read-only collection of UIElements: createInnerChildrenProperty
        | InnerChildProp                // property typed UIElement (e.g. Border.Child): createInnerChildProperty
        | ClrProp                       // createClrProperty id set clear value

    type PropModel =
        { PropName: string
          ValueType: Type
          Kind: PropKind }

    type EventModel =
        { EventName: string
          DelegateType: Type
          ArgsType: Type
          /// F# only exposes .AddHandler/.RemoveHandler for "standard" events, i.e. delegates whose
          /// first parameter is obj (RoutedEventHandler, EventHandler<T>, ...). Anything else, such as
          /// TypedEventHandler<UIElement, T>, is only reachable through add_X / remove_X.
          IsStandard: bool }

    type Members =
        { Props: PropModel list
          Events: EventModel list
          Skipped: string list } // ready-to-print reasons, emitted as comments

    type DpInfo =
        { Base: string
          Id: string
          DpValue: Type
          Attached: Type option }

    // =========================================================================
    // Naming helpers
    // =========================================================================

    let fsharpKeywords =
        HashSet<string>(
            ("abstract and as assert base begin class default delegate do done downcast downto elif else end "
             + "exception extern false finally fixed for fun function global if in inherit inline interface "
             + "internal lazy let match member module mutable namespace new null of open or override private "
             + "public rec return sig static struct then to true try type upcast use val void when while with "
             + "yield const component constraint include mixin parallel process protected pure sealed tailcall "
             + "trait virtual").Split(' ', StringSplitOptions.RemoveEmptyEntries)
        )

    let ident (s: string) =
        if fsharpKeywords.Contains s then "``" + s + "``" else s

    /// "Background" -> "background", "XYFocusKeyboardNavigation" -> "xyFocusKeyboardNavigation", "UIElement" -> "uiElement"
    let lowerFirst (s: string) =
        if String.IsNullOrEmpty s then
            s
        else
            let mutable i = 0
            while i < s.Length && Char.IsUpper s.[i] do
                i <- i + 1
            if i <= 1 then string (Char.ToLowerInvariant s.[0]) + s.Substring 1
            elif i = s.Length then s.ToLowerInvariant()
            else s.Substring(0, i - 1).ToLowerInvariant() + s.Substring(i - 1)

    let reservedLocals = set [ "handler"; "key"; "node"; "uiElement"; "h"; "e"; "value" ]

    /// Local variable used for the cast element inside an event subscription.
    let localVar (className: string) =
        let n = lowerFirst className
        if reservedLocals.Contains n then n + "Element" else ident n

    let stripArity (name: string) =
        let i = name.IndexOf '`'
        if i >= 0 then name.Substring(0, i) else name

    let rec outermost (t: Type) =
        if t.IsNested then outermost t.DeclaringType else t

    // =========================================================================
    // Type formatting (F# syntax)
    // =========================================================================

    let primitives : IDictionary<Type, string> =
        dict [
            typeof<bool>, "bool"
            typeof<byte>, "byte"
            typeof<sbyte>, "sbyte"
            typeof<int16>, "int16"
            typeof<uint16>, "uint16"
            typeof<int>, "int"
            typeof<uint32>, "uint32"
            typeof<int64>, "int64"
            typeof<uint64>, "uint64"
            typeof<float32>, "float32"
            typeof<float>, "float"
            typeof<decimal>, "decimal"
            typeof<char>, "char"
            typeof<string>, "string"
            typeof<obj>, "obj"
        ]

    /// Collects every (non-primitive) type mentioned by a type, generic definitions instead of instantiations.
    let rec collectTypes (acc: HashSet<Type>) (t: Type) =
        if primitives.ContainsKey t then
            ()
        elif t.IsArray then
            collectTypes acc (t.GetElementType())
        else
            acc.Add(if t.IsGenericType then t.GetGenericTypeDefinition() else t) |> ignore
            if t.IsGenericType then
                for a in t.GetGenericArguments() do
                    collectTypes acc a

    /// Returns a formatter: short names by default, namespace-qualified when two used types share a simple name.
    let makeFormatter (used: seq<Type>) : Type -> string =
        let collidingTypes =
            used
            |> Seq.map outermost
            |> Seq.distinct
            |> Seq.groupBy (fun t -> stripArity t.Name)
            |> Seq.filter (fun (_, ts) -> Seq.length ts > 1)
            |> Seq.collect snd
        let colliding = HashSet<Type>(collidingTypes)

        let rec fmt (t: Type) : string =
            match primitives.TryGetValue t with
            | true, name -> name
            | _ ->
                if t.IsArray then
                    fmt (t.GetElementType()) + "[]"
                else
                    let def = if t.IsGenericType then t.GetGenericTypeDefinition() else t
                    let prefix =
                        if def.IsNested then fmt def.DeclaringType + "."
                        elif colliding.Contains def && not (String.IsNullOrEmpty def.Namespace) then def.Namespace + "."
                        else ""
                    let args =
                        if t.IsGenericType then
                            "<" + String.Join(", ", t.GetGenericArguments() |> Array.map fmt) + ">"
                        else
                            ""
                    prefix + stripArity def.Name + args

        fmt

    // =========================================================================
    // Reflection
    // =========================================================================

    let flags =
        BindingFlags.Public ||| BindingFlags.Instance ||| BindingFlags.Static ||| BindingFlags.DeclaredOnly

    let safe (f: unit -> 'a[]) : 'a[] =
        try f () with _ -> [||]

    let isUsableType (t: Type) =
        not (isNull t) && t.IsVisible && not t.IsByRef && not t.IsPointer && not t.ContainsGenericParameters

    let paramCount (m: MethodBase) =
        try m.GetParameters().Length with _ -> -1

    let isStaticProperty (p: PropertyInfo) =
        let m = if isNull (p.GetGetMethod()) then p.GetSetMethod() else p.GetGetMethod()
        not (isNull m) && m.IsStatic

    let loadPublicTypes (asm: Assembly) : Type list =
        let types =
            try asm.GetTypes()
            with :? ReflectionTypeLoadException as e -> e.Types |> Array.filter (not << isNull)
        types |> Array.filter (fun t -> t.IsPublic || t.IsNestedPublic) |> Array.toList

    // Base types of all public types of an assembly, cached: "does this class have derived classes?"
    let baseTypeCache = Dictionary<Assembly, HashSet<Type>>()

    let baseTypesOf (asm: Assembly) : HashSet<Type> =
        match baseTypeCache.TryGetValue asm with
        | true, s -> s
        | _ ->
            let s =
                loadPublicTypes asm
                |> List.filter (fun t -> not t.IsGenericTypeDefinition && not (isNull t.BaseType))
                |> List.map (fun t -> t.BaseType)
                |> HashSet<Type>
            baseTypeCache.[asm] <- s
            s

    /// A class that can be created by the reconciler: concrete with a public parameterless constructor.
    /// (Composable base classes such as Panel or Control only expose protected constructors.)
    let isRenderable (t: Type) =
        not t.IsAbstract && not (isNull (t.GetConstructor(Type.EmptyTypes)))

    /// True for collection types such as UIElementCollection (an IList of UIElement-derived items).
    let isUIElementCollection (t: Type) =
        try
            Array.append [| t |] (t.GetInterfaces())
            |> Array.exists (fun i ->
                i.IsGenericType
                && i.GetGenericTypeDefinition() = typedefof<IList<_>>
                && typeof<UIElement>.IsAssignableFrom(i.GetGenericArguments().[0]))
        with _ -> false

    /// Everything the class declares itself (no inherited members) that we can generate code for.
    let extractMembers (t: Type) : Members =
        let dpType = typeof<DependencyProperty>
        let props = safe (fun () -> t.GetProperties flags)
        let fields = safe (fun () -> t.GetFields flags)
        let methods = safe (fun () -> t.GetMethods flags)
        let events = safe (fun () -> t.GetEvents flags)
        let skipped = ResizeArray<string>()
        let skip (kind: string) (name: string) (reason: string) =
            skipped.Add(sprintf "skipped %s %s: %s" kind name reason)

        // WinUI projections expose DependencyProperty identifiers as static properties; others use static fields.
        let dpIdentifiers =
            [ for p in props do
                  if p.PropertyType = dpType then yield p.Name
              for f in fields do
                  if f.FieldType = dpType then yield f.Name ]
            |> List.distinct
            |> List.sort

        let dpNames = set dpIdentifiers

        // ---- phase 1: resolve the value type of every dependency property
        let dpInfos : DpInfo list =
            dpIdentifiers
            |> List.choose (fun dpId ->
                if not (dpId.EndsWith("Property")) || dpId.Length <= "Property".Length then
                    skip "dependency property" dpId "identifier does not follow the <Name>Property convention"
                    None
                else
                    let baseName = dpId.Substring(0, dpId.Length - "Property".Length)
                    let wrapper =
                        props
                        |> Array.tryFind (fun p ->
                            p.Name = baseName && p.PropertyType <> dpType && not (isStaticProperty p))
                    match wrapper with
                    | Some w when isNull (w.GetSetMethod()) ->
                        skip "dependency property" dpId "read-only"
                        None
                    | Some w when not (isUsableType w.PropertyType) ->
                        skip "dependency property" dpId "unsupported value type"
                        None
                    | Some w ->
                        Some { Base = baseName; Id = dpId; DpValue = w.PropertyType; Attached = None }
                    | None ->
                        // XAML attached property: static GetXxx(element) / SetXxx(element, value)
                        let getter =
                            methods
                            |> Array.tryFind (fun m -> m.IsStatic && m.Name = "Get" + baseName && paramCount m = 1)
                        let hasSetter =
                            methods
                            |> Array.exists (fun m -> m.IsStatic && m.Name = "Set" + baseName && paramCount m = 2)
                        let attachedTarget =
                            getter
                            |> Option.bind (fun g ->
                                try Some(g.GetParameters().[0].ParameterType) with _ -> None)
                            |> Option.filter isUsableType
                        match getter, attachedTarget with
                        | Some g, Some target when hasSetter && isUsableType g.ReturnType ->
                            Some { Base = baseName; Id = dpId; DpValue = g.ReturnType; Attached = Some target }
                        | _ ->
                            skip "dependency property" dpId "no wrapper property and no usable attached-property accessors"
                            None)

        // ---- phase 2: decide how each one is generated
        // DataTemplate, or object-typed *Template properties such as RadioButtons.ItemTemplate
        let isTemplateDp (i: DpInfo) =
            i.Attached.IsNone
            && (i.DpValue = typeof<DataTemplate> || (i.DpValue = typeof<obj> && i.Base.EndsWith("Template")))

        let hasTemplateSibling (name: string) =
            dpInfos |> List.exists (fun i -> i.Base = name + "Template" && isTemplateDp i)

        let dependencyProps =
            dpInfos
            |> List.map (fun i ->
                let kind =
                    match i.Attached with
                    | Some target -> DpAttached target
                    | None ->
                        if typeof<UIElement>.IsAssignableFrom i.DpValue then InnerChildProp
                        elif isTemplateDp i then DpTemplate
                        elif i.DpValue = typeof<DataTemplateSelector> then DpTemplateSelector
                        elif i.DpValue = typeof<StyleSelector> then DpStyleSelector
                        elif i.DpValue = typeof<Microsoft.UI.Xaml.Media.Animation.TransitionCollection> then DpTransitions
                        elif i.DpValue = typeof<obj> && i.Base.EndsWith("ItemsSource") then DpItems
                        elif i.DpValue = typeof<obj>
                             && not (i.Base.EndsWith("Source"))
                             && hasTemplateSibling i.Base then DpContent
                        else DpPlain
                { PropName = i.Base; ValueType = i.DpValue; Kind = kind })

        // Plain CLR properties: writable, instance, not indexers, and not wrappers around a dependency property.
        let clrProps =
            props
            |> Array.toList
            |> List.choose (fun p ->
                let isIndexer =
                    try p.GetIndexParameters().Length > 0 with _ -> true
                if p.PropertyType = dpType then
                    None // DependencyProperty identifier, handled above
                elif dpNames.Contains(p.Name + "Property") then
                    None // wrapper around a DependencyProperty: not reported, it would only add noise
                elif isIndexer then
                    skip "property" p.Name "indexer"
                    None
                elif isStaticProperty p then
                    skip "property" p.Name "static"
                    None
                elif isUsableType p.PropertyType && isUIElementCollection p.PropertyType then
                    // e.g. Panel.Children: usually read-only, but its content can be filled
                    Some { PropName = p.Name; ValueType = p.PropertyType; Kind = ChildrenProp }
                elif isNull (p.GetSetMethod()) then
                    skip "property" p.Name "read-only"
                    None
                elif not (isUsableType p.PropertyType) then
                    skip "property" p.Name "unsupported type"
                    None
                else
                    let kind = if typeof<UIElement>.IsAssignableFrom p.PropertyType then InnerChildProp else ClrProp
                    Some { PropName = p.Name; ValueType = p.PropertyType; Kind = kind })
            |> List.sortBy (fun p -> p.PropName)

        let eventModels =
            events
            |> Array.toList
            |> List.choose (fun e ->
                let isStatic = not (isNull e.AddMethod) && e.AddMethod.IsStatic
                if isStatic then
                    skip "event" e.Name "static"
                    None
                else
                    try
                        let invoke = e.EventHandlerType.GetMethod("Invoke")
                        let ps = invoke.GetParameters()
                        if invoke.ReturnType = typeof<Void>
                           && ps.Length = 2
                           && isUsableType e.EventHandlerType
                           && isUsableType ps.[1].ParameterType then
                            Some { EventName = e.Name
                                   DelegateType = e.EventHandlerType
                                   ArgsType = ps.[1].ParameterType
                                   IsStandard = (ps.[0].ParameterType = typeof<obj>) }
                        else
                            skip "event" e.Name "delegate is not of the (sender, args) -> void shape"
                            None
                    with _ ->
                        skip "event" e.Name "could not inspect delegate"
                        None)
            |> List.sortBy (fun e -> e.EventName)

        { Props = (dependencyProps @ clrProps) |> List.distinctBy (fun p -> lowerFirst p.PropName)
          Events = eventModels
          Skipped = List.ofSeq skipped }

    // =========================================================================
    // Code generation
    // =========================================================================

    let baseOpens =
        [ "Microsoft.UI.Xaml"
          "Microsoft.UI.Xaml.Controls"
          "System"
          "System.Runtime.CompilerServices" ]

    let renderFile (t: Type) (isLeaf: bool) (renderable: bool) (m: Members) : string =
        // ---- which types / namespaces does this file mention?
        let used = HashSet<Type>()
        collectTypes used t
        for p in m.Props do
            collectTypes used p.ValueType
            match p.Kind with
            | DpAttached target -> collectTypes used target
            | DpItems -> collectTypes used typeof<System.Collections.ObjectModel.ObservableCollection<obj>>
            | _ -> ()
        for e in m.Events do
            collectTypes used e.DelegateType
            collectTypes used e.ArgsType

        let fmt = makeFormatter used
        let cls = fmt t

        let opens =
            used
            |> Seq.map (fun u -> u.Namespace)
            |> Seq.filter (fun n -> not (String.IsNullOrEmpty n))
            |> Seq.append baseOpens
            |> Seq.distinct
            |> Seq.sort
            |> Seq.toList

        let hasMembers = not m.Props.IsEmpty || not m.Events.IsEmpty
        let needsModule = hasMembers || renderable

        let out = StringBuilder()
        let add (s: string) = out.Append(s).Append('\n') |> ignore

        // ---- header
        add "namespace WinUI.Elmish.View"
        add ""
        for o in opens do
            add ("open " + o)
        add ""
        add "open Common"
        add "open WinUI.Elmish.IdGenerator"
        add "open WinUI.Elmish.VirtualElements"
        add "open WinUI.Elmish.EventSubscriptions"
        add "open WinUI.Elmish.Reconciler"

        // ---- members we could not generate, so the control can be investigated from its own file
        if not m.Skipped.IsEmpty then
            add ""
            for s in m.Skipped do
                add ("// " + s)

        if needsModule then
            add ""
            add ""
            add "[<AutoOpen>]"
            add (sprintf "module %sMod =" t.Name)
            add ""

            // ---- node id + default renderer (only for classes that can actually be instantiated)
            if renderable then
                let nodeId = lowerFirst t.Name + "NodeId"
                add (sprintf "    let private %s = getNextId ()" nodeId)
                add ""
                add (sprintf "    registerDefaultRenderer<%s> %s" cls nodeId)
                add ""

            // ---- property ids
            for p in m.Props do
                let x = lowerFirst p.PropName
                match p.Kind with
                | DpContent ->
                    add (sprintf "    let private str_%sPropertyId = getNextId ()" x)
                    add (sprintf "    let private vnode_%sPropertyId = getNextId ()" x)
                | _ ->
                    add (sprintf "    let private %sPropertyId = getNextId ()" x)
            if not m.Props.IsEmpty then
                add ""

            if hasMembers then
                add "    [<Extension>]"
                add (sprintf "    type %sExtensions =" t.Name)
                add ""

                /// Writes "[<Extension>] static member ..." up to and including the "=" line.
                /// Generic ('m) unless the class is a leaf; attached properties are always generic and
                /// constrained to the element type their accessors accept.
                let signature
                    (name: string)
                    (attachedTarget: string option)
                    (extraParams: string list)
                    (extraConstraints: string list)
                    (parameters: string)
                    =
                    let generic = attachedTarget.IsSome || not isLeaf
                    let target = defaultArg attachedTarget cls
                    let typeParams = (if generic then [ "'m" ] else []) @ extraParams
                    let constraints =
                        (if generic then [ sprintf "'m :> %s" target; "'m: not struct" ] else [])
                        @ extraConstraints
                    let nodeType = if generic then "VirtualNode<'m>" else sprintf "VirtualNode<%s>" cls
                    add "        [<Extension>]"
                    if typeParams.IsEmpty then
                        add (sprintf "        static member %s(node: %s, %s) =" name nodeType parameters)
                    else
                        let whenPart =
                            if constraints.IsEmpty then "" else " when " + String.Join(" and ", constraints)
                        add (sprintf "        static member %s<%s%s>" name (String.Join(", ", typeParams)) whenPart)
                        add (sprintf "            (node: %s, %s) =" nodeType parameters)

                let vnodeParams = [ "'vnode" ]
                let vnodeConstraints = [ "'vnode :> IVirtualNode" ]
                // templates are generic in their root element: the template function returns VirtualNode<'el>
                let templateParams = [ "'source"; "'el" ]
                let templateConstraints =
                    [ "'el :> FrameworkElement"; "'el: (new: unit -> 'el)"; "'el: not struct" ]

                // ---- dependency properties and CLR properties
                for p in m.Props do
                    let x = lowerFirst p.PropName
                    let name = ident x
                    let dp = sprintf "%s.%sProperty" cls p.PropName
                    let valueType = fmt p.ValueType

                    match p.Kind with
                    | DpPlain ->
                        signature name None [] [] (sprintf "value: %s" valueType)
                        add (sprintf "            createDependencyProperty %sPropertyId %s value" x dp)
                        add "            |> node.set"
                        add ""

                    | DpAttached target ->
                        signature (lowerFirst t.Name + "_" + x) (Some(fmt target)) [] [] (sprintf "value: %s" valueType)
                        add (sprintf "            createDependencyProperty %sPropertyId %s value" x dp)
                        add "            |> node.set"
                        add ""

                    | ClrProp ->
                        signature name None [] [] (sprintf "value: %s" valueType)
                        add "            createClrProperty"
                        add (sprintf "                %sPropertyId" x)
                        add (sprintf "                (fun uiElement _ -> (uiElement :?> %s).%s <- value)" cls p.PropName)
                        add (sprintf "                (fun uiElement -> (uiElement :?> %s).%s <- Unchecked.defaultof<%s>)" cls p.PropName valueType)
                        add "                value"
                        add "            |> node.set"
                        add ""

                    | DpContent ->
                        // plain value
                        signature name None [] [] "value: string"
                        add (sprintf "            createDependencyProperty str_%sPropertyId %s value" x dp)
                        add "            |> node.set"
                        add ""
                        // child element
                        let keyName = "contentReplaceKey"
                        signature name None vnodeParams vnodeConstraints (sprintf "value: 'vnode, ?%s: obj" keyName)
                        add "            createInnerChildProperty"
                        add (sprintf "                vnode_%sPropertyId" x)
                        add "                value"
                        add (sprintf "                (fun element child -> (element :?> %s).%s <- child)" cls p.PropName)
                        add (sprintf "                (fun element -> (element :?> %s).%s :?> UIElement)" cls p.PropName)
                        add (sprintf "                (%s |> Option.defaultValue 1)" keyName)
                        add "            |> node.set"
                        add ""

                    | InnerChildProp ->
                        signature name None [] [] "value: IVirtualNode, ?contentReplaceKey: obj"
                        add "            createInnerChildProperty"
                        add (sprintf "                %sPropertyId" x)
                        add "                value"
                        // the child arrives as UIElement: derived property types (IconElement, InfoBadge, ...)
                        // need a cast on the way in and an upcast on the way out
                        let isExact = (p.ValueType = typeof<UIElement>)
                        let setValue = if isExact then "child" else sprintf "(child :?> %s)" valueType
                        let getSuffix = if isExact then "" else " :> UIElement"
                        add (sprintf "                (fun element child -> (element :?> %s).%s <- %s)" cls p.PropName setValue)
                        add (sprintf "                (fun element -> (element :?> %s).%s%s)" cls p.PropName getSuffix)
                        add "                (contentReplaceKey |> Option.defaultValue 1)"
                        add "            |> node.set"
                        add ""

                    | DpItems ->
                        signature name None [ "'source"; "'key" ] [] "items: 'source seq, ?keySelector: ('source -> 'key)"
                        add "            createCollectionItemsProperty"
                        add (sprintf "                %sPropertyId" x)
                        add "                items"
                        add "                (keySelector |> Option.map (fun f -> f >> box))"
                        add (sprintf "                (fun element items -> (element :?> %s).%s <- ObservableCollection(items))" cls p.PropName)
                        add (sprintf "                (fun element -> (element :?> %s).%s :?> ObservableCollection<obj>)" cls p.PropName)
                        add "            |> node.set"
                        add ""

                    | ChildrenProp ->
                        signature name None [] [] "value: IVirtualNode list"
                        add "            createInnerChildrenProperty"
                        add (sprintf "                %sPropertyId" x)
                        add "                value"
                        add (sprintf "                (fun element children -> children |> List.iter (element :?> %s).%s.Add)" cls p.PropName)
                        add (sprintf "                (fun element -> (element :?> %s).%s)" cls p.PropName)
                        add "            |> node.set"
                        add ""

                    | DpTemplate ->
                        signature name None templateParams templateConstraints "template: ('source -> VirtualNode<'el>), ?diffKey: obj"
                        add "            getDataTemplate template"
                        add (sprintf "            |> createDependencyProperty %sPropertyId %s" x dp)
                        add "            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))"
                        add "            |> node.set"
                        add ""

                    | DpTemplateSelector ->
                        signature
                            name
                            None
                            templateParams
                            templateConstraints
                            "templates: ('source -> VirtualNode<'el>) seq, selector: ('source -> int), ?diffKey: obj"
                        add "            createTemplateSelector templates selector"
                        add (sprintf "            |> createDependencyProperty %sPropertyId %s" x dp)
                        add "            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))"
                        add "            |> node.set"
                        add ""

                    | DpTransitions ->
                        signature name None [] [] "transitions: Transition seq, ?diffKey: obj"
                        add "            let transitionCollection = TransitionCollection()"
                        add "            transitions"
                        add "            |> Seq.iter transitionCollection.Add"
                        add ""
                        add (sprintf "            createDependencyProperty %sPropertyId %s transitionCollection" x dp)
                        add "            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))"
                        add "            |> node.set"
                        add ""

                    | DpStyleSelector ->
                        signature name None [ "'source" ] [] "styles: Style seq, selector: ('source -> int), ?diffKey: obj"
                        add "            InternalStyleSelector(styles, selector)"
                        add (sprintf "            |> createDependencyProperty %sPropertyId %s" x dp)
                        add "            |> fun p -> p.useDiffKey(diffKey |> Option.orElse (Some 1))"
                        add "            |> node.set"
                        add ""

                // ---- events
                for e in m.Events do
                    let local = localVar t.Name
                    let args = fmt e.ArgsType
                    signature (sprintf "on%s" e.EventName) None [] [] (sprintf "handler: %s -> unit, ?key: string" args)
                    add "            node.handle({"
                    add (sprintf "                Key = key |> Option.defaultValue \"%s\"" e.EventName)
                    add "                Subscribe = (fun uiElement ->"
                    add (sprintf "                    let %s = uiElement :?> %s" local cls)
                    add (sprintf "                    let h = %s(fun _ e -> handler e)" (fmt e.DelegateType))
                    if e.IsStandard then
                        add (sprintf "                    %s.%s.AddHandler h" local e.EventName)
                        add "                    { new IDisposable with"
                        add (sprintf "                        member _.Dispose() = %s.%s.RemoveHandler h }" local e.EventName)
                    else
                        add (sprintf "                    %s.add_%s h" local e.EventName)
                        add "                    { new IDisposable with"
                        add (sprintf "                        member _.Dispose() = %s.remove_%s h }" local e.EventName)
                    add "                )"
                    add "            })"
                    add ""

            // ---- builder, last thing in the module
            if renderable then
                add (sprintf "    let %s () =" (ident (lowerFirst t.Name)))
                add (sprintf "        createNode<%s> %sNodeId" cls (lowerFirst t.Name))

        out.ToString().TrimEnd() + "\n"

    let generate (t: Type) : string =
        let isLeaf = not ((baseTypesOf t.Assembly).Contains t)
        renderFile t isLeaf (isRenderable t) (extractMembers t)

// =============================================================================
// Public API
// =============================================================================

/// Generates the complete F# source (namespace, opens, module, extension type, builder) for a WinUI type.
let generateCode (t: Type) : string = Impl.generate t