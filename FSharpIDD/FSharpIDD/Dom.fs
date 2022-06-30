namespace FSharpIDD

open WebSharper

/// The module provides basic HTML dom composing needed for IDD declarative chart creation without any external dependencies
[<JavaScript>]
module DOM =

    /// This is guard type holding validated string
    type HtmlString = Valid of string

    type Attribute = {
        Key: HtmlString
        Value: HtmlString
    }

    type TableCell = TD of Node
    and TableRow = Cells of TableCell list
    and Table = Rows of TableRow list
    and Node =
        |   Text of HtmlString
        |   Div of Div
        |   Table of Table
        |   Empty
    and Div = {
            Attributes: Attribute list
            Children:  Node list
        }
    

    let internal guardAttrName (str:string) : HtmlString = 
        let validChars = [|'-';|]
        if str.ToCharArray() |> Array.exists (fun c -> not(System.Char.IsLetterOrDigit(c) || (Array.contains c validChars)) ) then
            raise(System.ArgumentException(sprintf "the string must contain only letters, digits or %A, but %s was passed" validChars str))
        Valid str

    let internal guardAttrValue (str:string): HtmlString =
        // https://html.spec.whatwg.org/multipage/syntax.html#syntax-attribute-value
        // first escaping escape
        let str = str.Replace("\\","\\\\")
        // then escaping double quotes
        let str = str.Replace("\"","\\\"")
        Valid str

    let internal guardText (str:string) : HtmlString = 
        let str = str.Replace(">","&gt;")
        let str = str.Replace("<","&lt;")
        Valid str
        
    let createDiv() =
        {
            Attributes = []
            Children = []
        }

    /// Adds a text inside the div (inner content)
    let addText text div = 
        {
            div with
                Children = (Text (guardText text))::div.Children
        }

    /// Adds a div inside the div (inner html)
    let addInnerHtml htmlString div = 
        {
            div with
                Children = (Text (Valid htmlString))::div.Children
        }

    let addDiv child parent = 
        {
            parent with
                Children = (Div child)::parent.Children
        }

    let addTable table parent =
        {
            parent with
                Children = (Table table)::parent.Children
        }
    
    let addAttribute key value div =    
        {
            div with
                Attributes = {Key = guardAttrName key; Value = guardAttrValue value} :: div.Attributes
        }
    
    let rec toString node = 
            match node with
            |   Text htmlText ->
                match htmlText with
                | Valid text -> text
            |   Div div ->
                let attributeToStr attr = 
                    let key,value = 
                        match attr.Key,attr.Value with
                        | Valid key,Valid value -> key,value
                    sprintf "%s=\"%s\"" key value        

                let attributesStr = 
                    if List.length div.Attributes = 0 then ""
                    else
                        " "+System.String.Join(" ", div.Attributes |> Seq.map attributeToStr)
                let contents = System.String.Join("\n", div.Children |> Seq.rev |> Seq.map toString)
                sprintf "<div%s>%s</div>" attributesStr contents
            |   Table rows ->
                match rows with
                |   Rows rowsList ->
                    let rowsList = Seq.rev rowsList
                    let rowToStr row =
                        match row with
                        |   Cells cells ->
                            let cells = Seq.rev cells
                            let cellToStr cell =
                                match cell with
                                |   TD cellContent ->
                                    sprintf "<td>%s</td>" (toString cellContent)
                            sprintf "<tr>%s</tr>" (System.String.Join("", cells |> Seq.map cellToStr))
                    sprintf "<table class='idd-subplots-table'>%s</table>" (System.String.Join("\n",rowsList |> Seq.map rowToStr))
            |   Empty -> ""