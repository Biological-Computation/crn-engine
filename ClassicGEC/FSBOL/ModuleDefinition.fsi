[<JavaScript>]
module FSBOL.ModuleDefinition
open FSBOL.TopLevel
open FSBOL.FunctionalComponent
open FSBOL.Interaction
open FSBOL.Role
open FSBOL.Module
open FSBOL.Model

type ModuleDefinition = 
    inherit TopLevel
    new: uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * attachments:string list * roles:Role list * modules:Module list * interactions:Interaction list * functionalComponents:FunctionalComponent list * models: Model list  -> ModuleDefinition 

    member roles:Role list

    member functionalComponents:FunctionalComponent list

    member interactions:Interaction list

    member modules:Module list
    
    member models:Model list
