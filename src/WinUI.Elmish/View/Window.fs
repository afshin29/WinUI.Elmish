namespace WinUI.Elmish.View

open Microsoft.UI.Xaml

open WinUI.Elmish.VirtualElements


[<AutoOpen>]
module WindowElement =

    type VirtualWindow with

        member window.treatFirstElementAsTitleBar () =
            { window with
                TreatFirstElementAsTitleBar = true }
    
        member window.title(title: string) =
            { window with
                Title = title }

        member window.size(width, height) =
            { window with
                Size = Some <| Windows.Graphics.SizeInt32(width, height) }

        member window.requestedTheme(theme) =
            { window with
                RequestedTheme = Some theme }
    
        member window.backdrop(backdrop: WindowBackdrop) =
            { window with
                Backdrop = Some backdrop }
        
        member window.config(config: WindowConfig) =
            { window with
                Configs = window.Configs @ [config] }
        
        member window.configIf(condition: bool, config: WindowConfig) =
            if condition then window.config(config) else window

        member window.ref(instance: Window ref) =
            { window with
                Ref = Some instance }


    let mainWindow (content: IVirtualNode) =
        {
            Content = content
            TreatFirstElementAsTitleBar = false
            Title = null
            Size = None
            RequestedTheme = None
            Backdrop = None
            Configs = []
            Ref = None
        }