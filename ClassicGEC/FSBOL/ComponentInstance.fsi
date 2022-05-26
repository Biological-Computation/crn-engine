[<JavaScript>]
module FSBOL.ComponentInstance
open FSBOL.Identifiers
open FSBOL.MapsTo

type Access =
    | Public
    | Private
    static member toURI:Access -> string 
    static member fromURI:string -> Access

[<AbstractClass>]
type ComponentInstance = 
    inherit Identifiers

    new: uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * definition:string * access:Access * mapsTos:MapsTo list -> ComponentInstance
    
    member definition:string
    
    member access:Access

    member mapsTos:MapsTo list
