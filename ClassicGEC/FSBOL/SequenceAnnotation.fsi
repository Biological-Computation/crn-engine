[<JavaScript>]
module FSBOL.SequenceAnnotation
open FSBOL.Identifiers
open FSBOL.Location
open FSBOL.Component
open FSBOL.Role

type SequenceAnnotation = 
    inherit Identifiers

    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * componentObj:Component option* locations:List<Location> * roles:Role list-> SequenceAnnotation

    (* *)
    member locations:Location list

    (* *)
    member roles:Role list
    
    (* *)
    member componentObj:Component option