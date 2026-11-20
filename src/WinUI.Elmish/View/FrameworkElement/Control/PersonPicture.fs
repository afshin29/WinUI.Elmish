namespace WinUI.Elmish.View

open Microsoft.UI.Xaml
open Microsoft.UI.Xaml.Controls
open Microsoft.UI.Xaml.Media
open System
open System.Runtime.CompilerServices
open Windows.ApplicationModel.Contacts

open Common
open WinUI.Elmish.IdGenerator
open WinUI.Elmish.VirtualElements
open WinUI.Elmish.EventSubscriptions
open WinUI.Elmish.Reconciler

// skipped property TemplateSettings: read-only


[<AutoOpen>]
module PersonPictureMod =

    let private personPictureNodeId = getNextId ()

    registerDefaultRenderer<PersonPicture> personPictureNodeId

    let private badgeGlyphPropertyId = getNextId ()
    let private badgeImageSourcePropertyId = getNextId ()
    let private badgeNumberPropertyId = getNextId ()
    let private badgeTextPropertyId = getNextId ()
    let private contactPropertyId = getNextId ()
    let private displayNamePropertyId = getNextId ()
    let private initialsPropertyId = getNextId ()
    let private isGroupPropertyId = getNextId ()
    let private preferSmallImagePropertyId = getNextId ()
    let private profilePicturePropertyId = getNextId ()

    [<Extension>]
    type PersonPictureExtensions =

        [<Extension>]
        static member badgeGlyph(node: VirtualNode<PersonPicture>, value: string) =
            createDependencyProperty badgeGlyphPropertyId PersonPicture.BadgeGlyphProperty value
            |> node.set

        [<Extension>]
        static member badgeImageSource(node: VirtualNode<PersonPicture>, value: ImageSource) =
            createDependencyProperty badgeImageSourcePropertyId PersonPicture.BadgeImageSourceProperty value
            |> node.set

        [<Extension>]
        static member badgeNumber(node: VirtualNode<PersonPicture>, value: int) =
            createDependencyProperty badgeNumberPropertyId PersonPicture.BadgeNumberProperty value
            |> node.set

        [<Extension>]
        static member badgeText(node: VirtualNode<PersonPicture>, value: string) =
            createDependencyProperty badgeTextPropertyId PersonPicture.BadgeTextProperty value
            |> node.set

        [<Extension>]
        static member contact(node: VirtualNode<PersonPicture>, value: Contact) =
            createDependencyProperty contactPropertyId PersonPicture.ContactProperty value
            |> node.set

        [<Extension>]
        static member displayName(node: VirtualNode<PersonPicture>, value: string) =
            createDependencyProperty displayNamePropertyId PersonPicture.DisplayNameProperty value
            |> node.set

        [<Extension>]
        static member initials(node: VirtualNode<PersonPicture>, value: string) =
            createDependencyProperty initialsPropertyId PersonPicture.InitialsProperty value
            |> node.set

        [<Extension>]
        static member isGroup(node: VirtualNode<PersonPicture>, value: bool) =
            createDependencyProperty isGroupPropertyId PersonPicture.IsGroupProperty value
            |> node.set

        [<Extension>]
        static member preferSmallImage(node: VirtualNode<PersonPicture>, value: bool) =
            createDependencyProperty preferSmallImagePropertyId PersonPicture.PreferSmallImageProperty value
            |> node.set

        [<Extension>]
        static member profilePicture(node: VirtualNode<PersonPicture>, value: ImageSource) =
            createDependencyProperty profilePicturePropertyId PersonPicture.ProfilePictureProperty value
            |> node.set

    let personPicture () =
        createNode<PersonPicture> personPictureNodeId
