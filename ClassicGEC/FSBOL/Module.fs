[<JavaScript>]
module FSBOL.Module
open FSBOL.Identifiers
open FSBOL.MapsTo

type Module(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, definition:string, mapsTos:MapsTo list) =
    inherit Identifiers(uri,name,displayId,version,persistantId)
    
    member m.mapsTos = mapsTos
    member m.definition = definition