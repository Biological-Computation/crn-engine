[<JavaScript>]
module FSBOL.Module
open FSBOL.Identifiers
open FSBOL.MapsTo

type Module =
    inherit Identifiers
    
    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * definition:string * mapsTos:MapsTo list  -> Module

    member definition:string

    member mapsTos:MapsTo list