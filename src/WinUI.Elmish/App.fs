namespace WinUI.Elmish

open VirtualElements

open Elmish

open System
open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Markup
open Microsoft.Windows.ApplicationModel.DynamicDependency


type private App<'model, 'msg, 'view>(program: Program<unit, 'model, 'msg, VirtualWindow>) =
    inherit Application()

    let metadataProvider = Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider()

    interface IXamlMetadataProvider with
        member _.GetXamlType(t: Type) : IXamlType =
            metadataProvider.GetXamlType(t)

        member _.GetXamlType(fullName: string) : IXamlType =
            metadataProvider.GetXamlType(fullName)

        member _.GetXmlnsDefinitions() : XmlnsDefinition[] =
            metadataProvider.GetXmlnsDefinitions()

    override this.OnLaunched(args: LaunchActivatedEventArgs) =
        this.Resources.MergedDictionaries.Add(Microsoft.UI.Xaml.Controls.XamlControlsResources())

        Program.runWithDispatch ViewState.synDispatch () program

module App =

    let private start' (program: Program<unit, 'model, 'msg, VirtualWindow>) =
        let view = program |> Program.view
        let program =
            program
            |> Program.withSetState (fun model dispatch ->
                view model dispatch |> ViewState.setState dispatch)

        WinRT.ComWrappersSupport.InitializeComWrappers()

        Application.Start(fun _ ->
            let _ = new App<'model, 'msg, _>(program)
            ())

    /// Framework-dependent: needs the Windows App SDK runtime installed.
    let start program =
        Bootstrap.Initialize(0x00020000u)
        try start' program
        finally Bootstrap.Shutdown()

    /// Self-contained: the runtime ships in the output folder.
    let startSelfContained program = start' program