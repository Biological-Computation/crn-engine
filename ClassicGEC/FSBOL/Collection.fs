[<JavaScript>]
module FSBOL.Collection
open FSBOL.TopLevel


type Collection(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option,attachments:string list, members:string list) =
    inherit TopLevel(uri,name,displayId,version,persistantId,attachments)
    
    member c.members = members
