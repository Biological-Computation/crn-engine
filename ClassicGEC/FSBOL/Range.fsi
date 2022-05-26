[<JavaScript>]
module FSBOL.Range
open FSBOL.Location

type Range =
    inherit Location

    new : uri:string * name:string option * displayId:string option * version:string option  * persistantId:string option * orientation:Orientation * startIndex:int * endIndex:int -> Range

    member startIndex:int

    member endIndex:int
    