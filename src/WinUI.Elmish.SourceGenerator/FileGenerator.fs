module SourceGenerator.FileGenerator

open System
open System.IO
open System.Threading.Tasks

open Data
open CodeGenerator


[<Literal>]
let dirName = "Generated_Files"

let private generateFsProj () =
    let key (name: string) = name.TrimStart('_').ToLowerInvariant()

    // folder-a.fs or _folder-a.fs inside folder-a
    let isMain (dir: string) (file: string) =
        let n = Path.GetFileNameWithoutExtension file
        let d = Path.GetFileName dir
        n = d || n = "_" + d

    let rec collect (dir: string) : string list =
        let main, others =
            Directory.GetFiles(dir, "*.fs")
            |> Array.toList
            |> List.partition (isMain dir)

        if main.Length > 1 then failwithf "Multiple main files in %s" dir

        let children =
            [ for f in others -> key (Path.GetFileNameWithoutExtension f), [ f ]
              for d in Directory.GetDirectories dir -> key (Path.GetFileName d), collect d ]
            |> List.sortWith (fun (a, _) (b, _) -> String.CompareOrdinal(a, b))
            |> List.collect snd

        main @ children   // folder's own file first, then everything beneath it

    let items =
        collect $"{dirName}\UIElement\FrameworkElement"
        |> List.map (fun f ->
            let rel = Path.GetRelativePath(dirName, f).Replace('\\', '/')
            sprintf "    <Compile Include=\"%s\" />" rel)

    File.WriteAllLines(
        $"{dirName}\Compile.props",
        [ "<Project>"; "  <ItemGroup>" ] @ items @ [ "  </ItemGroup>"; "</Project>" ])

let private deletePreviousFiles () =
    if Directory.Exists(dirName) then
        Directory.Delete(dirName, true)

let private getFileName (node: WinUIElementTreeNode) =
    if node.IsRenderable then
        $"{node.Name}.fs"
    else
        $"_{node.Name}.fs"

let generateFiles (root: WinUIElementTreeNode) =
    let rec write (parentDir: string) (node: WinUIElementTreeNode) =
        // nodes with children get their own folder, leaves go straight into the parent's folder
        let dir =
            if Array.isEmpty node.DerivedElements then parentDir
            else Path.Combine(parentDir, node.Name)

        Directory.CreateDirectory dir |> ignore
        File.WriteAllText(Path.Combine(dir, getFileName node), generateCode node.Type)

        node.DerivedElements |> Array.iter (write dir)

    deletePreviousFiles ()
    write dirName root

    generateFsProj ()