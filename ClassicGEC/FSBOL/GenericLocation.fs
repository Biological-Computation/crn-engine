[<JavaScript>]
module FSBOL.GenericLocation
open FSBOL.Location

type GenericLocation(uri:string, name:string option, displayId:string option, version:string option, persistantId:string option, orientation:Orientation) = 
    inherit Location(uri, name, displayId, version, persistantId,orientation)
