module FSBOL.XmlSerializer

open FSBOL.Annotation
open FSBOL.Identifiers
open FSBOL.Sequence
open FSBOL.Attachment
open FSBOL.Collection
open FSBOL.MapsTo
open FSBOL.ComponentInstance
open FSBOL.Component
open FSBOL.Role
open FSBOL.Location
open FSBOL.Range
open FSBOL.Cut
open FSBOL.GenericLocation
open FSBOL.SequenceAnnotation
open FSBOL.SequenceConstraint
open FSBOL.ComponentDefinition
open FSBOL.VariableComponent
open FSBOL.CombinatorialDerivation
open FSBOL.Model
open FSBOL.Module
open FSBOL.FunctionalComponent
open FSBOL.Participation
open FSBOL.Interaction
open FSBOL.ModuleDefinition
open FSBOL.Implementation
open FSBOL.TopLevel
open FSBOL.SBOLDocument

open System
open System.Xml
open System.IO
open System.Text

/// To XML string
let sbol_to_XmlString (xdoc:XmlDocument) = 
    let sw = new StringWriter()
    let xwSettings = new XmlWriterSettings()
    xwSettings.Indent <- true
    xwSettings.Encoding <- Encoding.UTF8
    let xw = XmlWriter.Create(sw,xwSettings)
    (xdoc).WriteTo(xw)
    xw.Close()
    sw.ToString()


let identifiersToXml (xmlElement:XmlElement) (xdoc:XmlDocument) (id:Identifiers)= 
    
    xmlElement.SetAttribute("about",Terms.rdfns,id.uri) |> ignore
    

    
    (* Version *)
    match id.version with 
    | Some(ver) -> 
        let verXml = xdoc.CreateElement(QualifiedName.version,Terms.sbolns)
        verXml.AppendChild(xdoc.CreateTextNode(ver)) |> ignore
        xmlElement.AppendChild(verXml) |> ignore
    | None -> ()
    
    (* Name *)
    match id.name with 
    | Some(n) ->  
        let nameXml = xdoc.CreateElement(QualifiedName.name,Terms.dctermsns)
        nameXml.AppendChild(xdoc.CreateTextNode(n)) |> ignore
        xmlElement.AppendChild(nameXml) |> ignore
    | None -> ()
    
    
    (* Display Id*)
    match id.displayId with 
    | Some(display) -> 
        let disIdXml = xdoc.CreateElement(QualifiedName.displayId,Terms.sbolns)
        disIdXml.AppendChild(xdoc.CreateTextNode(display)) |> ignore
        xmlElement.AppendChild(disIdXml) |> ignore
    | None -> ()
    
    
    (* Persistent Identity*)
    match id.persistentIdentity with 
    | Some(pid) -> 
        let perIdXml = xdoc.CreateElement(QualifiedName.persistentIdentity,Terms.sbolns)
        perIdXml.SetAttribute("resource",Terms.rdfns,pid) |> ignore
        xmlElement.AppendChild(perIdXml)  |> ignore
    | None -> ()
    
    (* Description *)
    match id.description with 
    | Some(desc) -> 
        let descriptionXml = xdoc.CreateElement(QualifiedName.description,Terms.dctermsns)
        descriptionXml.AppendChild(xdoc.CreateTextNode(desc)) |> ignore
        xmlElement.AppendChild(descriptionXml) |> ignore
    | None -> ()
    
    let rec createAnnotation (xdoc:XmlDocument) (ann:Annotation) = 
        
        let create_qname_element (xdoc:XmlDocument) (qName:QName) = 
            match qName with
            | Name(n) -> xdoc.CreateElement(n)
            | QualifiedName(qn,nsURI) -> xdoc.CreateElement(qn,nsURI)
            | FullQName(prefix,lcname,nsURI) -> xdoc.CreateElement(prefix,lcname,nsURI)
    
        let annXml = create_qname_element xdoc ann.qName
        
        match ann.value with 
        | Literal (lit) -> annXml.AppendChild(xdoc.CreateTextNode(Literal.to_string lit)) |> ignore
        | Uri (uri) -> annXml.SetAttribute("resource",Terms.rdfns,uri) |> ignore
        | NestedAnnotation(na) -> 
            let nestedElem = create_qname_element xdoc na.nestedQName
            nestedElem.SetAttribute("resource",Terms.rdfns,na.nestedURI) |> ignore
            let nestAnnList = na.annotations |> List.map (fun nann -> createAnnotation xdoc nann)
            nestAnnList |> List.iter (fun y -> nestedElem.AppendChild(y) |> ignore)
            annXml.AppendChild(nestedElem) |> ignore
            
        annXml
    
    match id.annotations with 
    | [] -> ()
    | _ -> 
        let annList = id.annotations |> List.map (fun ann -> createAnnotation xdoc ann)
        annList |> List.iter (fun y -> xmlElement.AppendChild(y) |> ignore)

    

    
    
let topLevelToXml (xmlElement:XmlElement) (xdoc:XmlDocument) (toplevel:TopLevel)= 
    identifiersToXml xmlElement xdoc toplevel
    let attachmentToXML (xmlElement:XmlElement) (xdoc:XmlDocument) (attachmenturi:string) = 
        let attachmentXml = xdoc.CreateElement(QualifiedName.attachmentProperty,Terms.sbolns)
        attachmentXml.SetAttribute("resource",Terms.rdfns,attachmenturi) |> ignore
        xmlElement.AppendChild(attachmentXml) |> ignore
    toplevel.attachments |> List.iter (fun x -> attachmentToXML xmlElement xdoc x)
/// Serialize AttachmentProperty 
    

/// Serialize Sequence to XML
let sequenceToXml (xdoc:XmlDocument) (x:Sequence)=         
    let xmlElement = xdoc.CreateElement(QualifiedName.Sequence,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x

    (* Sequence *)
    let elementsXml = xdoc.CreateElement(QualifiedName.elementsProperty,Terms.sbolns)
    elementsXml.AppendChild(xdoc.CreateTextNode(x.elements)) |> ignore
    xmlElement.AppendChild(elementsXml) |> ignore

    (* Sequence Encoding *)
    let encodingXml = xdoc.CreateElement(QualifiedName.encodingProperty,Terms.sbolns)
    encodingXml.SetAttribute("resource",Terms.rdfns,Encoding.toURI (x.encoding)) |> ignore
    xmlElement.AppendChild(encodingXml) |> ignore

    xmlElement

/// Serialize Attachment to XML
let attachmentToXml (xdoc:XmlDocument) (x:Attachment) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Attachment,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x
    
    (* Source *)
    let sourceXml = xdoc.CreateElement(QualifiedName.sourceProperty,Terms.sbolns)
    sourceXml.SetAttribute("resource",Terms.rdfns,x.source) |> ignore
    xmlElement.AppendChild(sourceXml) |> ignore

    (* Format *)
    match x.format with 
    | Some(f) -> 
        let formatXml = xdoc.CreateElement(QualifiedName.formatProperty,Terms.sbolns)
        formatXml.SetAttribute("resource",Terms.rdfns,f) |> ignore
        xmlElement.AppendChild(formatXml) |> ignore
    | None -> ()
    

    (* Size *)
    match x.size with 
    | Some(s) -> 
        let sizeXml = xdoc.CreateElement(QualifiedName.sizeProperty,Terms.sbolns)
        sizeXml.AppendChild(xdoc.CreateTextNode(s.ToString())) |> ignore
        xmlElement.AppendChild(sizeXml) |> ignore
    | None -> ()

    (* Hash *)
    match x.hash with 
    | Some(h) -> 
        let hashXml = xdoc.CreateElement(QualifiedName.hashProperty,Terms.sbolns)
        hashXml.AppendChild(xdoc.CreateTextNode(h)) |> ignore
        xmlElement.AppendChild(hashXml) |> ignore
    | None -> ()

    xmlElement


/// Serialize Collection to XML
let collectionToXml (xdoc:XmlDocument) (x:Collection) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Collection,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x

    (* Members *)
    let memberToXml (xmlElement:XmlElement) (xdoc:XmlDocument) (memberuri:string) = 
        let memberXml = xdoc.CreateElement(QualifiedName.memberProperty,Terms.sbolns)
        memberXml.SetAttribute("resource",Terms.rdfns,memberuri) |> ignore
        xmlElement.AppendChild(memberXml) |> ignore
    
    x.members |> List.iter (fun m -> memberToXml xmlElement xdoc m)

    xmlElement

/// Serialize MapsTo to XML
let mapsToToXml (xdoc:XmlDocument) (x:MapsTo) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.MapsTo,Terms.sbolns)
    
    identifiersToXml xmlElement xdoc x
    
    (* Refinement *)
    let refinementXml = xdoc.CreateElement(QualifiedName.refinementProperty,Terms.sbolns)
    refinementXml.SetAttribute("resource",Terms.rdfns,Refinement.toURI x.refinement) |> ignore
    xmlElement.AppendChild(refinementXml) |> ignore

    (* Local *)
    let localXml = xdoc.CreateElement(QualifiedName.localProperty,Terms.sbolns)
    localXml.SetAttribute("resource",Terms.rdfns,x.local) |> ignore
    xmlElement.AppendChild(localXml) |> ignore

    (* Remote *)
    let remoteXml = xdoc.CreateElement(QualifiedName.remoteProperty,Terms.sbolns)
    remoteXml.SetAttribute("resource",Terms.rdfns,x.remote) |> ignore
    xmlElement.AppendChild(remoteXml) |> ignore

    xmlElement

/// Serialize ComponentInstance  to XML
let componentInstanceToXml (xmlElement:XmlElement) (xdoc:XmlDocument) (x:ComponentInstance) = 
    
    identifiersToXml xmlElement xdoc x
    
    (*Maps Tos*)
    x.mapsTos |> List.iter (fun mt ->
        let mapsToXml = xdoc.CreateElement(QualifiedName.mapsToProperty,Terms.sbolns)
        mapsToXml.AppendChild(mapsToToXml xdoc mt) |> ignore
        xmlElement.AppendChild(mapsToXml) |> ignore
        )

    (*Definition*)
    let definitionXml = xdoc.CreateElement(QualifiedName.definitionProperty,Terms.sbolns)
    definitionXml.SetAttribute("resouce",Terms.rdfns,x.definition) |> ignore
    xmlElement.AppendChild(definitionXml) |> ignore

    (*Access*)
    let accessXml = xdoc.CreateElement(QualifiedName.accessProperty,Terms.sbolns)
    accessXml.SetAttribute("resouce",Terms.rdfns,Access.toURI x.access) |> ignore
    xmlElement.AppendChild(accessXml) |> ignore

let roleToXml (xmlElement:XmlElement) (xdoc:XmlDocument) (role:Role) =
    let roleXml = xdoc.CreateElement(QualifiedName.roleProperty,Terms.sbolns)
    roleXml.SetAttribute("resource",Terms.rdfns,Role.toURI role) |> ignore
    xmlElement.AppendChild(roleXml) |> ignore

/// Serialize Component to XML
let componentToXml (xdoc:XmlDocument) (x:Component) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Component,Terms.sbolns)
    
    componentInstanceToXml xmlElement xdoc x 
    
    (* Roles *)
    x.roles |> List.iter (fun r -> roleToXml xmlElement xdoc r)        

    x.roleIntegrations |> List.iter (fun ri -> 
        let riXML = xdoc.CreateElement(QualifiedName.roleIntegrationProperty,Terms.sbolns)
        riXML.SetAttribute("resource",Terms.rdfns,RoleIntegration.toURI ri) |> ignore
        xmlElement.AppendChild(riXML) |> ignore
        )

    xmlElement

/// Serialize Location
let locationToXml (xmlElement:XmlElement) (xdoc:XmlDocument) (x:Location) = 
    
    identifiersToXml xmlElement xdoc x
    
    let orientationXml = xdoc.CreateElement(QualifiedName.orientationProperty,Terms.sbolns)
    orientationXml.SetAttribute("resource",Terms.rdfns,Orientation.toURI x.orientation) |> ignore
    xmlElement.AppendChild(orientationXml) |> ignore

/// Serialize Range
let rangeToXml (xdoc:XmlDocument) (x:Range) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Range,Terms.sbolns)
    
    locationToXml xmlElement xdoc x 
    
    let startIndexXml = xdoc.CreateElement(QualifiedName.startIndexProperty,Terms.sbolns)
    startIndexXml.AppendChild(xdoc.CreateTextNode(x.startIndex.ToString())) |> ignore
    xmlElement.AppendChild(startIndexXml) |> ignore

    let endIndexXml = xdoc.CreateElement(QualifiedName.endIndexProperty,Terms.sbolns)
    endIndexXml.AppendChild(xdoc.CreateTextNode(x.endIndex.ToString())) |> ignore
    xmlElement.AppendChild(endIndexXml) |> ignore

    xmlElement

/// Serialize Cut
let cutToXml (xdoc:XmlDocument) (x:Cut) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Cut,Terms.sbolns)
    
    locationToXml xmlElement xdoc x 
    
    let atXml = xdoc.CreateElement(QualifiedName.atProperty,Terms.sbolns)
    atXml.AppendChild(xdoc.CreateTextNode(x.at.ToString())) |> ignore
    xmlElement.AppendChild(atXml) |> ignore


    xmlElement

/// Serialize GenericLocation
let genericLocationToXml (xdoc:XmlDocument) (x:GenericLocation) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.GenericLocation,Terms.sbolns)
    
    locationToXml xmlElement xdoc x 

    xmlElement

/// Serialize SequenceAnnotation
let sequenceAnnotationToXml (xdoc:XmlDocument) (x:SequenceAnnotation) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.SequenceAnnotation,Terms.sbolns)
    
    identifiersToXml xmlElement xdoc x 

    x.locations |> List.iter (fun loc -> 
        let locationXml = xdoc.CreateElement(QualifiedName.locationProperty,Terms.sbolns)
        match loc with 
        | :? Range as range -> locationXml.AppendChild(rangeToXml xdoc range) |> ignore
        | :? Cut as cut -> locationXml.AppendChild(cutToXml xdoc cut) |> ignore
        | :? GenericLocation as genericlocation -> locationXml.AppendChild(genericLocationToXml xdoc genericlocation) |> ignore
        | _ -> failwith "Location can only be of type Range, Cut, or GenericLocation"
        xmlElement.AppendChild(locationXml) |> ignore
        )
    

    
    match x.componentObj with 
    | Some(comp) -> 
        let componentXml = xdoc.CreateElement(QualifiedName.componentProperty,Terms.sbolns)
        componentXml.SetAttribute("resource",Terms.rdfns,comp.uri) |> ignore
        xmlElement.AppendChild(componentXml) |> ignore
    | None -> ()
    
    x.roles |> List.iter(fun r -> roleToXml xmlElement xdoc r)

    xmlElement


/// Serialize SequenceConstraint
let sequenceConstraintToXml (xdoc:XmlDocument) (x:SequenceConstraint) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.SequenceConstraint,Terms.sbolns)
    
    identifiersToXml xmlElement xdoc x 

    let subjectXml = xdoc.CreateElement(QualifiedName.subjectProperty,Terms.sbolns)
    subjectXml.SetAttribute("resource",Terms.rdfns,x.subject.uri) |> ignore
    xmlElement.AppendChild(subjectXml) |> ignore

    let objectXml = xdoc.CreateElement(QualifiedName.objectProperty,Terms.sbolns)
    objectXml.SetAttribute("resource",Terms.rdfns,x.object.uri) |> ignore
    xmlElement.AppendChild(objectXml) |> ignore

    let restrictionXml = xdoc.CreateElement(QualifiedName.restrictionProperty,Terms.sbolns)
    restrictionXml.SetAttribute("resource",Terms.rdfns,Restriction.toURI x.restriction) |> ignore
    xmlElement.AppendChild(restrictionXml) |> ignore

    xmlElement

/// Serialize ComponentDefinition
let componentDefinitionToXml (xdoc:XmlDocument) (x:ComponentDefinition) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.ComponentDefinition,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x 
    
    (* Add Sequences *)
    x.sequences |> List.iter(fun seq -> 
        let seqXml = xdoc.CreateElement(QualifiedName.sequenceProperty,Terms.sbolns)
        seqXml.SetAttribute("resource",Terms.rdfns,seq.uri) |> ignore
        xmlElement.AppendChild(seqXml) |> ignore
        )
    
    (* Add Roles*)
    x.roles |> List.iter(fun role -> roleToXml xmlElement xdoc role)
    
    (* Add Types*)    
    x.types |> List.iter(fun t -> 
        let typeXml = xdoc.CreateElement(QualifiedName.typeProperty,Terms.sbolns)
        typeXml.SetAttribute("resource",Terms.rdfns,ComponentDefinitionType.toURI t) |> ignore
        xmlElement.AppendChild(typeXml) |> ignore
        )
    
    (*Add Components*)    
    x.components |> List.iter(fun comp -> 
        let componentPropertyXml = xdoc.CreateElement(QualifiedName.componentProperty,Terms.sbolns)
        componentPropertyXml.AppendChild(componentToXml xdoc comp) |> ignore
        xmlElement.AppendChild(componentPropertyXml) |> ignore
        )
    

    (*Add Sequence Annotations*)    
    x.sequenceAnnotations |> List.iter (fun sa -> 
        let saPropertyXml = xdoc.CreateElement(QualifiedName.sequenceAnnotationProperty,Terms.sbolns)
        saPropertyXml.AppendChild(sequenceAnnotationToXml xdoc sa) |> ignore
        xmlElement.AppendChild(saPropertyXml) |> ignore
        )
    
    
    (*Add Sequence Constraints*)    
    x.sequenceConstraints |> List.iter (fun sc -> 
        let scPropertyXml = xdoc.CreateElement(QualifiedName.sequenceConstraintsProperty,Terms.sbolns)
        scPropertyXml.AppendChild(sequenceConstraintToXml xdoc sc) |> ignore
        xmlElement.AppendChild(scPropertyXml) |> ignore
        )
        
    xmlElement

/// Serialize VariableComponent
let variableComponentToXml (xdoc:XmlDocument) (x:VariableComponent) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.VariableComponent,Terms.sbolns)
    
    identifiersToXml xmlElement xdoc x 
    
    let operatorXml = xdoc.CreateElement(QualifiedName.operatorProperty,Terms.sbolns)
    operatorXml.SetAttribute("resource",Terms.rdfns,Operator.toURI x.operator) |> ignore
    xmlElement.AppendChild(operatorXml) |> ignore

    (*Add Variable*)
    let componentXml = xdoc.CreateElement(QualifiedName.variableProperty,Terms.sbolns)
    componentXml.SetAttribute("resource",Terms.rdfns,x.variable) |> ignore
    xmlElement.AppendChild(componentXml) |> ignore

    x.variants |> List.iter (fun v -> 
        let variantXml = xdoc.CreateElement(QualifiedName.variantProperty,Terms.sbolns)
        variantXml.SetAttribute("resource",Terms.rdfns,v) |> ignore
        xmlElement.AppendChild(variantXml) |> ignore
        )

    x.variantDerivations |> List.iter (fun v -> 
        let variantXml = xdoc.CreateElement(QualifiedName.variantDerivationProperty,Terms.sbolns)
        variantXml.SetAttribute("resource",Terms.rdfns,v) |> ignore
        xmlElement.AppendChild(variantXml) |> ignore
        )
    
    x.variantCollections |> List.iter (fun v -> 
        let variantXml = xdoc.CreateElement(QualifiedName.variantCollectionProperty,Terms.sbolns)
        variantXml.SetAttribute("resource",Terms.rdfns,v) |> ignore
        xmlElement.AppendChild(variantXml) |> ignore
        )

    xmlElement

/// Serialize CombinatorialDerivation
let combinatorialDerivationToXml (xdoc:XmlDocument) (x:CombinatorialDerivation) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.CombinatorialDerivation,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x 
    
    match x.strategy with 
    | Some(s) -> 
        let strategyXml = xdoc.CreateElement(QualifiedName.strategyProperty,Terms.sbolns)
        strategyXml.SetAttribute("resource",Terms.rdfns,Strategy.toURI s) |> ignore
        xmlElement.AppendChild(strategyXml) |> ignore
    | None -> ()
    
    let templateXml = xdoc.CreateElement(QualifiedName.templateProperty,Terms.sbolns)
    templateXml.SetAttribute("resource",Terms.rdfns,x.template) |> ignore
    xmlElement.AppendChild(templateXml) |> ignore
    

    x.variableComponents |> List.iter (fun vc ->
        let variableComponentXml = xdoc.CreateElement(QualifiedName.variableComponentProperty,Terms.sbolns)
        variableComponentXml.AppendChild(variableComponentToXml xdoc vc) |> ignore
        xmlElement.AppendChild(variableComponentXml) |> ignore
        )
    
    xmlElement

/// Serialize Model
let modelToXml (xdoc:XmlDocument) (x:Model) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Model,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x 
    
    let sourceXml = xdoc.CreateElement(QualifiedName.sourceProperty,Terms.sbolns)
    sourceXml.SetAttribute("resource",Terms.rdfns,x.source) |> ignore
    xmlElement.AppendChild(sourceXml) |> ignore

    let langXml = xdoc.CreateElement(QualifiedName.languageProperty,Terms.sbolns)
    langXml.SetAttribute("resource",Terms.rdfns,Language.toURI x.language) |> ignore
    xmlElement.AppendChild(langXml) |> ignore

    let frameworkXml = xdoc.CreateElement(QualifiedName.frameworkProperty,Terms.sbolns)
    frameworkXml.SetAttribute("resource",Terms.rdfns,Framework.toURI x.framework) |> ignore
    xmlElement.AppendChild(frameworkXml) |> ignore
    
    xmlElement

/// Serialize Module
let moduleToXml (xdoc:XmlDocument) (x:Module) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Module,Terms.sbolns)
    
    identifiersToXml xmlElement xdoc x 
    
    let definitionXml = xdoc.CreateElement(QualifiedName.definitionProperty,Terms.sbolns)
    definitionXml.SetAttribute("resource",Terms.rdfns,x.definition) |> ignore
    xmlElement.AppendChild(definitionXml) |> ignore

    
    x.mapsTos |> List.iter (fun mt -> 
        let mapsTosXml = xdoc.CreateElement(QualifiedName.mapsToProperty,Terms.sbolns)
        mapsTosXml.AppendChild(mapsToToXml xdoc mt) |> ignore
        xmlElement.AppendChild(mapsTosXml) |> ignore
        )
    
    
    xmlElement

/// Serialize FunctionalComponent
let functionalComponentToXml (xdoc:XmlDocument) (x:FunctionalComponent) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.FunctionalComponent,Terms.sbolns)
    
    componentInstanceToXml xmlElement xdoc x 
    
    let directionXml = xdoc.CreateElement(QualifiedName.directionProperty,Terms.sbolns)
    directionXml.SetAttribute("resource",Terms.rdfns,Direction.toURI x.direction) |> ignore
    xmlElement.AppendChild(directionXml) |> ignore
    
    xmlElement


/// Serialize Participation
let participationToXml (xdoc:XmlDocument) (x:Participation) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Participation,Terms.sbolns)
    
    identifiersToXml xmlElement xdoc x 
    
    x.roles |> List.iter (fun r -> 
        let xmlRole = xdoc.CreateElement(QualifiedName.roleProperty,Terms.sbolns)
        xmlRole.SetAttribute("resource",Terms.rdfns,ParticipationRole.toURI r) |> ignore
        xmlElement.AppendChild(xmlRole) |> ignore
        )
    
    let participantXml = xdoc.CreateElement(QualifiedName.participantProperty,Terms.sbolns)
    participantXml.SetAttribute("resource",Terms.rdfns,x.participant.uri) |> ignore
    xmlElement.AppendChild(participantXml) |> ignore

    xmlElement

/// Serialize Interaction
let interactionToXml (xdoc:XmlDocument) (x:Interaction) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Interaction,Terms.sbolns)
    
    identifiersToXml xmlElement xdoc x 
    
    x.participations |> List.iter (fun part -> 
        let partXml = xdoc.CreateElement(QualifiedName.participationProperty,Terms.sbolns)
        partXml.AppendChild(participationToXml xdoc part) |> ignore
        xmlElement.AppendChild(partXml) |> ignore
        )
    
    x.types |> List.iter (fun t -> 
        let typeXml = xdoc.CreateElement(QualifiedName.typeProperty,Terms.sbolns)
        typeXml.SetAttribute("resource",Terms.rdfns,InteractionType.toURI t) |> ignore
        xmlElement.AppendChild(typeXml) |> ignore
        )

    xmlElement

/// Serialize ModuleDefinition
let moduleDefinitionToXml (xdoc:XmlDocument) (x:ModuleDefinition) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.ModuleDefinition,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x 
    
    x.roles |> List.iter(fun r -> 
        let roleXml = xdoc.CreateElement(QualifiedName.roleProperty,Terms.sbolns)
        roleXml.SetAttribute("resource",Terms.rdfns,Role.toURI r) |> ignore
        xmlElement.AppendChild(roleXml) |> ignore
        )
    
    x.functionalComponents |> List.iter(fun fc -> 
        let fcXml =xdoc.CreateElement(QualifiedName.functionalComponentProperty,Terms.sbolns)
        fcXml.AppendChild(functionalComponentToXml xdoc fc) |> ignore
        xmlElement.AppendChild(fcXml) |> ignore
        )

    x.interactions |> List.iter(fun i -> 
        let interactionXml =xdoc.CreateElement(QualifiedName.interactionProperty,Terms.sbolns)
        interactionXml.AppendChild(interactionToXml xdoc i) |> ignore
        xmlElement.AppendChild(interactionXml) |> ignore
        )

    x.modules |> List.iter (fun m -> 
        let moduleXml = xdoc.CreateElement(QualifiedName.moduleProperty,Terms.sbolns)
        moduleXml.AppendChild(moduleToXml xdoc m) |> ignore
        xmlElement.AppendChild(moduleXml) |> ignore
        )    

    x.models |> List.iter (fun m -> 
        let modelXml = xdoc.CreateElement(QualifiedName.modelProperty,Terms.sbolns)
        modelXml.SetAttribute("resource",Terms.rdfns,m.uri) |> ignore
        xmlElement.AppendChild(modelXml) |> ignore
        )

    xmlElement

/// Serialize Implementation
let implementationToXml (xdoc:XmlDocument) (x:Implementation) = 
    let xmlElement = xdoc.CreateElement(QualifiedName.Implementation,Terms.sbolns)
    
    topLevelToXml xmlElement xdoc x 
    
    match x.built with 
    | Some(b) -> 
        let builtXml = xdoc.CreateElement(QualifiedName.builtProperty,Terms.sbolns)
        match b with 
        | CD(y) -> builtXml.SetAttribute("resource",Terms.rdfns,y.uri) |> ignore
        | MD(y) -> builtXml.SetAttribute("resource",Terms.rdfns,y.uri) |> ignore
        xmlElement.AppendChild(builtXml) |> ignore
    | None -> ()

    xmlElement


/// Serialize SBOLDocument
let sbolToXml (sbol:SBOLDocument) =
    let xdoc = new XmlDocument();
    let rootXml = xdoc.CreateElement(QualifiedName.rdfQN,Terms.rdfns) 
    xdoc.AppendChild(rootXml) |> ignore
    xdoc.DocumentElement.SetAttribute(QualifiedName.sbolQN,Terms.sbolns)
    xdoc.DocumentElement.SetAttribute(QualifiedName.dctermsQN,Terms.dctermsns)
    xdoc.DocumentElement.SetAttribute(QualifiedName.provQN,Terms.provns)
    xdoc.DocumentElement.SetAttribute(QualifiedName.ns0QN,Terms.ns0)
    xdoc.AppendChild(rootXml) |> ignore
    
    sbol.collections |> List.iter (fun x -> rootXml.AppendChild(collectionToXml xdoc x) |> ignore)

    sbol.attachments |> List.iter (fun x -> rootXml.AppendChild(attachmentToXml xdoc x) |> ignore)

    sbol.sequences |> List.iter (fun x -> rootXml.AppendChild(sequenceToXml xdoc x) |> ignore)

    sbol.componentDefinitions |> List.iter (fun x -> rootXml.AppendChild(componentDefinitionToXml xdoc x) |> ignore)
    
    sbol.models |> List.iter (fun x -> rootXml.AppendChild(modelToXml xdoc x) |> ignore)

    sbol.moduleDefinitions |> List.iter (fun x -> rootXml.AppendChild(moduleDefinitionToXml xdoc x) |> ignore)
    
    sbol.combinatorialDerivations |> List.iter (fun x -> rootXml.AppendChild(combinatorialDerivationToXml xdoc x) |> ignore)
    
    sbol.implementations |> List.iter(fun x -> rootXml.AppendChild(implementationToXml xdoc x) |> ignore)

    xdoc


(* From XML Methods *)

let getChildXmlElements (xElem:XmlElement) = 
     let childXmlElements = ([0..(xElem.ChildNodes.Count-1)]
                               |> List.map(fun index -> xElem.ChildNodes.Item(index)))
                               |> List.filter(fun item -> 
                                    match item with 
                                    | :? XmlElement -> true
                                    | _ -> false)
                               |> List.map (fun item -> (downcast item:XmlElement))    
     childXmlElements

let identifiersFromXml (xElem:XmlElement) (childXmlElements:XmlElement list) = 
    let uri = xElem.GetAttribute("about")
    
    let displayIdXmlList = childXmlElements 
                           |> List.filter(fun xmlElem -> (xmlElem.Name = QualifiedName.displayId))
    
    let annotationXmlList = childXmlElements
                            |> List.filter (fun xmlElem -> (not (QualifiedName.isKnownQName (xmlElem.Name)) ))

    let getQName (annXmlElem:XmlElement) = 
        let localName = annXmlElem.LocalName
        let prefix = annXmlElem.Prefix
        let nsuri = annXmlElem.NamespaceURI
        match prefix with 
        | "" -> 
            match nsuri with 
            | "" -> Name (localName)
            | _ -> QualifiedName(localName,nsuri)
        | _ -> FullQName(prefix,localName,nsuri)
    

    let rec getAnnotation (annXmlElem:XmlElement) = 
        let qname = getQName annXmlElem
        let annotationValue = 
            match annXmlElem.HasAttribute("resource") with 
            | true -> AnnotationValue.Uri(annXmlElem.GetAttribute("resource"))
            | false -> 
                match annXmlElem.InnerText with 
                | "" -> 
                    let annotationChildren = getChildXmlElements annXmlElem
                    match annotationChildren with 
                    | [] -> failwith "Nested Annotation not found" 
                    | [nannXmlElem] ->
                        let nestedQName = getQName nannXmlElem
                        let nestedURI = nannXmlElem.GetAttribute("resouce")
                        let nestedChildNodes = getChildXmlElements nannXmlElem
                        let nestedAnnotations = nestedChildNodes |> List.map (fun x -> getAnnotation x)
                        AnnotationValue.NestedAnnotation(new NestedAnnotation(nestedQName,nestedURI,nestedAnnotations))
                    | _ -> failwith "Too many nested annotations found. This is not right"

                | _ -> AnnotationValue.Literal(Literal.from_string (annXmlElem.InnerText)) 
                (*let annotationChildren = getChildXmlElements annXmlElem
                match annotationChildren with 
                | [] -> failwith "This annotation does not seem to have any value"
                | [annXml] -> 
                    match annXml.NodeType with 
                    | XmlNodeType.Text ->
                        AnnotationValue.Literal(Literal.from_string (annXml.InnerText)) 
                    | _ -> 
                        let nestAnn = getAnnotation annXml
                        failwith "not implemented yet"
                | _ -> failwith "not implemented yet"*)
                
        new Annotation(qname,annotationValue)
    
    let annotationList = annotationXmlList |> List.map (fun x -> getAnnotation x)
    
    let displayId = 
        match displayIdXmlList with 
        | [] -> None 
        | [d] -> Some(d.InnerText)
        | _ -> failwith "Too many Description fields"
    
    let nameXmlList = childXmlElements 
                      |> List.filter(fun xmlElem -> (xmlElem.Name = QualifiedName.name))
    
    let name = 
        match nameXmlList with 
        | [] -> None
        | [n] -> Some(n.InnerText)
        | _ -> failwith "Too many Name fields"
    
    let versionXmlList = childXmlElements 
                         |> List.filter(fun xmlElem -> (xmlElem.Name = QualifiedName.version))
    
    let version = 
        match versionXmlList with 
        | [] -> None
        | [v] -> Some(v.InnerText)
        | _ -> failwith "Too many Version fields"

    let persistentIdXmlList = childXmlElements 
                              |> List.filter(fun xmlElem -> (xmlElem.Name = QualifiedName.persistentIdentity))
    
    let persistentId = 
        match persistentIdXmlList with 
        | [] -> None
        | [pid] -> Some(pid.GetAttribute("resource"))
        | _ -> failwith "Too many Persistent Identity fields."
    
    let descriptionXmlList = childXmlElements 
                             |> List.filter(fun xmlElem -> (xmlElem.Name = QualifiedName.description))
    let description = 
        match descriptionXmlList with 
        | [] -> None
        | [desc] -> Some(desc.InnerText)
        | _ -> failwith "Too many Description fields."
    (uri,name,displayId,version,persistentId,description,annotationList)

   

let addAnnotations (id:Identifiers) (annotations:(Annotation) list) (d:string option)= 
    id.annotations <- annotations
    id.description <- d
    
let topLevelFromXml (xElem:XmlElement) (childXmlElements:XmlElement list) = 
    
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements
    let attchXmlList = childXmlElements 
                       |> List.filter(fun xmlElem -> (xmlElem.Name = QualifiedName.attachmentProperty))
    let attachments = attchXmlList |> List.map (fun x -> x.GetAttribute("resource"))         
    (uri,name,displayId,version,persistentId,attachments,description,annotations)


let sequenceFromXml (xElem:XmlElement) = 
        let childXmlElements = getChildXmlElements xElem
        let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements
        
        

        let encodingList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.encodingProperty)
        let elementsList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.elementsProperty)

        let elements = 
            match elementsList with 
            | [] -> failwith "Elements is a required field in a Sequence"
            | [e] -> e.InnerText
            | _ -> failwith "Too many Elements found"
        
        let encoding = 
            match encodingList with 
            | [] -> failwith "Encoding is a required field in a Sequence"
            | [e] -> e.GetAttribute("resource")
            | _ -> failwith "Too many Encoding properties found"

        let x = new Sequence(uri,name,displayId,version,persistentId,attachments,elements,Encoding.fromURI(encoding))
        addAnnotations x annotations description
        x

let attachmentFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements
    
    let sourceList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.sourceProperty)
    let formatList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.formatProperty)
    let sizeList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.sizeProperty)
    let hashList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.hashProperty)

    let source = 
        match sourceList with 
        | [] -> failwith "Source cannot be empty in Attachment"
        | [s] -> s.GetAttribute("resource")
        | _ -> failwith "Too many source fields encountered in Attachment"
    
    let format = 
       match formatList with 
        | [] -> None
        | [s] -> Some(s.GetAttribute("resource"))
        | _ -> failwith "Too many format fields encountered in Attachment" 
    
    let size = 
       match sizeList with 
        | [] -> None
        | [s] -> Some(Convert.ToInt64(s.InnerText))
        | _ -> failwith "Too many size fields encountered in Attachment" 
    
    let hash = 
       match hashList with 
        | [] -> None
        | [s] -> Some(s.InnerText)
        | _ -> failwith "Too many size fields encountered in Attachment" 
    
    let x = Attachment(uri,name,displayId,version,persistentId,attachments,source,format,size,hash)
    addAnnotations x annotations description
    x

let collectionFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements
    
    let collectionList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.memberProperty)
    let collection = collectionList |> List.map (fun x -> x.GetAttribute("resource"))
        
    let x = Collection(uri,name,displayId,version,persistentId,attachments,collection)
    addAnnotations x annotations description
    x    

let mapsToFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements
    
    let refinementList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.refinementProperty)
    let localList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.localProperty)
    let remoteList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.remoteProperty)
    
    let local = 
        match localList with 
        | [] -> failwith "Property local is required in MapsTo"
        | [x] -> x.GetAttribute("resource")
        | _ -> failwith "Too many local properties encountered in MapsTo"
    
    let remote = 
        match remoteList with 
        | [] -> failwith "Property remote is required in MapsTo"
        | [x] -> x.GetAttribute("resource")
        | _ -> failwith "Too many remote properties encountered in MapsTo"
    
    let refinement = 
        match refinementList with 
        | [] -> failwith "Property refinement is required in MapsTo"
        | [x] -> Refinement.fromURI(x.GetAttribute("resource"))
        | _ -> failwith "Too many refinement properties encountered in MapsTo"
    

    let x = MapsTo(uri,name,displayId,version,persistentId,local,remote,refinement)
    addAnnotations x annotations description
    x  

let componentInstanceFromXml (xElem:XmlElement) (childXmlElements:XmlElement list) = 
    
    let definitionList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.definitionProperty)
    let accessList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.accessProperty)
    let mapsTosList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.mapsToProperty)
    
    let definition = 
        match definitionList with 
        | [] -> failwith "Property definition is required in a ComponentInstance"
        | [x] -> x.GetAttribute("resource")
        | _ -> failwith "Too many definition properties encountered in ComponentInstance"
    
    let access = 
        match accessList with 
        | [] -> failwith "Property access is required in ComponentInstance"
        | [x] -> Access.fromURI(x.GetAttribute("resource"))
        | _ -> failwith "Too many access properties encountered in ComponentInstance"
    
    let mapsTos = 
        mapsTosList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                mapsToFromXml elem
            | _ -> failwith "Unexpected Xml Node encountered")
        
    (definition,access,mapsTos)

let componentFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements
    let (definition,access,mapsTos) = componentInstanceFromXml xElem childXmlElements
    let roleList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.roleProperty)
    let roleIntegrationList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.roleIntegrationProperty)
    
    let roles = roleList |> List.map (fun x -> Role.fromURI(x.GetAttribute("resource")))
        
    let roleIntegrations = roleIntegrationList |> List.map (fun x -> RoleIntegration.fromURI(x.GetAttribute("resource")))
        

    let x = Component(uri,name,displayId,version,persistentId,definition,access,mapsTos,roles,roleIntegrations)
    addAnnotations x annotations description
    x      

let locationFromXml (xElem:XmlElement) (childXmlElements:XmlElement list) = 
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements

    let orientationList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.orientationProperty)
    
    let orientation = 
        match orientationList with 
        | [] -> failwith "Property orientation is required in a Location"
        | [x] -> Orientation.fromURI(x.GetAttribute("resource"))
        | _ -> failwith "Too many orientation properties encountered in Location"
    
        
    (uri,name,displayId,version,persistentId,description,annotations,orientation)

let rangeFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations,orientation) = locationFromXml xElem childXmlElements

    let startList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.startIndexProperty)
    let endList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.startIndexProperty)
    
    let startIndex = 
        match startList with 
            | [] -> failwith "Property startIndex is required in a Range"
            | [x] -> Convert.ToInt32(x.InnerText)
            | _ -> failwith "Too many startIndex properties encountered in Range"
    let endIndex = 
        match endList with 
            | [] -> failwith "Property endIndex is required in a Range"
            | [x] -> Convert.ToInt32(x.InnerText)
            | _ -> failwith "Too many endIndex properties encountered in Range"
    
    let x = Range(uri,name,displayId,version,persistentId,orientation,startIndex,endIndex)
    addAnnotations x annotations description
    x  

let cutFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations,orientation) = locationFromXml xElem childXmlElements

    let atList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.atProperty)
    
    let atIndex = 
        match atList with 
            | [] -> failwith "Property at is required in a Cut"
            | [x] -> Convert.ToInt32(x.InnerText)
            | _ -> failwith "Too many at properties encountered in Cut"
    
    let x = Cut(uri,name,displayId,version,persistentId,orientation,atIndex)
    addAnnotations x annotations description
    x  

let genericLocationFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations,orientation) = locationFromXml xElem childXmlElements

    
    let x = GenericLocation(uri,name,displayId,version,persistentId,orientation)
    addAnnotations x annotations description
    x 

let sequenceAnnotationFromXml (xElem:XmlElement) (components:Component list)= 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements

    let locationList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.locationProperty)
    
    let locations = 
        locationList
        |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                match elem with 
                | elem when (elem.Name = QualifiedName.Range) -> (rangeFromXml elem) :> Location
                | elem when (elem.Name = QualifiedName.Cut) -> (cutFromXml elem) :> Location
                | elem when (elem.Name = QualifiedName.GenericLocation) -> (genericLocationFromXml elem) :> Location
                | _ -> failwith "Unknown type of location found"
            | _ -> failwith "Unexpected Xml Node encountered")
    
    let roleList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.roleProperty)
    let roles = roleList |> List.map (fun x -> Role.fromURI(x.GetAttribute("resource")))
    
    let componentList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.componentProperty)
    let compId = 
        match componentList with 
        | [] -> None
        | [c] -> Some(c.GetAttribute("resource"))
        | _ -> failwith "Too many Component properties found in SequenceAnnotations"
    
    let comp = 
        match compId with 
        | Some(id) -> 
            let copt = 
                components 
                |> List.map (fun c -> (c.uri,c))
                |> List.tryFind( fun (fid,fcomp) -> (fid = id))
            match copt with 
            | Some(k,v) -> Some(v)
            | None -> failwith ("Component with id: " + id + " not defined." )
        | None -> None
        
    
    let x = SequenceAnnotation(uri,name,displayId,version,persistentId,comp,locations,roles)
    addAnnotations x annotations description
    x 

let sequenceConstraintFromXml (xElem:XmlElement) (components:Component list)= 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements

    let subjectList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.subjectProperty)
    let objectList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.objectProperty)
    let restrictionList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.restrictionProperty)
    
    let subject = 
        match subjectList with 
        | [] -> failwith "Subject is a required property in Sequence Constraint"
        | [s] -> 
            let id = s.GetAttribute("resource")
            let copt = 
                components 
                |> List.map (fun c -> (c.uri,c))
                |> List.tryFind( fun (fid,fcomp) -> (fid = id))
            match copt with 
            | Some(k,v) -> v
            | None -> failwith ("Component with id: " + id + " not defined." )            
        | _ -> failwith "Too many subject properties in Sequence Constraint"
    
    let object = 
        match objectList with 
        | [] -> failwith "Object is a required property in Sequence Constraint"
        | [s] -> 
            let id = s.GetAttribute("resource")
            let copt = 
                components 
                |> List.map (fun c -> (c.uri,c))
                |> List.tryFind( fun (fid,fcomp) -> (fid = id))
            match copt with 
            | Some(k,v) -> v
            | None -> failwith ("Component with id: " + id + " not defined." )            
        | _ -> failwith "Too many object properties in Sequence Constraint"
    
    let restriction = 
        match restrictionList with 
        | [] -> failwith "Restriction is a required property in Sequence Constraint"
        | [s] -> Restriction.fromURI (s.GetAttribute("resource"))  
        | _ -> failwith "Too many restriction properties in Sequence Constraint"

           
    let x = SequenceConstraint(uri,name,displayId,version,persistentId,subject,object,restriction)
    addAnnotations x annotations description
    x

let componentDefinitionFromXml (xElem:XmlElement) (sequences:Sequence list)= 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements

    let componentList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.componentProperty)
    let sequenceList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.sequenceProperty)
    let saList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.sequenceAnnotationProperty)
    let scList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.sequenceConstraintsProperty)
    let roleList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.roleProperty)
    let typeList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.typeProperty)

    let components = 
        componentList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                componentFromXml elem
            | _ -> failwith "Unexpected Xml Node encountered")
    
    
    let sequences = 
        sequenceList
        |> List.map(fun x -> x.GetAttribute("resource"))
        |> List.map(fun x -> 
            match sequences |> List.tryFind(fun y -> y.uri = x) with  
            | Some(seq) -> seq
            | None -> failwith "Sequence not listed"
            )
    
    let sequenceAnnotations = 
        saList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                sequenceAnnotationFromXml elem components
            | _ -> failwith "Unexpected Xml Node encountered")
    
    let sequenceConstraints = 
        scList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                sequenceConstraintFromXml elem components
            | _ -> failwith "Unexpected Xml Node encountered")
    
    let roles = roleList |> List.map (fun x -> Role.fromURI (x.GetAttribute("resource")))
    let types = typeList |> List.map (fun x -> ComponentDefinitionType.fromURI (x.GetAttribute("resource")))

    let x = ComponentDefinition(uri,name,displayId,version,persistentId,attachments,types,roles,sequences,components,sequenceAnnotations,sequenceConstraints)
    addAnnotations x annotations description
    x


let variableComponentFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements

    let operatorList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.operatorProperty)
    let variantList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.locationProperty)
    let variantCollectionList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.locationProperty)
    let variantDerivationList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.locationProperty)
    let variableList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.locationProperty)
    
    let operator = 
        match operatorList with 
        | [] -> failwith "Operator is a required property in Variable Components"
        | [s] -> Operator.fromURI (s.GetAttribute("resource"))                 
        | _ -> failwith "Too many operator properties in Variable Components"
    
    let variants = variantList |> List.map (fun x -> x.GetAttribute("resource"))
    let variantCollections = variantCollectionList |> List.map (fun x -> x.GetAttribute("resource"))
    let variantDerivations = variantDerivationList |> List.map (fun x -> x.GetAttribute("resource"))
    let variable = 
        match variableList with 
        | [] -> failwith "Variable is a required property in Variable Components"
        | [s] -> (s.GetAttribute("resource"))                 
        | _ -> failwith "Too many variable properties in Variable Components"
 
           
    let x = VariableComponent(uri,name,displayId,version,persistentId,operator,variants,variantCollections,variantDerivations,variable)
    addAnnotations x annotations description
    x

let combinatorialDerivationFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements

    let strategyList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.strategyProperty)
    let templateList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.templateProperty)
    let variableComponentList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.variableComponentProperty)
    
    let strategy = 
        match strategyList with 
        | [] -> None
        | [s] -> Some(Strategy.fromURI (s.GetAttribute("resource")))                 
        | _ -> failwith "Too many strategy properties in Combinatorial Derivation"
    
    let template = 
        match templateList with 
        | [] -> failwith "Template is a required property in Combinatorial Derivation"
        | [s] -> s.GetAttribute("resource")                 
        | _ -> failwith "Too many template properties in Combinatorial Derivation"
    
    let variableComponents = 
        variableComponentList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                variableComponentFromXml elem
            | _ -> failwith "Unexpected Xml Node encountered")


    let x = CombinatorialDerivation(uri,name,displayId,version,persistentId,attachments,strategy,template,variableComponents)
    addAnnotations x annotations description
    x

let modelFromXml (xElem:XmlElement) = 
    let childXmlElements =getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements

    let sourceList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.sourceProperty)
    let languageList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.languageProperty)
    let frameworkList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.frameworkProperty)
    
    let source = 
        match sourceList with 
        | [] -> failwith "Source is a required field in Model"
        | [s] -> s.GetAttribute("resource")                 
        | _ -> failwith "Too many source properties in Model"
    
    let language = 
        match languageList with 
        | [] -> failwith "Language is a required field in Model"
        | [s] -> Language.fromURI (s.GetAttribute("resource"))                 
        | _ -> failwith "Too many language properties in Model"
    
    let framework = 
        match frameworkList with 
        | [] -> failwith "Framework is a required field in Model"
        | [s] -> Framework.fromURI (s.GetAttribute("resource"))                 
        | _ -> failwith "Too many framework properties in Model"
    

    let x = Model(uri,name,displayId,version,persistentId,attachments,source,language,framework)
    addAnnotations x annotations description
    x

let moduleFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements

    let definitionList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.definitionProperty)
    let mapsTosList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.mapsToProperty)
    
    let definition = 
        match definitionList with 
        | [] -> failwith "Definition is a required field in Module"
        | [s] -> s.GetAttribute("resource")                 
        | _ -> failwith "Too many definition properties in Module"

    let mapsTos = 
        mapsTosList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                mapsToFromXml elem
            | _ -> failwith "Unexpected Xml Node encountered")


    let x = Module(uri,name,displayId,version,persistentId,definition,mapsTos)
    addAnnotations x annotations description
    x

let functionalComponentFromXml (xElem:XmlElement) = 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements
    let (definition,access,mapsTos) = componentInstanceFromXml xElem childXmlElements
    
    let directionList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.directionProperty)
    
    let direction = 
        match directionList with 
        | [] -> failwith "Direction is a required field in Functional Component"
        | [s] -> Direction.fromURI (s.GetAttribute("resource"))                 
        | _ -> failwith "Too many direction properties in Functional Component"

    let x = FunctionalComponent(uri,name,displayId,version,persistentId,definition,access,mapsTos,direction)
    addAnnotations x annotations description
    x

let participationFromXml (xElem:XmlElement) (fclist:FunctionalComponent list)= 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements
    
    let roleList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.roleProperty)
    let participantList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.participantProperty)
    
    let roles = roleList |> List.map(fun x -> ParticipationRole.fromURI (x.GetAttribute("resource"))) 
    
    let participant = 
        match participantList with 
        | [] -> failwith "Participant is a required property in Participation"
        | [s] -> 
            let id = s.GetAttribute("resource")
            let copt = 
                fclist 
                |> List.map (fun c -> (c.uri,c))
                |> List.tryFind( fun (fid,fcomp) -> (fid = id))
            match copt with 
            | Some(k,v) -> v
            | None -> failwith ("FunctionalComponent with id: " + id + " not defined." )            
        | _ -> failwith "Too many participant properties in Participation"

    
    let x = Participation(uri,name,displayId,version,persistentId,roles,participant)
    addAnnotations x annotations description
    x

let interactionFromXml (xElem:XmlElement) (fclist:FunctionalComponent list)= 
    let childXmlElements = getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,description,annotations) = identifiersFromXml xElem childXmlElements
    
    let typeList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.typeProperty)
    let participationList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.participationProperty)
    
    let types = typeList |> List.map(fun x -> InteractionType.fromURI (x.GetAttribute("resource"))) 
    
    let participations = 
        participationList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                participationFromXml elem fclist
            | _ -> failwith "Unexpected Xml Node encountered")

    
    let x = Interaction(uri,name,displayId,version,persistentId,types,participations)
    addAnnotations x annotations description
    x

let moduleDefinitionFromXml (xElem:XmlElement) (mlist:Model list)= 
    let childXmlElements =getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements

    let roleList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.roleProperty)
    let functionalComponentList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.functionalComponentProperty)
    let interactionList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.interactionProperty)
    let moduleList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.moduleProperty)
    let modelList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.modelProperty)
    
    let roles = roleList |> List.map (fun x -> Role.fromURI (x.GetAttribute("resource")))

    let functionalComponents = 
        functionalComponentList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                functionalComponentFromXml elem 
            | _ -> failwith "Unexpected Xml Node encountered")

    let interactions = 
        interactionList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                interactionFromXml elem functionalComponents
            | _ -> failwith "Unexpected Xml Node encountered")
    
    let modules = 
        moduleList |> List.map(fun x -> x.FirstChild)
        |> List.map(fun item -> 
            match item with 
            | :? XmlElement -> 
                let elem = (downcast item:XmlElement)
                moduleFromXml elem 
            | _ -> failwith "Unexpected Xml Node encountered")
    
    let models = 
        modelList
        |> List.map(fun x -> x.GetAttribute("resource"))
        |> List.map(fun x -> 
            match mlist |> List.tryFind(fun y -> y.uri = x) with  
            | Some(seq) -> seq
            | None -> failwith "Sequence not listed"
            )
    

    let x = ModuleDefinition(uri,name,displayId,version,persistentId,attachments,roles,modules,interactions,functionalComponents,models)
    addAnnotations x annotations description
    x


let implementationFromXml (xElem:XmlElement) (clist:ComponentDefinition list) (mlist:ModuleDefinition list) = 
    let childXmlElements =getChildXmlElements xElem
    let (uri,name,displayId,version,persistentId,attachments,description,annotations) = topLevelFromXml xElem childXmlElements

    let builtList = childXmlElements |> List.filter (fun elem -> elem.Name = QualifiedName.builtProperty)
    
    
    let built = 
        match builtList with 
        | [] -> None
        | [s] -> 
            let id = s.GetAttribute("resource")
            match clist |> List.tryFind(fun x -> x.uri = id) with 
            | Some(c) -> Some(CD(c))
            | None -> 
                match mlist |> List.tryFind(fun y -> y.uri = id) with 
                | Some(m) -> Some(MD(m))
                | None -> failwith "ModuleDefinition or ComponentDefinition not defined"
        | _ -> failwith "Too many built properties found in Implementation"          

    let x = Implementation(uri,name,displayId,version,persistentId,attachments,built)
    addAnnotations x annotations description
    x

let sbolFromXML (xdoc:XmlDocument) = 
    let rootXml = xdoc.FirstChild
    match rootXml with 
    | :? XmlElement -> 
        let (rootElem:XmlElement) = (downcast rootXml: XmlElement)
        let childXmlElements =getChildXmlElements rootElem
        let sequences = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.Sequence)
            |> List.map (fun x -> (sequenceFromXml x)) 
        let models = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.Model)
            |> List.map (fun x -> (modelFromXml x)) 
        let componentDefinitions = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.ComponentDefinition)
            |> List.map (fun x -> (componentDefinitionFromXml x sequences)) 
        
        let moduleDefinitions = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.ModuleDefinition)
            |> List.map (fun x -> (moduleDefinitionFromXml x models)) 
        
        let attachments = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.Attachment)
            |> List.map (fun x -> (attachmentFromXml x)) 
        
        let collections = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.Collection)
            |> List.map (fun x -> (collectionFromXml x)) 
        
        let combinatorialDerivations = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.CombinatorialDerivation)
            |> List.map (fun x -> (combinatorialDerivationFromXml x)) 
        
        let implementations = 
            childXmlElements 
            |> List.filter (fun elem -> elem.Name = QualifiedName.Implementation)
            |> List.map (fun x -> (implementationFromXml x componentDefinitions moduleDefinitions)) 

        let collection = 
            (collections |> List.map (fun x -> x :> TopLevel))
            @ (attachments |> List.map (fun x -> x :> TopLevel))
            @ (sequences |> List.map (fun x -> x :> TopLevel))
            @ (componentDefinitions |> List.map (fun x -> x :> TopLevel))
            @ (models |> List.map (fun x -> x :> TopLevel))
            @ (moduleDefinitions |> List.map (fun x -> x :> TopLevel))
            @ (combinatorialDerivations |> List.map (fun x -> x :> TopLevel))
            @ (implementations |> List.map (fun x -> x :> TopLevel))
            
        SBOLDocument(collection)
    | _ -> failwith "Unknown XML format"
