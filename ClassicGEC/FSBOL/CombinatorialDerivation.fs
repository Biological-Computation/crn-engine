[<JavaScript>]
module FSBOL.CombinatorialDerivation
open FSBOL.TopLevel
open FSBOL.VariableComponent

type Strategy = 
    | Enumerate
    | Sample
    static member fromURI(str:string) =
        match str with 
        | "http://sbols.org/v2#enumerate" -> Enumerate
        | "http://sbols.org/v2#sample" -> Sample
        | _ -> failwith "Unknown Strategy Found. Strategy can only be Enumerate or Sample."
    static member toURI(st:Strategy) =
        match st with 
        | Enumerate -> "http://sbols.org/v2#enumerate"
        | Sample -> "http://sbols.org/v2#sample"

type CombinatorialDerivation(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, attachments:string list, strategy: Strategy option, template:string, variableComponents:VariableComponent list) = 
    inherit TopLevel(uri,name,displayId,version,persistantId,attachments)

    member vc.strategy = strategy
    
    member vc.template = template

    member vc.variableComponents = variableComponents
  