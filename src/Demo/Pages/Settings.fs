module Demo.Pages.Settings

open Elmish
open Microsoft.UI.Xaml
open WinUI.Elmish
open WinUI.Elmish.View

type Model =
    {
        UserTheme: ElementTheme
        Backdrop: VirtualElements.WindowBackdrop
    }

type Msg =
    | ChangeTheme of ElementTheme
    | ChangeBackdrop of VirtualElements.WindowBackdrop

let init () =
    {
        UserTheme = ElementTheme.Default
        Backdrop = VirtualElements.WindowBackdrop.Acrylic
    }
    , Cmd.none

let update msg model =
    match msg with
    | ChangeTheme theme -> { model with UserTheme = theme }
    | ChangeBackdrop backdrop -> { model with Backdrop = backdrop }
    , Cmd.none

let view (model: Model) (dispatch: Dispatch<Msg>) =
     vstack()
        .spacing(16)
        .center()
        .children([
                    
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