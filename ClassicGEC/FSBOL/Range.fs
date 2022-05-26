[<JavaScript>]
module FSBOL.Range
open FSBOL.Location


type Range(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, orientation:Orientation, startIndex:int, endIndex:int) = 
    inherit Location(uri, name, displayId, version, persistantId,orientation)

    do 
      if startIndex < 1 then failwith "Start Index must be greater than 0"
      if endIndex < 1 then failwith "End Index must be greater than 0"
    
    member x.startIndex = startIndex

    member x.endIndex = endIndex

    

