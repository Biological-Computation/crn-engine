namespace FSharpIDD

open WebSharper

[<JavaScript>]
module internal HistogramBuilder =

    open System

    type Histogram = {
        BinCentres : float list
        BinCounters: int list
        BinWidth: float
    }

    let buildHistogram (samples:float seq) binCount =
        let eps = 1e-6
        let left = Seq.min samples - eps
        let right = Seq.max samples + eps //adding and subtracting eps ensures that the boundary bins will contain boundary points
        let width = (right-left)/float(binCount)
        let centres = List.init binCount (fun idx -> left + width*0.5 + float(idx)*width)
        let boundaries = Array.init binCount (fun idx -> left + float(idx)*width)        
        let foundIndices = samples |> Seq.map (fun s -> Array.BinarySearch(boundaries,s))
        let binIndices = foundIndices |> Seq.map (fun idx -> if idx<0 then ~~~idx - 1 else idx)   
        let binCounts = Array.zeroCreate<int> binCount
        binIndices |> Seq.iter (fun (elem:int) -> binCounts.[elem] <- binCounts.[elem] + 1)
        {
            BinCentres = centres
            BinCounters = List.ofArray binCounts
            BinWidth = width
        }



namespace FSharpIDD.Plots

open FSharpIDD
open WebSharper

[<JavaScript>]
module Histogram =
        /// Histogram plot
        type Plot = {        
            /// Specifies how to annotate the histogram in a legend
            Name: string
            /// Samples to calculate the histogram for
            Samples: DataSeries        
            /// The colour of the histogram
            Colour: Colour.Colour
            /// Number of bins in the histogram
            BinCount: int
        }

        /// Creates a histogram plot for the passed data
        let createHistogram samples =
            {
                Name = null
                Samples = samples
                Colour = Colour.Default
                BinCount = 30
            }       

        let setName name hist = {hist with Name = name}
        let setColour colour hist = {hist with Colour = colour}
        let setBinCount count hist = {hist with BinCount = count}
        
        type Options() =
            let mutable name: string option = None
            let mutable colour: Colour.Colour option = None
            let mutable binCount: int option = None

            member s.Name with set v = name <- Some(v)                
            member s.SpecifiedName with internal get() = name
            member s.Colour with set v = colour <- Some(v)
            member s.SpecifiedColour with internal get() = colour
            member s.BinCount with set v = binCount <- Some(v)
            member s.SpecifiedBinCount with internal get() = binCount
    
        /// sets several histogram options at once
        let setOptions (options:Options) histogram =
            let histogram =
                match options.SpecifiedName with
                | None -> histogram
                | Some(name) -> {histogram with Name = name}
            let histogram =
                match options.SpecifiedColour with
                | None -> histogram
                | Some(colour) -> {histogram with Colour = colour}
            let histogram =
                match options.SpecifiedBinCount with
                | None -> histogram
                | Some(binCount) -> {histogram with BinCount = binCount}
            histogram
    