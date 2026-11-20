module internal WinUI.Elmish.EventSubscriptions

open Microsoft.UI.Xaml
open System
open System.Collections.Generic
open System.Runtime.CompilerServices


let private elementSubscriptions =
    ConditionalWeakTable<UIElement, Dictionary<string, IDisposable>>()


let trackSubscription (uiElement: UIElement) eventName (subscription: IDisposable) =
    let subscriptions = elementSubscriptions.GetOrCreateValue(uiElement)
    subscriptions[eventName] <- subscription


let disposeSubscription (uiElement: UIElement) eventName =
    match elementSubscriptions.TryGetValue(uiElement) with
    | true, subscriptions ->
        match subscriptions.Remove<_, _>(eventName) with
        | true, subscription -> subscription.Dispose()
        | _ -> ()
    | _ -> ()