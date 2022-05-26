[<JavaScript>]
module FSBOL.Role

type Role = 
    | Promoter
    | RBS
    | CDS
    | Terminator
    | Gene
    | Operator
    | EngineeredGene
    | MRNA
    | Effector
    | TranscriptionFactor
    | OtherRole of string 
    static member fromURI (str:string) = 
        match str with 
        | "http://identifiers.org/so/SO:0000167" -> Promoter
        | "http://identifiers.org/so/SO:0000139" -> RBS
        | "http://identifiers.org/so/SO:0000316" -> CDS
        | "http://identifiers.org/so/SO:0000141" -> Terminator
        | "http://identifiers.org/so/SO:0000704" -> Gene
        | "http://identifiers.org/so/SO:0000057" -> Operator
        | "http://identifiers.org/so/SO:0000280" -> EngineeredGene
        | "http://identifiers.org/so/SO:0000234" -> MRNA
        | "http://identifiers.org/chebi/CHEBI:35224" -> Effector
        | "http://identifiers.org/go/GO:0003700" -> TranscriptionFactor
        | _ -> OtherRole(str)
    static member toURI (r:Role) = 
        match r with 
        | Promoter -> "http://identifiers.org/so/SO:0000167"
        | RBS -> "http://identifiers.org/so/SO:0000139"
        | CDS -> "http://identifiers.org/so/SO:0000316"
        | Terminator -> "http://identifiers.org/so/SO:0000141"
        | Gene -> "http://identifiers.org/so/SO:0000704"
        | Operator -> "http://identifiers.org/so/SO:0000057"
        | EngineeredGene -> "http://identifiers.org/so/SO:0000280"
        | MRNA -> "http://identifiers.org/so/SO:0000234"
        | Effector -> "http://identifiers.org/chebi/CHEBI:35224"
        | TranscriptionFactor -> "http://identifiers.org/go/GO:0003700"
        | OtherRole(s) -> s
