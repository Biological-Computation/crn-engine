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
    static member fromURI(str:string) =
        match str with 
        | "http://identifiers.org/biomodels.sbo/SBO:0000020" -> Inhibitor
        | "http://identifiers.org/biomodels.sbo/SBO:0000642" -> Inhibited
        | "http://identifiers.org/biomodels.sbo/SBO:0000459" -> Stimulator
        | "http://identifiers.org/biomodels.sbo/SBO:0000643" -> Stimulated
        | "http://identifiers.org/biomodels.sbo/SBO:0000010" -> Reactant
        | "http://identifiers.org/biomodels.sbo/SBO:0000011" -> Product
        | "http://identifiers.org/biomodels.sbo/SBO:0000598" -> PromoterParticipation
        | "http://identifiers.org/biomodels.sbo/SBO:0000019" -> Modifier
        | "http://identifiers.org/biomodels.sbo/SBO:0000644" -> Modified
        | "http://identifiers.org/biomodels.sbo/SBO:0000645" -> Template
        | _ -> OtherParticipationRole(str)
    static member toURI (pr:ParticipationRole) =
        match pr with
        | Inhibitor -> "http://identifiers.org/biomodels.sbo/SBO:0000020"
        | Inhibited -> "http://identifiers.org/biomodels.sbo/SBO:0000642"
        | Stimulator -> "http://identifiers.org/biomodels.sbo/SBO:0000459"
        | Stimulated -> "http://identifiers.org/biomodels.sbo/SBO:0000643"
        | Reactant -> "http://identifiers.org/biomodels.sbo/SBO:0000010"
        | PromoterParticipation -> "http://identifiers.org/biomodels.sbo/SBO:0000598"
        | Product -> "http://identifiers.org/biomodels.sbo/SBO:0000011"
        | Modifier -> "http://identifiers.org/biomodels.sbo/SBO:0000019"
        | Modified -> "http://identifiers.org/biomodels.sbo/SBO:0000644"
        | Template -> "http://identifiers.org/biomodels.sbo/SBO:0000645"
        | OtherParticipationRole(s) -> s


type Participation(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, roles:ParticipationRole list, participant:FunctionalComponent ) =
    inherit Identifiers(uri, name, displayId, version, persistantId)

    member x.roles = roles

    member x.participant = participant

