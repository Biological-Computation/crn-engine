[<JavaScript>]
module FSBOL.Participation
open FSBOL.Identifiers
open FSBOL.FunctionalComponent

type ParticipationRole = 
    | Inhibitor
    | Inhibited
    | Stimulator
    | Stimulated
    | Reactant
    | Product
    | PromoterParticipation
    | Modifier
    | Modified
    | Template
    | OtherParticipationRole of string
    static member fromURI:string -> ParticipationRole
    static member toURI:ParticipationRole -> string

type Participation = 
    inherit Identifiers

    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * roles:ParticipationRole list * participant:FunctionalComponent -> Participation

    member roles:ParticipationRole list

    member participant:FunctionalComponent
