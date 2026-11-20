open ColorCode
open ColorCode.Styling
open Elmish
open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open Microsoft.Windows.ApplicationModel.WindowsAppRuntime
open System
open System.Numerics
open System.Threading.Tasks
open Windows.ApplicationModel.DataTransfer

open Microsoft.UI

open WinUI.Elmish
open WinUI.Elmish.View

open SourceGenerator.Data
open SourceGenerator.CodeGenerator
open SourceGenerator.FileGenerator
open Microsoft.UI.Xaml.Media.Imaging


let mutable appWindow: Window ref = ref null
let dialog = WeakReference<ContentDialog>(null)
let codeTextBlock = WeakReference<RichTextBlock>(null)

let setCodeOnTextBlock code =
    match codeTextBlock.TryGetTarget() with
    | true, textBlock ->
        textBlock.Blocks.Clear()

        let styles =
            match textBlock.ActualTheme with
            | ElementTheme.Dark -> StyleDictionary.DefaultDark
            | _ -> StyleDictionary.DefaultLight

        let formatter = RichTextBlockFormatter(styles)
        formatter.FormatRichTextBlock(code, Languages.FSharp, textBlock)

    | _ -> ()

type DataState =
    | UnInitialized
    | Initialized of rootNode: WinUIElementTreeNode * selected: WinUIElementTreeNode
    | FailedInitialization

type CodeState =
    | NotGenerated
    | Requested
    | Ready of string

type FileGenerationState =
    | FilesNotRequested
    | FilesRequested
    | FilesGenerated
    | FilesFailed

type DialogContent =
    | SettingsDialog
    | FileGenerationDialog

type Model =
    {
        Data: DataState
        Code: CodeState
        IsCodeView: bool
        FileGeneration: FileGenerationState
        Dialog: DialogContent
        UserTheme: ElementTheme
        Backdrop: VirtualElements.WindowBackdrop
    }
    
type Msg =
    | NoOp
    | InitCompleted of rootNode: WinUIElementTreeNode * selected: WinUIElementTreeNode
    | InitFailed
    | NodeSelected of TreeViewNode
    | CodeRequested
    | CodeReady of string
    | BackToDetail
    | CopyCode
    | OpenSettingsDialog
    | OpenFileGenerationDialog
    | GenerateFiles
    | FileGenerataionCompleted of success: bool
    | ChangeTheme of ElementTheme
    | ChangeBackdrop of VirtualElements.WindowBackdrop

let init () =
    {
        Data = UnInitialized
        Code = NotGenerated
        IsCodeView = false
        FileGeneration = FilesNotRequested
        Dialog = SettingsDialog
        UserTheme = ElementTheme.Default
        Backdrop = VirtualElements.WindowBackdrop.MicaAlt
    },
    Cmd.batch [
        Cmd.OfFunc.perform
            (fun _ ->
                let iconPath =
                    System.IO.Path.Combine(
                        AppContext.BaseDirectory,
                        "assets",
                        "logo.ico"
                    )

                appWindow.Value.AppWindow.SetIcon(iconPath))
            ()
            (fun _ -> NoOp)
        
        Cmd.OfTask.either
            (fun _ -> Task.Run(generateElementTree))
            ()
            (fun result -> InitCompleted (result, result))
            (fun _ -> InitFailed)
    ]

let update msg model =
    match msg with
    | NoOp -> model, Cmd.none
    
    | InitCompleted (root, selected) -> { model with Data = Initialized (root, selected) }, Cmd.none
    
    | InitFailed -> { model with Data = FailedInitialization }, Cmd.none
    
    | NodeSelected node ->
        match model.Data with
        | Initialized (root, _) ->
            { model with
                Data = Initialized (root, findNode (node.Content :?> string) root)
                Code = CodeState.NotGenerated
                IsCodeView = false }
        | _ -> model
        , Cmd.none
    
    | CodeRequested ->
        if model.Code.IsReady then
            { model with IsCodeView = true }, Cmd.none
        else
            let codeTask model =
                match model.Data with
                | Initialized (_, selected) -> Task.Run(fun () -> generateCode selected.Type)
                | _ -> Task.FromResult("</>")

            { model with Code = CodeState.Requested }, Cmd.OfTask.perform codeTask model (fun s -> CodeReady s)
    
    | CodeReady code ->
        { model with Code = CodeState.Ready code; IsCodeView = true },
        Cmd.OfFunc.perform setCodeOnTextBlock code (fun _ -> NoOp)
    
    | BackToDetail -> { model with IsCodeView = false }, Cmd.none
    
    | CopyCode ->
        let code =
            match model.Code with
            | Ready code -> code
            | _ -> "<no-code>"

        let package = DataPackage()
        package.SetText(code)
        Clipboard.SetContent(package)
        
        model, Cmd.none
    
    | OpenSettingsDialog ->
        match dialog.TryGetTarget() with
        | true, dialog ->
            let showDialog () =
                dialog.ShowAsync().AsTask() 

            { model with Dialog = SettingsDialog }
            , Cmd.OfTask.either showDialog () (fun _ -> NoOp) (fun _ -> NoOp)
        | _ -> model, Cmd.none
    
    | OpenFileGenerationDialog ->
        match dialog.TryGetTarget() with
        | true, dialog ->
            let showDialog () =
                dialog.ShowAsync().AsTask() 

            { model with Dialog = FileGenerationDialog }
            , Cmd.OfTask.either showDialog () (fun _ -> NoOp) (fun _ -> NoOp)
        | _ -> model, Cmd.none
    
    | GenerateFiles ->
        let fileGenerationTask () =
            match model.Data with
            | Initialized (root, _) -> Task.Run<unit>(fun () -> generateFiles root)
            | _ -> Task.FromResult()

        { model with FileGeneration = FilesRequested },
        Cmd.OfTask.either fileGenerationTask () (fun _ -> FileGenerataionCompleted true) (fun _ -> FileGenerataionCompleted false)
    
    | FileGenerataionCompleted success ->
        { model with FileGeneration = if success then FilesGenerated else FilesFailed }, Cmd.none
    
    | ChangeTheme theme ->
        let cmd =
            match model.Code with
            | Ready code -> Cmd.OfFunc.perform setCodeOnTextBlock code (fun _ -> NoOp)
            | _ -> Cmd.none
        
        { model with UserTheme = theme }, cmd
    
    | ChangeBackdrop backdrop -> { model with Backdrop = backdrop }, Cmd.none

let rec getTreeViewNode (root: WinUIElementTreeNode) =
    let node =
        TreeViewNode(
            Content = root.Name,
            IsExpanded = true
        )

    root.DerivedElements
    |> Array.map getTreeViewNode
    |> Array.sortBy (fun (node: TreeViewNode) -> (box node.Content) :?> string)
    |> Array.iter (node.Children.Add)

    node

let view (model: Model) (dispatch: Dispatch<Msg>) =
    mainWindow(
        grid(
            [GridLength.Auto; GridLength(1, GridUnitType.Star)],
            [GridLength(1, GridUnitType.Star); GridLength(2, GridUnitType.Star)]
        )
            .columnSpacing(12)
            .requestedTheme(model.UserTheme)
            .children([
                titleBar()
                    .title("WinUI.Elmish Source Generator")
                    .grid(0, 0)
                    .grid_columnSpan(2)
                    .margin(Thickness(0, 0, 0, 8))
                    .iconSource(ImageIconSource(ImageSource = BitmapImage(Uri("ms-appx:///assets/logo.ico"))))
                    .rightHeader(
                        hstack()
                            .children([
                                button()
                                    .onClick(fun _ -> dispatch OpenFileGenerationDialog)
                                    .content(
                                        fontIcon'("\uF000")
                                            .fontSize(16)
                                    )
                                    .height(48)
                                    .cornerRadius(CornerRadius(0))
                                    .borderThickness(Thickness(0))
                                    .background(SolidColorBrush(Colors.Transparent))

                                button()
                                    .onClick(fun _ -> dispatch OpenSettingsDialog)
                                    .content(
                                        fontIcon'("\uE713")
                                            .fontSize(16)
                                    )
                                    .height(48)
                                    .cornerRadius(CornerRadius(0))
                                    .borderThickness(Thickness(0))
                                    .background(SolidColorBrush(Colors.Transparent))
                            ])
                    )
                    .config(fun titleBar ->
                        titleBar.Resources["TitleBarContentHorizontalAlignment"] <- HorizontalAlignment.Stretch
                    )
                    .memoize()

                match model.Data with
                | UnInitialized ->
                    textBlock'("Loading...")
                        .grid_row(1)
                        .grid_columnSpan(2)
                        .center()

                | FailedInitialization ->
                    textBlock'("Initialization Failed :(")
                        .grid_row(1)
                        .grid_columnSpan(2)
                        .center()

                | Initialized (root, selected) ->
                    border()
                        .bindThemeResource(Border.BackgroundProperty, "CardBackgroundFillColorDefaultBrush")
                        .cornerRadius(CornerRadius(8))
                        .grid(1, 0)
                        .margin(Thickness(16, 0, 0, 16))
                        .child(
                            treeViewReadOnly([getTreeViewNode root])
                                .canDragItems(false)
                                .canReorderItems(false)
                                .onSelectionChanged(fun args ->
                                    args.AddedItems
                                    |> Seq.cast<TreeViewNode>
                                    |> Seq.head
                                    |> NodeSelected
                                    |> dispatch
                                )
                        )
                        .memoize()
                    
                    grid([GridLength.Auto; GridLength(1, GridUnitType.Star)], [])
                        .margin(Thickness(0, 0, 16, 16))
                        .grid(1, 1)
                        .children([
                            grid([], [GridLength(1, GridUnitType.Star); GridLength.Auto])
                                .bindThemeResource(Grid.BackgroundProperty, "CardBackgroundFillColorDefaultBrush")
                                .cornerRadius(CornerRadius(8))
                                .padding(Thickness(12))
                                .margin(Thickness(0, 0, 0, 16))
                                .children([
                                    vstack()
                                        .children([
                                            textBlock_subtitle(selected.FullName)
                                    
                                            textBlock_body($"Base: {selected.BaseTypeName}")
                                                .bindThemeResource(TextBlock.ForegroundProperty, "TextFillColorSecondaryBrush")
                                    
                                            textBlock_body($"Renderable")
                                                .bindThemeResource(TextBlock.ForegroundProperty, "SystemFillColorSuccessBrush")
                                                .visibleIf(selected.IsRenderable)
                                    
                                            textBlock_body($"Non-Renerable")
                                                .bindThemeResource(TextBlock.ForegroundProperty, "TextFillColorTertiaryBrush")
                                                .visibleIf(not selected.IsRenderable)
                                        ])
                                        
                                    button_accent'("Generate Code")
                                        .isEnabled(not model.Code.IsRequested)
                                        .visibleIf(not model.IsCodeView)
                                        .onClick(fun _ -> dispatch CodeRequested)
                                        .grid_column(1)
                                        
                                    button'("Back")
                                        .visibleIf(model.IsCodeView)
                                        .onClick(fun _ -> dispatch BackToDetail)
                                        .grid_column(1)
                                ])
                                .config(fun grid ->
                                    grid.Shadow <- ThemeShadow()
                                    grid.Translation <- Vector3(0f, 0f, 20f)
                                )

                            let getExpander title (children: string array) =
                                expander()
                                    .header($"{title} ({children.Length})")
                                    .isExpanded(false)
                                    .isEnabled(children.Length > 0)
                                    .horizontalAlignment(HorizontalAlignment.Stretch)
                                    .horizontalContentAlignment(HorizontalAlignment.Left)
                                    .content(
                                        vstack()
                                            .children([
                                                for row in children ->
                                                    textBlock_body(row)
                                                        .isTextSelectionEnabled(true)
                                            ])
                                    )

                            scrollView()
                                .visibleIf(not model.IsCodeView)
                                .grid_row(1)
                                .content(
                                    vstack()
                                        .spacing(8)
                                        .children([
                                            getExpander "Dependency Properties" selected.DependencyProperties
                                            getExpander "Properties" selected.Properties
                                            getExpander "Events" selected.Events
                                            getExpander "Methods" selected.Methods
                                        ])
                                )

                            scrollView()
                                .bindThemeResource(ScrollView.BackgroundProperty, "CardBackgroundFillColorDefaultBrush")
                                .visibleIf(model.IsCodeView)
                                .grid_row(1)
                                .content(
                                    richTextBlock()
                                        .isTextSelectionEnabled(true)
                                        .textWrapping(TextWrapping.NoWrap)
                                        .margin(Thickness(16))
                                        .ref(codeTextBlock)
                                )

                            button()
                                .content(
                                    fontIcon'("\uE8C8")
                                        .fontSize(16)
                                )
                                .onClick(fun _ -> dispatch CopyCode)
                                .margin(Thickness(0, 16, 16, 0))
                                .horizontalAlignment(HorizontalAlignment.Right)
                                .verticalAlignment(VerticalAlignment.Top)
                                .visibleIf(model.IsCodeView)
                                .grid_row(1)
                        ])


                let settingsDialogContent =
                    vstack()
                        .spacing(16)
                        .center()
                        .children([
                            textBlock'($"Windows App SDK: {ReleaseInfo.AsString}")
                                .fontSize(14)
                                .margin(Thickness(0, 0, 0, 8))

                            textBlock'("Theme")
                                .fontSize(18)

                            hstack()
                                .spacing(8)
                                .children([
                                    button'("System")
                                        .onClick(fun _ -> ChangeTheme ElementTheme.Default |> dispatch)
                                        .isEnabled(model.UserTheme <> ElementTheme.Default )
                                        .center()
                        
                                    button'("Light")
                                        .onClick(fun _ -> ChangeTheme ElementTheme.Light |> dispatch)
                                        .isEnabled(model.UserTheme <> ElementTheme.Light )
                                        .center()
                        
                                    button'("Dark")
                                        .onClick(fun _ -> ChangeTheme ElementTheme.Dark |> dispatch)
                                        .isEnabled(model.UserTheme <> ElementTheme.Dark )
                                        .center()
                                ])
                        
                            textBlock'("Backdrop")
                                .fontSize(18)

                            hstack()
                                .spacing(8)
                                .children([
                                    button'("Acrylic")
                                        .isEnabled(model.Backdrop <> VirtualElements.WindowBackdrop.Acrylic )
                                        .onClick(fun _ -> ChangeBackdrop VirtualElements.WindowBackdrop.Acrylic |> dispatch)
                        
                                    button'("Mica")
                                        .isEnabled(model.Backdrop <> VirtualElements.WindowBackdrop.Mica)
                                        .onClick(fun _ -> ChangeBackdrop VirtualElements.WindowBackdrop.Mica |> dispatch)
                        
                                    button'("Mica Alt")
                                        .isEnabled(model.Backdrop <> VirtualElements.WindowBackdrop.MicaAlt)
                                        .onClick(fun _ -> ChangeBackdrop VirtualElements.WindowBackdrop.MicaAlt|> dispatch)
                        
                                ])
                        ])
                
                let fileGenerationDialogContent =
                    vstack()
                        .spacing(16)
                        .center()
                        .children([
                            let text =
                                match model.FileGeneration with
                                | FilesNotRequested -> $"Files and folders will be generated inside the '{dirName}' folder in the process directory."
                                | FilesRequested -> "Generating files..."
                                | FilesGenerated -> "Files were successfuly generated."
                                | FilesFailed -> "Failed to generate files. Make sure the process has access and close all previously genereted files."
                                
                            textBlock_body(text)
                        ])

                contentDialog()
                    .grid_row(1)
                    .setIf(
                        model.Dialog.IsFileGenerationDialog,
                        fun node ->
                            node
                                .title("File Generation")
                                .closeButtonText("Close")
                                .primaryButtonText("Generate Files")
                                .isPrimaryButtonEnabled(not model.FileGeneration.IsFilesRequested)
                                .defaultButton(ContentDialogButton.Primary)
                                .onPrimaryButtonClick(fun arg ->
                                    arg.Cancel <- true
                                    dispatch GenerateFiles
                                )
                    )
                    .setIf(
                        model.Dialog.IsSettingsDialog,
                        fun node ->
                            node
                                .title("Settings")
                                .closeButtonText("Done")
                    )
                    .ref(dialog)
                    .content(
                        match model.Dialog with
                        | SettingsDialog -> settingsDialogContent
                        | FileGenerationDialog -> fileGenerationDialogContent
                    )
            ])
    )
        .treatFirstElementAsTitleBar()
        .requestedTheme(model.UserTheme)
        .backdrop(model.Backdrop)
        .size(1000, 600)
        .ref(appWindow)


[<EntryPoint; STAThread>]
let main _ =
    Program.mkProgram init update view
    |> App.startSelfContained

    0