namespace FSharpIDD.Plots

open FSharpIDD
open WebSharper

[<JavaScript>]
module Bars =
    type Shadow =
    |   WithoutShadow
    |   WithShadow of Colour.Colour

    /// Bars plot settings
    type Plot = {
        /// Specifies how to annotate Bars plot in a legend
        Name: string
        /// Series of bar centers horizontal coordinates. Length of the series equals the number of bars
        BarCenters: DataSeries
        /// Series of bar heights. Length of the series equals the number of bars
        BarHeights: DataSeries
        /// The width in plot coordinates of one bar in a bar chart plot
        BarWidth: float
        /// The colour with which a bar will be filled
        FillColour: Colour.Colour
        /// The colour of a bar border
        BorderColour: Colour.Colour
        /// Shadow mode: with or without shadow, shadow colour
        Shadow: Shadow
    }
        
    type Options() =
        let mutable name: string option = None
        let mutable fillcolour: Colour.Colour option = None
        let mutable bordercolour: Colour.Colour option = None
        let mutable shadow: Shadow option = None
        let mutable barwidth: float option = None

        member s.Name with set v = name <- Some(v)                
        member s.SpecifiedName with internal get() = name
        member s.FillColour with set v = fillcolour <- Some(v)
        member s.SpecifiedFillColour with internal get() = fillcolour
        member s.BorderColour with set v = bordercolour <- Some(v)
        member s.SpecifiedBorderColour with internal get() = bordercolour
        member s.Shadow with set v = shadow <- Some(v)
        member s.SpecifiedShadow with internal get() = shadow
        member s.BarWidth with set v = barwidth <- Some(v)
        member s.SpecifiedBarWidth with internal get() = barwidth
    
    /// sets several bar chart options at once
    let setOptions (options:Options) barchart =
        let barchart =
            match options.SpecifiedName with
            | None -> barchart
            | Some(name) -> {barchart with Name = name}
        let barchart =
            match options.SpecifiedFillColour with
            | None -> barchart
            | Some(fillcolour) -> {barchart with FillColour = fillcolour}
        let barchart =
            match options.SpecifiedBorderColour with
            | None -> barchart
            | Some(bordercolour) -> {barchart with BorderColour = bordercolour}
        let barchart =
            match options.SpecifiedBarWidth with
            | None -> barchart
            | Some(barwidth) -> {barchart with BarWidth = barwidth}
        let barchart =
            match options.SpecifiedShadow with
            | None -> barchart
            | Some(shadow) -> {barchart with Shadow = shadow}
        barchart
        
    /// Creates bar chart plot using the specified set of BarCenters and BarHeights with default settings
    let createBars barcenters barheights = 
            {
                BarCenters = barcenters
                BarHeights = barheights
                Name = null
                FillColour = Colour.Default
                BorderColour = Colour.Default
                Shadow = Shadow.WithoutShadow
                BarWidth = 1.0
            }
        
    /// Changes a colour of bar's borders
    let setBorderColour bordercolour barchart =
        {
            barchart with
                BorderColour = bordercolour
        }
        
    /// Changes a colour with which a bar is filled (how are they depicted in the legend)
    let setFillColour fillcolour barchart =
        {
            barchart with
                FillColour = fillcolour
        }

    /// Sets whether a bar has a shadow and what colour is it
    let setShadow shadow barchart =
        {
            barchart with
                Shadow = shadow
        }

    /// Sets bar width (in plot coords) of a bar
    let setBarWidth barwidth barchart =
        {
            barchart with
                BarWidth = barwidth
        }
                
    /// Changes the name of bar chart plot (how it is depicted in the legend)
    let setName name barChart =
        {
            barChart with
                Name = name
        }
    

