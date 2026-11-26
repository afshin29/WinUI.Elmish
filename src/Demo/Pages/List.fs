module Demo.Pages.List

open Elmish
open Microsoft.UI.Xaml.Controls
open System
open WinUI.Elmish.View

type Contact =
    {
        Id: Guid
        Name: string
        BirthDate: DateOnly option
        IsFavorite: bool
    }

let private initialContacts =
    [|
        { Id = Guid.NewGuid(); Name = "John Doe"; BirthDate = DateOnly(1977, 1, 1) |> Some; IsFavorite = true }
        { Id = Guid.NewGuid(); Name = "Jane Doe"; BirthDate = None; IsFavorite = false }
    |]

type Model =
    {
        Contacts: Contact array
        SelectedContact: Contact option
    }

type Msg =
    | ContactSelected of Contact option
    | AddContact of Contact * int
    | UpdateContact of Contact * int
    | RemoveContact of  int
    | MoveContact of Contact * int * int

let init () = { Contacts = initialContacts; SelectedContact = None }, Cmd.none

let update msg model =
    match msg with
    | ContactSelected c -> { model with SelectedContact = c }
    | AddContact (c, idx) -> { model with Contacts = model.Contacts |> Array.insertAt idx c }
    | UpdateContact (c, idx) -> { model with Contacts = model.Contacts |> Array.updateAt idx c }
    | RemoveContact idx -> { model with Contacts = model.Contacts |> Array.removeAt idx }
    | MoveContact (c, currIdx, newIdx) -> { model with Contacts = model.Contacts |> Array.removeAt currIdx |> Array.insertAt newIdx c }
    , Cmd.none

let view (model: Model) (dispatch: Dispatch<Msg>) =
    vstack()
        .spacing(8)
        .center()
        .grid(1, 2)
        .children([

            textBlock'(
                model.SelectedContact
                |> Option.map _.Name
                |> Option.defaultValue "<nothing-selected>"
            )

            hstack()
                .spacing(8)
                .children([
                    button'("Remove")
                        .isEnabled(model.Contacts.Length > 0)
                        .onClick(
                            (fun _ -> dispatch <| RemoveContact (model.Contacts.Length - 1)),
                            ?key = (Some <| (model.Contacts.Length - 1).ToString()))
                        
                    button'("Remove First")
                        .isEnabled(model.Contacts.Length > 0)
                        .onClick(fun _ -> dispatch <| RemoveContact 0)
                ])
                    
            //listView()
            //    .itemsSource(model.Contacts, fun contact -> contact.Name)
            //    .itemTemplate(fun contact ->
            //        let favoriteStr = if contact.IsFavorite then " ⭐" else ""
            //        textBlock'($"Item: {contact.Name}{favoriteStr}")
            //    )

            listView()
                .itemsSource(model.Contacts, fun contact -> contact.Name)
                .selectedItem(model.SelectedContact |> Option.toObj)
                .itemTemplateSelector(
                    [
                        fun item -> 
                            let favoriteStr = if item.IsFavorite then " ⭐" else ""
                            textBlock'($"Item: {item.Name}{favoriteStr}")
                        fun item -> 
                            let favoriteStr = if item.IsFavorite then " ❤️" else ""
                            textBlock'($"Item: {item.Name}{favoriteStr}")
                    ],
                    (fun (contact: Contact) -> if contact.Name.Contains("John") then 0 else 1)
                )
                .selectionMode(ListViewSelectionMode.Single)
                .onSelectionChanged(fun args ->
                    args.AddedItems
                    |> Seq.cast<Contact>
                    |> Seq.tryHead
                    |> ContactSelected
                    |> dispatch)
        ])