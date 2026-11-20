module internal WinUI.Elmish.Reconciler

open Microsoft.UI.Composition.SystemBackdrops
open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open Microsoft.UI.Windowing
open System.Collections.Generic

open IdGenerator
open VirtualElements


type Renderer = IVirtualNode -> UIElement
let renderers = Dictionary<NodeId, Renderer>()


let rec renderNode (node: IVirtualNode) : UIElement =
    match node.RenderCondition with
    | Some false -> null
    | _ ->
        let renderer = renderers[node.Id]

        let uiElement = renderer node

        uiElement


let inline private tryGetFirstLayoutChild (element: UIElement) =
    match element with
    | :? Panel as panel -> panel.Children |> Seq.tryHead
    | _ -> None

let inline setBackdrop (window: Window) backdrop =
    window.SystemBackdrop <-
        match backdrop with
        | WindowBackdrop.Acrylic -> DesktopAcrylicBackdrop() :> SystemBackdrop
        | WindowBackdrop.Mica -> MicaBackdrop(Kind = MicaKind.Base)
        | WindowBackdrop.MicaAlt -> MicaBackdrop(Kind = MicaKind.BaseAlt)

let renderWindow (window: VirtualWindow) =
    let content = renderNode window.Content

    let mainWindow = Window()

    if window.TreatFirstElementAsTitleBar then
        content
        |> tryGetFirstLayoutChild
        |> Option.iter (fun titleBar ->
            mainWindow.ExtendsContentIntoTitleBar <- true
            mainWindow.SetTitleBar(titleBar)
            //mainWindow.AppWindow.TitleBar.PreferredHeightOption <- TitleBarHeightOption.Tall
            (content :?> FrameworkElement).add_ActualThemeChanged(fun sender args ->
                mainWindow.AppWindow.TitleBar.PreferredTheme <-
                    match sender.ActualTheme with
                    | ElementTheme.Light -> TitleBarTheme.Light
                    | ElementTheme.Dark -> TitleBarTheme.Dark
                    | _ -> TitleBarTheme.UseDefaultAppMode)
            )

    if window.RequestedTheme.IsSome then
        mainWindow.AppWindow.TitleBar.PreferredTheme <-
            match window.RequestedTheme.Value with
            | ElementTheme.Light -> TitleBarTheme.Light
            | ElementTheme.Dark -> TitleBarTheme.Dark
            | _ -> TitleBarTheme.UseDefaultAppMode
    
    mainWindow.Content <- content

    mainWindow.Title <- window.Title

    window.Size
    |> Option.iter (fun size -> mainWindow.AppWindow.Resize(size))

    window.Backdrop
    |> Option.iter (setBackdrop mainWindow)

    window.Configs
    |> List.iter (fun config -> config mainWindow)
    
    window.Ref
    |> Option.iter (fun w -> w.Value <- mainWindow)

    mainWindow