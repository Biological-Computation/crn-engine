[<JavaScript>]
module FSBOL.MapsTo
open FSBOL.Identifiers


type Refinement =
    | UseRemote
    | UseLocal
    | VerifyIdentical
    | Merge
    | OtherRefinement of string
    static member toURI(r:Refinement) =
      match r with 
      | UseRemote -> "http://sbolstandard.org/#useRemote"
      | UseLocal -> "http://sbolstandard.org/#useLocal"
      | VerifyIdentical -> "http://sbolstandard.org/#verifyIdentical"
      | Merge -> "http://sbolstandard.org/#merge"
      | OtherRefinement(str) -> str
    static member fromURI(str:string) = 
      match str with 
      | "http://sbolstandard.org/#useRemote" -> UseRemote
      | "http://sbolstandard.org/#useLocal" -> UseLocal
      | "http://sbolstandard.org/#verifyIdentical" -> VerifyIdentical
      | "http://sbolstandard.org/#merge" -> Merge
      | _ -> OtherRefinement(str)

type MapsTo(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, local:string, remote:string, refinement:Refinement) =
    inherit Identifiers(uri,name,displayId,version,persistantId)

    (* *)
    member x.local = local
    
    (* *)
    member x.remote = remote

    (* *)
    member x.refinement = refinement
