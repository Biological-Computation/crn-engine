[<JavaScript>]
module FSBOL.GenericLocation
open FSBOL.Location

type GenericLocation =
    inherit Location

    new : uri:string * name:string option * displayId:string option * version:string option  * persistantId:string option * orientation:Orientation  -> GenericLocation
