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
   static member fromURI: string -> Restriction
   static member toURI:Restriction -> string

type SequenceConstraint = 
    inherit Identifiers

    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * subject:Component * object:Component * restriction:Restriction -> SequenceConstraint

    member subject:Component

    member object:Component

    member restriction:Restriction
