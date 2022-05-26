[<JavaScript>]
module FSBOL.VariableComponent
open FSBOL.Identifiers

type Operator = 
    | ZeroOrOne
    | One
    | ZeroOrMore
    | OneOrMore
    static member toURI(op:Operator) = 
        match op with 
        | ZeroOrOne -> "http://sbols.org/v2#zeroOrOne"
        | One -> "http://sbols.org/v2#one"
        | ZeroOrMore -> "http://sbols.org/v2#zeroOrMore"
        | OneOrMore -> "http://sbols.org/v2#oneOrMore"
    static member fromURI(str:string) =
        match str with 
        | "http://sbols.org/v2#zeroOrOne" -> ZeroOrOne
        | "http://sbols.org/v2#one" -> One
        | "http://sbols.org/v2#zeroOrMore" -> ZeroOrMore
        | "http://sbols.org/v2#oneOrMore" -> OneOrMore
        | _ -> failwith "Unknown Operator found. Operator must be ZeroOrOne, One, ZeroOrMore, or OneOrMore"

type VariableComponent(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, operator:Operator, variants:string list, variantCollections:string list, variantDerivations:string list, variable:string) = 
    inherit Identifiers(uri,name,displayId,version,persistantId)

    member vc.operator = operator

    member vc.variants = variants

    member vc.variantCollections = variantCollections

    member vc.variantDerivations = variantDerivations

    member vc.variable = variable