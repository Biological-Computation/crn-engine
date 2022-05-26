[<JavaScript>]
module FSBOL.Implementation
open FSBOL.TopLevel
open FSBOL.ComponentDefinition
open FSBOL.ModuleDefinition


type Built = 
    | CD of ComponentDefinition
    | MD of ModuleDefinition

type Implementation = 
    inherit TopLevel

    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * attachments:string list * built: Built option -> Implementation

    member built:Built option