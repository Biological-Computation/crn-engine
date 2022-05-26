[<JavaScript>]
module FSBOL.FunctionalComponent
open FSBOL.ComponentInstance
open FSBOL.MapsTo

type Direction = 
    | In
    | Out
    | InOut
    | NoDirection
    static member toURI:Direction -> string 
    static member fromURI:string -> Direction 

type FunctionalComponent =
    inherit ComponentInstance
    
    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * definition:string * access:Access * mapsTos:MapsTo list * direction:Direction  -> FunctionalComponent

    member direction:Direction