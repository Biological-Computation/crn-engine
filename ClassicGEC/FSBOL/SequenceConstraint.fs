[<JavaScript>]
module FSBOL.SequenceConstraint
open FSBOL.Identifiers
open FSBOL.Component

type Restriction = 
   | Precedes 
   | SameOrientationAs
   | OppositeOrientationAs
   | DifferentFrom
   | OtherRestriction of string
   static member fromURI(str:string) = 
     match str with 
     | "http://sbols.org/v2#precedes" -> Precedes
     | "http://sbols.org/v2#sameOrientationAs" -> SameOrientationAs
     | "http://sbols.org/v2#oppositeOrientationAs" -> OppositeOrientationAs
     | "http://sbols.org/v2#differentFrom" -> DifferentFrom
     | _ -> OtherRestriction(str)
   static member toURI (r:Restriction) = 
     match r with 
     | Precedes -> "http://sbols.org/v2#precedes"
     | SameOrientationAs -> "http://sbols.org/v2#sameOrientationAs"
     | OppositeOrientationAs -> "http://sbols.org/v2#oppositeOrientationAs"
     | DifferentFrom -> "http://sbols.org/v2#differentFrom"
     | OtherRestriction(str) -> str

type SequenceConstraint(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, subject:Component, object:Component, restriction:Restriction) = 
    inherit Identifiers(uri, name, displayId, version, persistantId)   

    member sc.subject = subject
    
    member sc.object = object

    member sc.restriction = restriction    