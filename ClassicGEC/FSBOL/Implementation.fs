[<JavaScript>]
module FSBOL.Implementation
open FSBOL.TopLevel
open FSBOL.ModuleDefinition
open FSBOL.ComponentDefinition

type Built = 
    | CD of ComponentDefinition
    | MD of ModuleDefinition

type Implementation(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, attachments:string list, built: Built option)= 
    inherit TopLevel(uri,name,displayId,version,persistantId,attachments)

    member i.built = built