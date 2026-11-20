module internal WinUI.Elmish.ViewState

open VirtualElements

open Microsoft.UI.Xaml


let mutable currentVirtualNode = Unchecked.defaultof<VirtualWindow>
        
let mutable mainWindow: Window = null


let synDispatch dispatch =
    let dispatcherQueue =
        Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread()

    fun msg ->
        if dispatcherQueue.HasThreadAccess then
            dispatch msg
        else
            dispatcherQueue.TryEnqueue(fun () -> dispatch msg) |> ignore


let setState dispatch virtualNode =
    if mainWindow = null then
        currentVirtualNode <- virtualNode

        mainWindow <- Reconciler.renderWindow currentVirtualNode
        mainWindow.Activate()

    else
        let diff = Diff.diffWindow currentVirtualNode virtualNode
        currentVirtualNode <- virtualNode
        Patcher.applyWindowDiff mainWindow diff