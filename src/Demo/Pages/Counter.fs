module Demo.Pages.Counter

open Elmish
open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open WinUI.Elmish.View

type Model =
    {
        Count: int
    }

type Msg =
    | Increment
    | Decrement

let init () = { Count = 0 }, Cmd.none

let update msg model =
    match msg with
    | Increment -> { model with Count = model.Count + 1 }
    | Decrement -> { model with Count = model.Count - 1 }
    , Cmd.none

let view (model: Model) (dispatch: Dispatch<Msg>) =
    grid(
        [GridLength(2, GridUnitType.Star); GridLength(3, GridUnitType.Star)],
        [GridLength(1, GridUnitType.Star)])
        .children([
                infoBar'(null, "You have reached maximum count", InfoBarSeverity.Warning)
                    .isClosable(false)
                    .center()
                    .isOpen(model.Count = 5)
                    
                infoBar'(null, "You have reached minimum count", InfoBarSeverity.Warning)
                    .isOpen(true)
                    .isClosable(false)
                    .center()
                    .visibleIf(model.Count = -5)

                vstack()
                    .spacing(12)
                    .grid_row(1)
                    .children([
                        button_accent'("+ Increment")
                            .center()
                            .onClick(fun _ -> dispatch Increment)
                    
                        textBlock'($"Count: {model.Count.ToString()}")
                            .fontSize(18)
                            .center()
                    
                        button'("- Decrement")
                            .center()
                            .onClick(fun _ -> dispatch Decrement)
                    ])
            ])