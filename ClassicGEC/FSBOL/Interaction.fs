[<JavaScript>]
module FSBOL.Interaction
open FSBOL.Identifiers
open FSBOL.Participation

type InteractionType = 
    | Inhibition
    | Stimulation
    | BiochemicalReaction
    | NonCovalentBinding
    | Degradation
    | GeneticProduction
    | Control
    | OtherInteractionType of string
    static member fromURI(str:string) =
        match str with 
        | "http://identifiers.org/biomodels.sbo/SBO:0000169" -> Inhibition
        | "http://identifiers.org/biomodels.sbo/SBO:0000170" -> Stimulation
        | "http://identifiers.org/biomodels.sbo/SBO:0000176" -> BiochemicalReaction
        | "http://identifiers.org/biomodels.sbo/SBO:0000177" -> NonCovalentBinding
        | "http://identifiers.org/biomodels.sbo/SBO:0000179" -> Degradation 
        | "http://identifiers.org/biomodels.sbo/SBO:0000589" -> GeneticProduction
        | "http://identifiers.org/biomodels.sbo/SBO:0000168" -> Control
        | _ -> OtherInteractionType(str)
    static member toURI(it:InteractionType) =
        match it with 
        | Inhibition -> "http://identifiers.org/biomodels.sbo/SBO:0000169"
        | Stimulation -> "http://identifiers.org/biomodels.sbo/SBO:0000170"
        | BiochemicalReaction -> "http://identifiers.org/biomodels.sbo/SBO:0000176"
        | NonCovalentBinding -> "http://identifiers.org/biomodels.sbo/SBO:0000177"
        | Degradation -> "http://identifiers.org/biomodels.sbo/SBO:0000179"
        | GeneticProduction -> "http://identifiers.org/biomodels.sbo/SBO:0000589"
        | Control -> "http://identifiers.org/biomodels.sbo/SBO:0000168"
        | OtherInteractionType(s) -> s

type Interaction(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, types:InteractionType list, participations:Participation list) =
    inherit Identifiers(uri, name, displayId, version, persistantId)

    member x.types = types

    member x.participations = participations
