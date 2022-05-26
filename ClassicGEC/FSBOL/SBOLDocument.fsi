[<JavaScript>]
module FSBOL.SBOLDocument
open FSBOL.ComponentDefinition
open FSBOL.ModuleDefinition
open FSBOL.Sequence
open FSBOL.Attachment
open FSBOL.Model
open FSBOL.Implementation
open FSBOL.Collection
open FSBOL.CombinatorialDerivation
open FSBOL.TopLevel

type SBOLDocument = 
    new: collection:TopLevel list -> SBOLDocument

    member attachments:Attachment list

    member sequences:Sequence list

    member componentDefinitions:ComponentDefinition list

    member moduleDefinitions:ModuleDefinition list

    member models:Model list

    member implementations:Implementation list

    member collections:Collection list

    member combinatorialDerivations: CombinatorialDerivation list
