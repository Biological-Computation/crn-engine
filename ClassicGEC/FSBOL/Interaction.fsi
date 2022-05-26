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
    static member fromURI:string -> InteractionType
    static member toURI:InteractionType -> string

type Interaction = 
    inherit Identifiers

    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * types:InteractionType list * participations:Participation list -> Interaction

    member types:InteractionType list

    member participations:Participation list
