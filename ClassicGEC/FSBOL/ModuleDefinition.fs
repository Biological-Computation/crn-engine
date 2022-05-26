[<JavaScript>]
module FSBOL.ModuleDefinition
open FSBOL.TopLevel
open FSBOL.FunctionalComponent
open FSBOL.Interaction
open FSBOL.Role
open FSBOL.Model
open FSBOL.Module

type ModuleDefinition(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, attachments:string list, roles:Role list, modules:Module list, interactions:Interaction list, functionalComponents:FunctionalComponent list, models: Model list) =
    inherit TopLevel(uri, name, displayId, version, persistantId,attachments)

    member md.functionalComponents = functionalComponents

    member md.interactions = interactions

    member md.roles = roles

    member md.modules = modules

    member md.models = models


