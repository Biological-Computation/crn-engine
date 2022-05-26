[<JavaScript>]
module FSBOL.MapsTo
open FSBOL.Identifiers


type Refinement =
    | UseRemote
    | UseLocal
    | VerifyIdentical
    | Merge
    | OtherRefinement of string
    static member toURI:Refinement -> string
    static member fromURI:string -> Refinement

type MapsTo = 
    inherit Identifiers

    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * local:string * remote:string * refinement:Refinement -> MapsTo

    member local:string 

    member remote:string 

    member refinement:Refinement