[<JavaScript>]
module FSBOL.Model
open FSBOL.TopLevel

type Language =
    | SBML
    | CellML
    | BioPax
    | OtherLanguage of string 
    static member toURI: Language -> string 
    static member fromURI: string -> Language

type Framework = 
    | Continuous
    | Discrete
    | OtherFramework of string 
    static member toURI:Framework -> string
    static member fromURI:string -> Framework


type Model =
    inherit TopLevel
    
    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * attachments:string list * source:string * language:Language * framework:Framework -> Model

    member source:string
    
    member language:Language

    member framework:Framework