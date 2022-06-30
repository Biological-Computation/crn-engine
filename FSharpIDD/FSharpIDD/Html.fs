namespace FSharpIDD

open WebSharper

[<JavaScript>]
module HTML = 
    let ofChart chart =
        let structure = HtmlConverters.toHtmlStructure chart
        DOM.toString structure
    let ofSubplots subplots =
        let structure = Subplots.subplotsToHtmlStructure subplots
        DOM.toString structure