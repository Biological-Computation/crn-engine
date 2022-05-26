[<JavaScript>]
module FSBOL.QualifiedName

let sbolQN = "xmlns:sbol"
let rdfQN = "rdf:RDF"
let provQN = "xmlns:prov"
let dctermsQN = "xmlns:dcterms"
let ns0QN = "xmlns:ns0"

let id = "rdf:about"
let displayId = "sbol:displayId"
let version = "sbol:version"
let persistentIdentity ="sbol:persistentIdentity"
let name = "dcterms:title"
let description = "dcterms:description"


let atProperty = "sbol:at"
let startIndexProperty = "sbol:start"
let endIndexProperty = "sbol:end"
let orientationProperty = "sbol:orientation"
let elementsProperty = "sbol:elements"
let encodingProperty = "sbol:encoding"
let roleProperty = "sbol:role"
let roleIntegrationProperty = "sbol:roleIntegration"
let typeProperty = "sbol:type"
let directionProperty = "sbol:direction"
let participantProperty = "sbol:participant"
let sourceProperty = "sbol:source"
let formatProperty = "sbol:format"
let sizeProperty = "sbol:size"
let hashProperty = "sbol:hash"
let memberProperty = "sbol:member"
let attachmentProperty = "sbol:attachment"
let refinementProperty = "sbol:refinement"
let localProperty = "sbol:local"
let remoteProperty = "sbol:remote"
let locationProperty = "sbol:location"
let sequenceAnnotationProperty = "sbol:sequenceAnnotation"
let sequenceConstraintsProperty = "sbol:sequenceConstraint"
let componentProperty = "sbol:component"
let sequenceProperty = "sbol:sequence"
let componentDefinitionProperty = "sbol:componentDefinition"
let functionalComponentProperty = "sbol:functionalComponent"
let participationProperty = "sbol:participation"
let interactionProperty = "sbol:interaction"
let moduleDefinitionProperty = "sbol:moduleDefinition"
let mapsToProperty = "sbol:mapsTo"
let definitionProperty = "sbol:definition"
let accessProperty = "sbol:access"
let subjectProperty = "sbol:subject"
let objectProperty = "sbol:object"
let restrictionProperty = "sbol:restriction"
let variableProperty = "sbol:variable"
let variantCollectionProperty = "sbol:variantCollection"
let variantDerivationProperty = "sbol:variantDerivation"
let variantProperty = "sbol:variant"
let strategyProperty = "sbol:strategy"
let templateProperty = "sbol:template"
let variableComponentProperty = "sbol:variableComponent"
let languageProperty = "sbol:language"
let frameworkProperty = "sbol:framework"
let moduleProperty = "sbol:module"
let modelProperty = "sbol:model"
let builtProperty = "sbol:built"
let operatorProperty = "sbol:operator"

let Range = "sbol:Range"
let SequenceAnnotation = "sbol:SequenceAnnotation"
let Component = "sbol:Component"
let Sequence = "sbol:Sequence"
let Attachment = "sbol:Attachment"
let Collection = "sbol:Collection"
let MapsTo = "sbol:MapsTo"
let Location = "sbol:Location"
let Cut = "sbol:Cut"
let GenericLocation = "sbol:GenericLocation"
let SequenceConstraint = "sbol:SequenceConstraint"
let VariableComponent = "sbol:VariableComponent"
let CombinatorialDerivation = "sbol:CombinatorialDerivation"
let Model = "sbol:Model"
let Module = "sbol:Module"
let Implementation = "sbol:Implementation"
let ComponentDefinition = "sbol:ComponentDefinition"
let FunctionalComponent = "sbol:FunctionalComponent"
let Participation = "sbol:Participation"
let Interaction = "sbol:Interaction"
let ModuleDefinition = "sbol:ModuleDefinition"



let allKnownQNames = [id        ;
    displayId                   ;
    version                     ;
    persistentIdentity          ;
    name                        ;
    description                 ;
    atProperty                  ;
    startIndexProperty          ;
    endIndexProperty            ;
    orientationProperty         ;
    elementsProperty            ;
    encodingProperty            ;
    roleProperty                ;
    roleIntegrationProperty     ;
    typeProperty                ;
    directionProperty           ;
    participantProperty         ;
    sourceProperty              ;
    formatProperty              ;
    sizeProperty                ;
    hashProperty                ;
    memberProperty              ;
    attachmentProperty          ;
    refinementProperty          ;
    localProperty               ;
    remoteProperty              ;
    locationProperty            ;
    sequenceAnnotationProperty  ;
    sequenceConstraintsProperty ;
    componentProperty           ;
    sequenceProperty            ;
    componentDefinitionProperty ;
    functionalComponentProperty ;
    participationProperty       ;
    interactionProperty         ;
    moduleDefinitionProperty    ;
    mapsToProperty              ;
    definitionProperty          ;
    accessProperty              ;
    subjectProperty             ;
    objectProperty              ;
    restrictionProperty         ;
    variableProperty            ;
    variantCollectionProperty   ;
    variantDerivationProperty   ;
    variantProperty             ;
    strategyProperty            ;
    templateProperty            ;
    variableComponentProperty   ;
    languageProperty            ;
    frameworkProperty           ;
    moduleProperty              ;
    modelProperty               ;
    builtProperty               ;
    operatorProperty            ;                                
    Range                       ;
    SequenceAnnotation          ;
    Component                   ;
    Sequence                    ;
    Attachment                  ;
    Collection                  ;
    MapsTo                      ;
    Location                    ;
    Cut                         ;
    GenericLocation             ;
    SequenceConstraint          ;
    VariableComponent           ;
    CombinatorialDerivation     ;
    Model                       ;
    Module                      ;
    Implementation              ;
    ComponentDefinition         ;
    FunctionalComponent         ;
    Participation               ;
    Interaction                 ;
    ModuleDefinition]



let isKnownQName (x:string) = 
    allKnownQNames |> List.contains x