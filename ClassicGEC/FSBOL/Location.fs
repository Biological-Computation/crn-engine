[<JavaScript>]
module FSBOL.Location
open FSBOL.Identifiers


type Orientation = 
    | Inline
    | ReverseComplement
    | OtherOrientation of string
    static member toURI (o:Orientation) = 
        match o with 
        | Inline -> "http://sbolstandard.org/#inline"
        | ReverseComplement -> "http://sbolstandard.org/#reverseComplement"
        | OtherOrientation(str) -> str
    static member fromURI(str:string) = 
        match str with 
        | "http://sbolstandard.org/#inline" -> Inline
        | "http://sbolstandard.org/#reverseComplement" -> ReverseComplement
        | _ -> OtherOrientation(str)

[<AbstractClass>]
type Location(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, orientation:Orientation) =
    inherit Identifiers(uri,name,displayId,version,persistantId)

    member l.orientation = orientation

