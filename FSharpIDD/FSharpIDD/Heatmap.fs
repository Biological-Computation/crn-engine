namespace FSharpIDD.Plots

open FSharpIDD
open WebSharper

[<JavaScript>]
module Heatmap =
    type Palette =
    |   IddPaletteString of string
    |   Default
    /// Heatmap plot settings
    type Plot = {
        /// Specifies how to annotate Heatmap plot in a legend
        Name: string
        /// X coords of grid points. If missing is considered to be sequential integers. Should have at least 2 elements
        X: float[]
        /// Y coords of grid points. If missing is considered to be sequential integers. Should have at least 2 elements
        Y: float[]
        /// Two-dimensional array of values in grid points. If value is NaN, the point is skipped
        Data: float[,]
        /// Colour palette
        Palette: Palette
        /// Heatmap transparency in [0, 1] domain, where 0 - transparent, 1 - opaque
        Opacity: float
    }

    type Options() =
        let mutable name: string option = None
        let mutable palette: string option = None
        let mutable opacity: float option = None

        member s.Name with set v = name <- Some(v)                
        member s.SpecifiedName with internal get() = name
        member s.Palette with set v = palette <- Some(v)
        member s.SpecifiedPalette with internal get() = palette
        member s.Opacity with set v = opacity <- Some(v)
        member s.SpecifiedOpacity with internal get() = opacity
    
    /// sets several heatmap options at once
    let setOptions (options:Options) heatmap =
        let heatmap =
            match options.SpecifiedName with
            | None -> heatmap
            | Some(name) -> {heatmap with Name = name}
        let heatmap =
            match options.SpecifiedPalette with
            | None -> heatmap
            | Some(palette) -> {heatmap with Palette = IddPaletteString palette}
        let heatmap =
            match options.SpecifiedOpacity with
            | None -> heatmap
            | Some(opacity) -> {heatmap with Opacity = opacity}
        heatmap
        
    /// Creates a basic heatmap using the specified set of X, Y coords and data array
    let createHeatmap x y data = 
            {
                Name = null
                X = x
                Y = y
                Data = data
                Palette = Palette.Default
                Opacity = 1.0
            }
        
    /// Changes name of the heatmap (how it is depicted in the legend)
    let setName name heatmap =
        {
            heatmap with
                Name = name
        }
        
    /// Changes palette of the heatmap
    let setPalette palette heatmap =
        {
            heatmap with
                Palette = palette
        }
        
    /// Changes opacity of the heatmap
    let setOpacity opacity heatmap =
        {
            heatmap with
                Opacity = opacity
        }