[<JavaScript>]
module FSBOL.Model
open FSBOL.TopLevel

type Language =
    | SBML
    | CellML
    | BioPax
    | OtherLanguage of string 
    static member toURI (l:Language) =
        match l with 
        | SBML -> "http://identifiers.org/edam/format_2585"
        | CellML -> "http://identifiers.org/edam/format_3240"
        | BioPax -> "http://identifiers.org/edam/format_3156"
        | OtherLanguage(s) -> s
    static member fromURI (str:string) = 
        match str with 
        | "http://identifiers.org/edam/format_2585" -> SBML
        | "http://identifiers.org/edam/format_3240" -> CellML
        | "http://identifiers.org/edam/format_3156" -> BioPax
        | _ -> OtherLanguage(str)

type Framework = 
    | Continuous
    | Discrete
    | OtherFramework of string 
    static member toURI(f:Framework) = 
        match f with 
        | Continuous -> "http://identifiers.org/biomodels.sbo/SBO:0000062"
        | Discrete -> "http://identifiers.org/biomodels.sbo/SBO:0000063"
        | OtherFramework(s) -> s
    static member fromURI (str:string) =
        match str with 
        | "http://identifiers.org/biomodels.sbo/SBO:0000062" -> Continuous
        | "http://identifiers.org/biomodels.sbo/SBO:0000063" -> Discrete
        | _ -> OtherFramework(str)

type Model (uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, attachments:string list, source:string, language:Language, framework:Framework) =
    inherit TopLevel(uri,name,displayId,version,persistantId,attachments)
    
    member m.source = source
    
    member m.language = language

    member m.framework = framework