module Demo.Program

open Elmish
open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media.Animation
open WinUI.Elmish
open WinUI.Elmish.View


let titleBarRef = System.WeakReference<TitleBar>(null)

type AppPage =
    | CounterPageStateful
    | CounterPageStateless
    | ListPage
    | SettingsPage

type Model = 
    {
        CurrentPage: AppPage
        IsPaneOpen: bool
        CounterModelStateful: Pages.Counter.Model
        CounterModelStateless: Pages.Counter.Model option
        ListModel: Pages.List.Model
        SettingsModel: Pages.Settings.Model
    }

type Msg =
    | NoOp
    | Navigate of AppPage
    | TogglePane
    | CounterMsg of Pages.Counter.Msg
    | ListMsg of Pages.List.Msg
    | SettingsMsg of Pages.Settings.Msg

let init () =
    let counterModel, counterCmd = Pages.Counter.init ()
    let listModel, listCmd = Pages.List.init ()
    let settingsModel, settingsCmd = Pages.Settings.init ()

    {
        CurrentPage = AppPage.CounterPageStateful
        IsPaneOpen = true
        CounterModelStateful = counterModel
        CounterModelStateless = None
        ListModel = listModel
        SettingsModel = settingsModel
    },
    Cmd.batch [
        Cmd.map CounterMsg counterCmd
        Cmd.map ListMsg listCmd
        Cmd.map SettingsMsg settingsCmd
    ]

let update msg model =
    match msg with
    | NoOp -> model, Cmd.none
    | Navigate page ->
        if page = CounterPageStateless then
            let model', cmd = Pages.Counter.init ()
            { model with
                CurrentPage = page; CounterModelStateless = Some model' }
            , Cmd.map CounterMsg cmd
        else
            { model with
                CurrentPage = page; CounterModelStateless = None }, Cmd.none
    | TogglePane -> { model with IsPaneOpen = not model.IsPaneOpen }, Cmd.none
    | CounterMsg msg' ->
        if model.CurrentPage = CounterPageStateful then
            let model', cmd = Pages.Counter.update msg' model.CounterModelStateful
            { model with CounterModelStateful = model' }
            , Cmd.map CounterMsg cmd
        else
            let model', cmd = Pages.Counter.update msg' model.CounterModelStateless.Value
            { model with CounterModelStateless = Some model' }
            , Cmd.map CounterMsg cmd
    | ListMsg msg' ->
        let model', cmd = Pages.List.update msg' model.ListModel
        { model with ListModel = model' }
        , Cmd.map ListMsg cmd
    | SettingsMsg msg' ->
        let model', cmd = Pages.Settings.update msg' model.SettingsModel
        { model with SettingsModel = model' }
        , Cmd.map CounterMsg cmd
    

let view (model: Model) (dispatch: Dispatch<Msg>) =

    mainWindow(
        grid(
            [GridLength.Auto; GridLength(1, GridUnitType.Star)],
            [GridLength(1, GridUnitType.Star)]
        )
            .children([
                titleBar()
                    .title("WinUI Elmish")
                    .isPaneToggleButtonVisible(true)
                    .onPaneToggleRequested(fun _ -> dispatch TogglePane)
                
                navigationView()
                    .paneDisplayMode(NavigationViewPaneDisplayMode.Left)
                    .isBackButtonVisible(NavigationViewBackButtonVisible.Collapsed)
                    .isSettingsVisible(true)
                    .isPaneToggleButtonVisible(false)
                    .isPaneOpen(model.IsPaneOpen)
                    .openPaneLength(160)
                    .contentTransitions([ContentThemeTransition() :> Transition])
                    .grid_row(1)
                    .content(
                        (match model.CurrentPage with
                        | CounterPageStateful -> Pages.Counter.view model.CounterModelStateful (CounterMsg >> dispatch) :> IVirtualNode
                        | CounterPageStateless -> Pages.Counter.view model.CounterModelStateless.Value (CounterMsg >> dispatch)
                        | ListPage -> Pages.List.view model.ListModel (ListMsg >> dispatch)
                        | SettingsPage -> Pages.Settings.view model.SettingsModel (SettingsMsg >> dispatch)),
                        model.CurrentPage
                    )
                    .menuItems'([
                        navigationViewItem()
                            .content("Counters")
                            .icon(fontIcon'("\uE8CB"))
                            .isExpanded(true)
                            .selectsOnInvoked(false)
                            .menuItems'([
                                navigationViewItem()
                                    .content("Stateful")
                                    .tag(0)
                                    .setIf(
                                        model.CurrentPage = CounterPageStateful,
                                        fun node -> node.isSelected(true)
                                    )
                                    :> IVirtualNode
            
                                navigationViewItem()
                                    .content("Stateless")
                                    .tag(1)
                            ])
                            :> IVirtualNode
    
                        navigationViewItem()
                            .content("List")
                            .icon(fontIcon'("\uEA42"))
                            .tag(2)
                    ])
                    .onSelectionChanged(
                        fun args ->
                            if args.IsSettingsSelected then
                                dispatch <| Navigate SettingsPage
                            else
                                match (args.SelectedItem :?> FrameworkElement).Tag with
                                | :? int as tag when tag = 0 -> dispatch <| Navigate CounterPageStateful
                                | :? int as tag when tag = 1 -> dispatch <| Navigate CounterPageStateless
                                | :? int as tag when tag = 2 -> dispatch <| Navigate ListPage
                                | _ -> dispatch NoOp
                    )
            ]
        )
            .requestedTheme(model.SettingsModel.UserTheme)
    )
        .treatFirstElementAsTitleBar()
        .backdrop(model.SettingsModel.Backdrop)
        .size(700, 500)

Program.mkProgram init update view
|> App.startSelfContained