[<JavaScript>]
module FSBOL.Collection
open FSBOL.TopLevel

type Collection =
    inherit TopLevel
    
    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * attachments:string list * members:string list -> Collection

    member members:string list