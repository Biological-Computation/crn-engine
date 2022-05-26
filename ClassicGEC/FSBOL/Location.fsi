[<JavaScript>]
module FSBOL.Location
open FSBOL.Identifiers

type Orientation = 
    | Inline
    | ReverseComplement
    | OtherOrientation of string
    static member toURI:Orientation -> string
    static member fromURI:string -> Orientation

[<AbstractClass>]
type Location = 
    inherit Identifiers
    new: uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * orientation:Orientation -> Location

    member orientation:Orientation
