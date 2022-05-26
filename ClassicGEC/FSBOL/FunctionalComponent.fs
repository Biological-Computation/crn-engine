[<JavaScript>]
module FSBOL.FunctionalComponent
open FSBOL.ComponentInstance
open FSBOL.MapsTo

type Direction = 
    | In
    | Out
    | InOut
    | NoDirection //Instead of None to avoid confusion with the None keyword
    static member toURI(d:Direction) = 
        match d with 
        | In -> "http://sbols.org/v2#in"
        | Out -> "http://sbols.org/v2#out"
        | InOut -> "http://sbols.org/v2#inout"
        | NoDirection -> "http://sbols.org/v2#none"
    static member fromURI (str:string) = 
        match str with 
        | "http://sbols.org/v2#in" -> In
        | "http://sbols.org/v2#out" -> Out
        | "http://sbols.org/v2#inout" -> InOut
        | "http://sbols.org/v2#none" -> NoDirection
        | _ -> failwith "Unexpected type of Direction found. Direction must either be of type In, Out, InOut, or None."

type FunctionalComponent(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, definition:string, access:Access, mapsTos:MapsTo list,direction:Direction) = 
    inherit ComponentInstance(uri, name, displayId, version, persistantId,definition,access,mapsTos)

    member x.direction = direction


