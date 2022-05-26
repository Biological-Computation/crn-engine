[<JavaScript>]
module FSBOL.Identifiers
open System.Collections.Generic
open FSBOL.Annotation

[<AbstractClass>]
type Identifiers(uri:string, name:string option, displayId:string option, version:string option, persistentId:string option) = 

    /// Version of the object
    member i.version = version
    
    /// Name 
    member i.name = name

    /// Human readable id of the object
    member i.displayId = displayId

    /// Indentity shared by multiple versions of the same object 
    member i.persistentIdentity = persistentId
    
    /// Unique URI that identifies the object
    member i.uri = uri
    
    member val description:string option  = None with get,set
    
    member val annotations:Annotation list = [] with get,set 
    
    
    member x.getAnnotation (key:QName) = 
        x.annotations |> List.tryFind (fun ann -> QName.equal (ann.qName) key) 
        
