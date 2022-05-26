[<JavaScript>]
module FSBOL.Cut
open FSBOL.Location


type Cut(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, orientation:Orientation, at:int) = 
    inherit Location(uri, name, displayId, version, persistantId,orientation)

    do 
      if at < 1 then failwith "At must be greater than or equal to 0"
    
    member x.at = at
