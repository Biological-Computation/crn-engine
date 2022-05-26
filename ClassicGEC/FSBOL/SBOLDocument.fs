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


type SBOLDocument (collection:TopLevel list) = 
    
    let cdlist  = collection 
                  |> List.choose(fun elem -> 
                    match elem with 
                    | :? ComponentDefinition as x -> Some(x)
                    | _ -> None)
    
    let mdlist  = collection 
                  |> List.choose(fun elem -> 
                    match elem with 
                    | :? ModuleDefinition as x -> Some(x)
                    | _ -> None)
    
    let attachlist  = collection 
                       |> List.choose(fun elem -> 
                         match elem with 
                         | :? Attachment as x -> Some(x)
                         | _ -> None)
    
    let combDerivations = collection 
                          |> List.choose(fun elem -> 
                            match elem with 
                            | :? CombinatorialDerivation as x -> Some(x)
                            | _ -> None)
    
    let implementionList = collection 
                           |> List.choose(fun elem -> 
                             match elem with 
                             | :? Implementation as x -> Some(x)
                             | _ -> None)

    let topModels  = collection 
                       |> List.choose(fun elem -> 
                         match elem with 
                         | :? Model as x -> Some(x)
                         | _ -> None)
    
    let topSequences  = collection 
                        |> List.choose(fun elem -> 
                          match elem with 
                          | :? Sequence as x -> Some(x)
                          | _ -> None)
    
    let topModels  = collection 
                     |> List.choose(fun elem -> 
                       match elem with 
                       | :? Model as x -> Some(x)
                       | _ -> None)
    
    let cdSequences = 
        let list = cdlist |> List.map(fun x -> x.sequences)
        match list with 
        | [] -> []
        | [a] -> a
        | _ -> list |> List.reduce(fun a b -> a@b)
                      
        
    let mdModels = 
        let list = mdlist |> List.map(fun x -> x.models)
        match list with 
        | [] -> []
        | [a] -> a
        | _ -> list |> List.reduce(fun a b -> a@b)

    let collectionList = collection 
                         |> List.choose(fun elem -> 
                           match elem with 
                           | :? Collection as x -> Some(x)
                           | _ -> None)

    (* *)
    member sbol.attachments = attachlist
    
    (* *)
    member sbol.componentDefinitions = cdlist

    (* *)
    member sbol.moduleDefinitions = mdlist

    (* *)
    member sbol.combinatorialDerivations = combDerivations

    (* *)
    member sbol.implementations = implementionList

    (* *)
    member sbol.sequences = topSequences@cdSequences 
                            |> List.map (fun x -> x.uri,x) 
                            |> Map.ofList 
                            |> Map.toList 
                            |> List.map (fun (_,value) -> value)
    (* *)
    member sbol.models = topModels@mdModels 
                            |> List.map (fun x -> x.uri,x) 
                            |> Map.ofList 
                            |> Map.toList 
                            |> List.map (fun (_,value) -> value)
    
    (* *)
    member sbol.collections = collectionList
