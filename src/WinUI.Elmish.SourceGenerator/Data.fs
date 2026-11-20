module SourceGenerator.Data

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.Reflection


type WinUIElementTreeNode =
    {
        Type: Type
        IsRenderable: bool
        Name: string
        FullName: string
        BaseTypeName: string
        DependencyProperties: string array
        Properties: string array
        Events: string array
        Methods: string array
        IsConstructable: bool
        DerivedElements: WinUIElementTreeNode array
        //DerivedElements: ObservableCollection<WinUIElementTreeNode>
    }

let rec findNode (name: string) (node: WinUIElementTreeNode) =
    if node.Name = name then node
    else
        node.DerivedElements
        |> Array.pick (fun child ->
            if child.Name = name then Some child
            else
                try Some (findNode name child)
                with _ -> None
        )

let rec private typeName (t: Type) : string =
    if t.IsArray then
        typeName (t.GetElementType()) + "[]"
    elif t.IsGenericType then
        let args = t.GetGenericArguments() |> Array.map typeName
        let baseName =
            let i = t.Name.IndexOf '`'
            if i >= 0 then t.Name.Substring(0, i) else t.Name
        if t.GetGenericTypeDefinition() = typedefof<Nullable<_>> then args.[0] + "?"
        else sprintf "%s<%s>" baseName (String.Join(", ", args))
    else
        t.Name

let private flags =
    BindingFlags.Public ||| BindingFlags.Instance ||| BindingFlags.Static ||| BindingFlags.DeclaredOnly

let private safe (f: unit -> 'a[]) : 'a[] =
    try f () with _ -> [||]

let rec private generateTreeNode (baseType: Type) =
    
    let props = safe (fun () -> baseType.GetProperties flags)
    let events = safe (fun () -> baseType.GetEvents flags)
    let methods = safe (fun () -> baseType.GetMethods flags)
    
    let dps =
        props
        |> Array.filter (fun p -> p.PropertyType = typeof<DependencyProperty>)
        |> Array.map _.Name
        |> Array.sort
    let dpNames = set dps

    let dependencyProps =
        dps
        |> Array.map (fun dpName ->
            let baseName =
                if dpName.EndsWith("Property") then dpName.Substring(0, dpName.Length - "Property".Length)
                else dpName
            let valueType =
                match props |> Array.tryFind (fun p -> p.Name = baseName && p.PropertyType <> typeof<DependencyProperty>) with
                | Some p -> Some p.PropertyType
                | None ->
                    methods
                    |> Array.tryFind (fun m -> m.IsStatic && m.Name = "Get" + baseName && m.GetParameters().Length = 1)
                    |> Option.map (fun m -> m.ReturnType)
            match valueType with
            | Some ty -> sprintf "%s (%s)" dpName (typeName ty)
            | None -> dpName
        )

    let properties =
        props
        |> Array.filter (fun p -> p.PropertyType <> typeof<DependencyProperty>)
        |> Array.filter (fun p -> not <| dpNames.Contains(p.Name + "Property"))
        |> Array.map (fun p ->
            let g = p.GetGetMethod()
            let s = p.GetSetMethod()
            let isStatic = (not (isNull g) && g.IsStatic) || (not (isNull s) && s.IsStatic)
            let acc =
                ((if isNull g then "" else "get; ") + (if isNull s then "" else "set;")).Trim()
            let text =
                sprintf "%s%s : %s { %s }" (if isStatic then "static " else "") p.Name (typeName p.PropertyType) acc
            p.Name, text
        )
        |> Array.sortBy fst
        |> Array.map snd

    let events =
        events
        |> Array.map (fun e ->
            let isStatic = not (isNull e.AddMethod) && e.AddMethod.IsStatic
            e.Name, sprintf "%s%s : %s" (if isStatic then "static " else "") e.Name (typeName e.EventHandlerType)
        )
        |> Array.sortBy fst
        |> Array.map snd

    let methods =
        methods
        |> Array.filter (fun m -> not m.IsSpecialName)
        |> Array.choose (fun m ->
            try
                let ps =
                    m.GetParameters()
                    |> Array.map (fun p -> sprintf "%s %s" (typeName p.ParameterType) p.Name)
                let generic =
                    if m.IsGenericMethod then
                        "<" + String.Join(", ", m.GetGenericArguments() |> Array.map typeName) + ">"
                    else ""
                let text =
                    sprintf "%s%s%s(%s) : %s" (if m.IsStatic then "static " else "") m.Name generic
                        (String.Join(", ", ps)) (typeName m.ReturnType)
                Some((m.Name, ps.Length), text)
            with _ -> None)
        |> Array.sortBy fst
        |> Array.map snd

    let isRenderable =
        not baseType.IsAbstract
        && baseType.GetConstructor(Type.EmptyTypes) |> isNull |> not

    let derivedTypes: Type array =
        try baseType.Assembly.GetTypes()
        with :? ReflectionTypeLoadException as e -> e.Types |> Array.filter (not << isNull)
        |> Array.filter (fun t ->
            t <> baseType
            //&& not t.IsAbstract
            && not t.IsGenericTypeDefinition
            && t.BaseType = baseType
        )

    {
        Type = baseType
        IsRenderable = isRenderable
        Name = baseType.Name
        FullName = baseType.FullName
        BaseTypeName = baseType.BaseType.Name
        DependencyProperties = dependencyProps
        Properties = properties
        Events = events
        Methods = methods
        IsConstructable = false
        //DerivedElements = (derivedTypes |> Array.map generateTreeNode |> ObservableCollection)
        DerivedElements = (derivedTypes |> Array.map generateTreeNode)
    }

let internal generateElementTree () =
    typeof<UIElement>
    |> generateTreeNode