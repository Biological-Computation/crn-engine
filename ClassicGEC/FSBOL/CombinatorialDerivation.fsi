[<JavaScript>]
module FSBOL.CombinatorialDerivation
open FSBOL.TopLevel
open FSBOL.VariableComponent

type Strategy = 
    | Enumerate
    | Sample
    static member fromURI:string -> Strategy
    static member toURI:Strategy -> string

type CombinatorialDerivation = 
    inherit TopLevel
    new: uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * attachments:string list * strategy: Strategy option * template:string * variableComponents:VariableComponent list-> CombinatorialDerivation

    member strategy:Strategy option
    
    member template:string 

    member variableComponents:VariableComponent list
