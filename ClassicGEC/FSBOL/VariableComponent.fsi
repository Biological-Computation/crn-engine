[<JavaScript>]
module FSBOL.VariableComponent
open FSBOL.Identifiers

type Operator = 
    | ZeroOrOne
    | One
    | ZeroOrMore
    | OneOrMore
    static member toURI:Operator -> string
    static member fromURI:string -> Operator


type VariableComponent = 
    inherit Identifiers

    new: uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * operator:Operator * variants:string list * variantCollections:string list * variantDerivations:string list * variable:string-> VariableComponent

    member operator:Operator

    member variants:string list

    member variantCollections:string list

    member variantDerivations:string list

    member variable:string
