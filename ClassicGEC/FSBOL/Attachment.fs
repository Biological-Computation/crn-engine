[<JavaScript>]
module FSBOL.Attachment
open FSBOL.TopLevel

type Attachment(uri:string, name:string option, displayId:string option, version:string option, persistentId:string option, attachments:string list, source:string, format:string option, size: int64 option, hash:string option) = 
    inherit TopLevel(uri,name,displayId,version,persistentId,attachments)

    member a.source = source

    member a.format = format

    member a.size = size

    member a.hash = hash