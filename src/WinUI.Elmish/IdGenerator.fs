module WinUI.Elmish.IdGenerator

type NodeId = int
type PropertyId = int

let mutable private _nextId = 0

let internal getNextId () : NodeId =
    let id = _nextId
    _nextId <- _nextId + 1
    id