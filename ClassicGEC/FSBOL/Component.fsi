[<JavaScript>]
module FSBOL.Component
open FSBOL.ComponentInstance
open FSBOL.MapsTo
open FSBOL.Role

type RoleIntegration = 
    | OverrideRoles
    | MergeRoles 
    //| OtherRoleIntegration of string
    static member toURI:RoleIntegration -> string
    static member fromURI:string -> RoleIntegration

type Component = 
    inherit ComponentInstance
    
    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * definition:string * access:Access * mapsTos:MapsTo list *  roles:Role list * roleIntegrations: RoleIntegration list -> Component

    member roles:Role list

    member roleIntegrations:RoleIntegration list
    