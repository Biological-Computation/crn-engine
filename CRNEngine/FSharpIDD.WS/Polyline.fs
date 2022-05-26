namespace FSharpIDD.Plots

open WebSharper
open FSharpIDD

[<JavaScript>]
module Polyline =

    /// The cap (shape of ending) of the line
    type LineCap =
    |   Butt
    |   Round
    |   Square

    /// The shape of joins of polyline's strait segments
    type LineJoin =
    |   Bevel
    |   Round
    |   Miter

    /// The single polyline settings
    type Plot = {
        /// Specifies how to annotate the polyline in the legend. Null means that name is not set
        Name: string
        /// Series of X coords of the points that form the polyline
        X: DataSeries
        /// Series of Y coords of the points that form the polyline
        Y: DataSeries
        /// The fill colour of the polyline
        Colour: Colour.Colour
        /// The line thickness of the polyline in pixels
        Thickness: float
        /// The cap (shape of ending) of the line
        LineCap: LineCap
        /// The shape of joins of polyline's strait segments
        LineJoin: LineJoin
        }
        
    type Options() =
        let mutable name: string option = None
        let mutable colour: Colour.Colour option = None
        let mutable thickness: float option = None
        let mutable lineCap: LineCap option = None
        let mutable lineJoin: LineJoin option = None

        member s.Name with set v = name <- Some(v)                
        member s.SpecifiedName with internal get() = name
        member s.Colour with set v = colour <- Some(v)
        member s.SpecifiedColour with internal get() = colour
        member s.Thickness with set v = thickness <- Some(v)
        member s.SpecifiedThickness with internal get() = thickness
        member s.LineCap with set v = lineCap <- Some(v)
        member s.SpecifiedLineCap with internal get() = lineCap
        member s.LineJoin with set v = lineJoin <- Some(v)
        member s.SpecifiedLineJoin with internal get() = lineJoin
    
    /// sets several polyline options at once
    let setOptions (options:Options) polyline =
        let polyline =
            match options.SpecifiedName with
            | None -> polyline
            | Some(name) -> {polyline with Name = name}
        let polyline =
            match options.SpecifiedColour with
            | None -> polyline
            | Some(color) -> {polyline with Colour = color}
        let polyline =
            match options.SpecifiedThickness with
            | None -> polyline
            | Some(thickness) -> {polyline with Thickness = thickness}
        let polyline =
            match options.SpecifiedLineCap with
            | None -> polyline
            | Some(cap) -> {polyline with LineCap = cap}
        let polyline =
            match options.SpecifiedLineJoin with
            | None -> polyline
            | Some(join) -> {polyline with LineJoin = join}
        polyline
        
    /// Creates a basic polyline using the specified set of X and Y coords
    let createPolyline x y = 
            {
                X = x
                Y = y
                Colour = Colour.Default
                Thickness = 1.0
                Name = null
                LineCap = LineCap.Butt
                LineJoin = LineJoin.Miter
            }
        
    /// Changes the name of the polyline (how it is depicted in the legend)
    let setName name polyline =
        {
            polyline with
                Name = name
        }

    /// Changes the colour of the polyline
    let setStrokeColour colour polyline =
        {
            polyline with
                Colour = colour
        }
        
    /// Sets the line thickness in pixels
    let setThickness thickness polyline =
        {
            polyline with
                Thickness = thickness
        }
        
    /// Sets the shape of polyline caps (line endings)
    let setLineCap capType polyline =
        {
            polyline with
                LineCap = capType
        }

    /// Sets the shape of polyline strait segment joins
    let setLineJoin joinType polyline =
        {
            polyline with
                LineJoin = joinType
        }


