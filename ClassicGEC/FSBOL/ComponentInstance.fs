[<JavaScript>]
module FSBOL.ComponentInstance
open FSBOL.Identifiers
open FSBOL.MapsTo

type Access =
    | Public
    | Private
    static member toURI (a:Access) = 
        match a with 
        | Public -> "http://sbols.org/v2#public"
        | Private -> "http://sbols.org/v2#private"
    static member fromURI (str:string) = 
        match str with 
        | "http://sbols.org/v2#public" -> Public 
        | "http://sbols.org/v2#private" -> Private
        | _ -> failwith "Unknown Access encountered. Access can only be Private or Public."

[<AbstractClass>]
type ComponentInstance(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option,definition:string, access:Access, mapsTos:MapsTo list) =
    inherit Identifiers(uri,name,displayId,version,persistantId)

    member ci.definition = definition

    member ci.access = access

    member ci.mapsTos = mapsTos

