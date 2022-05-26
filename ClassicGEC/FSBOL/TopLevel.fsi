[<JavaScript>]
module FSBOL.TopLevel
open FSBOL.Identifiers

[<AbstractClass>]
type TopLevel = 
    inherit Identifiers
    new: uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * attachments:string list -> TopLevel
    
    member attachments:string list
