namespace FSharpIDD.Plots


open WebSharper
open FSharpIDD

[<JavaScript>]
module Markers =
    /// Marker primitives
    type Shape =
    |   Box
    |   Circle
    |   Diamond
    |   Cross
    |   Triangle

    /// The single marker plot settings
    type Plot = {
        /// Specifies how to annotate markers in a legend. Null means that name is not set
        Name: string
        /// Series of X coords of markers
        X: DataSeries
        /// Series of Y coords of markers
        Y: DataSeries
        /// Specifies the size of one marker in pixels
        Size: float
        /// The colour with which the colour will be filled
        FillColour: Colour.Colour
        /// The colour of a marker border
        BorderColour: Colour.Colour
        /// The shape of a marker
        Shape: Shape
    }
        
    type Options() =
        let mutable name: string option = None
        let mutable fillcolour: Colour.Colour option = None
        let mutable bordercolour: Colour.Colour option = None
        let mutable shape: Shape option = None
        let mutable size: float option = None

        member s.Name with set v = name <- Some(v)                
        member s.SpecifiedName with internal get() = name
        member s.FillColour with set v = fillcolour <- Some(v)
        member s.SpecifiedFillColour with internal get() = fillcolour
        member s.BorderColour with set v = bordercolour <- Some(v)
        member s.SpecifiedBorderColour with internal get() = bordercolour
        member s.Shape with set v = shape <- Some(v)
        member s.SpecifiedShape with internal get() = shape
        member s.Size with set v = size <- Some(v)
        member s.SpecifiedSize with internal get() = size
    
    /// sets several markers options at once
    let setOptions (options:Options) markers =
        let markers =
            match options.SpecifiedName with
            | None -> markers
            | Some(name) -> {markers with Name = name}
        let markers =
            match options.SpecifiedFillColour with
            | None -> markers
            | Some(fillcolour) -> {markers with FillColour = fillcolour}
        let markers =
            match options.SpecifiedBorderColour with
            | None -> markers
            | Some(bordercolour) -> {markers with BorderColour = bordercolour}
        let markers =
            match options.SpecifiedShape with
            | None -> markers
            | Some(shape) -> {markers with Shape = shape}
        let markers =
            match options.SpecifiedSize with
            | None -> markers
            | Some(size) -> {markers with Size = size}
        markers
        
    /// Creates markers plot using the specified set of X and Y coords with default settings
    let createMarkers x y = 
            {
                X = x
                Y = y
                FillColour = Colour.Default
                BorderColour = Colour.Default
                Name = null
                Shape = Shape.Box
                Size = 10.0
            }
        
    /// Changes the name of markers (how are they depicted in the legend)
    let setName name markers =
        {
            markers with
                Name = name
        }
        
    /// Changes the colour of a marker's border
    let setBorderColour bordercolour markers =
        {
            markers with
                BorderColour = bordercolour
        }
        
    /// Changes the colour with which a marker is filled (how are they depicted in the legend)
    let setFillColour fillcolour markers =
        {
            markers with
                FillColour = fillcolour
        }

    /// Sets the shape of a marker
    let setShape shapeType markers =
        {
            markers with
                Shape = shapeType
        }

    /// Sets the size of a marker
    let setSize size markers =
        {
            markers with
                Size = size
        }


