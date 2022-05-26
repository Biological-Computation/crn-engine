[<JavaScript>]
module FSBOL.Cut
open FSBOL.Location


type Cut =
    inherit Location

    new : uri:string * name:string option * displayId:string option * version:string option  * persistantId:string option * orientation:Orientation * at:int -> Cut

    member at:int


