[<JavaScript>]
module FSBOL.Attachment
open FSBOL.TopLevel

type Attachment = 
    inherit TopLevel

    new : uri:string * name:string option * displayId:string option * version:string option * persistentId:string option * attachments:string list * source:string * format:string option * size: int64 option * hash:string option -> Attachment

    member source:string

    member format:string option

    member size:int64 option

    member hash:string option