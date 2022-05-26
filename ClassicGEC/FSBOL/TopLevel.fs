[<JavaScript>]
module FSBOL.TopLevel
open FSBOL.Identifiers

[<AbstractClass>]
type TopLevel(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, attachments:string list ) = 
    inherit Identifiers(uri,name,displayId,version,persistantId)

    member tl.attachments = attachments
