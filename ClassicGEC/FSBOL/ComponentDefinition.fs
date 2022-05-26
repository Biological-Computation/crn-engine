[<JavaScript>]
module FSBOL.ComponentDefinition
open FSBOL.TopLevel
open FSBOL.Component
open FSBOL.SequenceAnnotation
open FSBOL.SequenceConstraint
open FSBOL.Sequence
open FSBOL.Role

type ComponentDefinitionType = 
    | DNA
    | RNA
    | Protein
    | SmallMolecule
    | Complex
    | Linear
    | Circular
    | SingleStranded
    | DoubleStranded
    | OtherType of string
    static member fromURI (str:string) = 
        match str with 
        | "http://www.biopax.org/release/biopax-level3.owl#DnaRegion" -> DNA
        | "http://www.biopax.org/release/biopax-level3.owl#RnaRegion" -> RNA
        | "http://www.biopax.org/release/biopax-level3.owl#Protein" -> Protein
        | "http://www.biopax.org/release/biopax-level3.owl#SmallMolecule" -> SmallMolecule 
        | "http://www.biopax.org/release/biopax-level3.owl#Complex" -> Complex
        | "http://identifiers.org/so/SO:0000987" -> Linear
        | "http://identifiers.org/so/SO:0000988" -> Circular
        | "http://identifiers.org/so/SO:0000984" -> SingleStranded 
        | "http://identifiers.org/so/SO:0000985" -> DoubleStranded
        | _ -> OtherType(str)
    static member toURI (t:ComponentDefinitionType) = 
        match t with 
        | DNA -> "http://www.biopax.org/release/biopax-level3.owl#DnaRegion"
        | RNA -> "http://www.biopax.org/release/biopax-level3.owl#RnaRegion"
        | Protein -> "http://www.biopax.org/release/biopax-level3.owl#Protein"
        | SmallMolecule -> "http://www.biopax.org/release/biopax-level3.owl#SmallMolecule"
        | Complex -> "http://www.biopax.org/release/biopax-level3.owl#Complex"
        | Linear -> "http://identifiers.org/so/SO:0000987"
        | Circular -> "http://identifiers.org/so/SO:0000988"
        | SingleStranded -> "http://identifiers.org/so/SO:0000984"
        | DoubleStranded -> "http://identifiers.org/so/SO:0000985"
        | OtherType(s) -> s

type ComponentDefinition (uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, attachments:string list,types:ComponentDefinitionType list, roles:Role list, sequences:Sequence list, components:Component list, sequenceAnnotations:SequenceAnnotation list, sequenceConstraints:SequenceConstraint list) = 
    inherit TopLevel(uri, name, displayId, version, persistantId,attachments)

    member x.roles = roles

    member x.types = types

    member x.sequences = sequences

    member x.sequenceAnnotations = sequenceAnnotations
    
    member x.sequenceConstraints = sequenceConstraints

    member x.components = components

    

    