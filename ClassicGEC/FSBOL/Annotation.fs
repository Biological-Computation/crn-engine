[<JavaScript>]
module FSBOL.Annotation


type Literal = 
    | String of string 
    | Integer of int
    | Long of int64
    | Double of double 
    | Boolean of bool
with 
    static member to_string (l:Literal) = 
        match l with 
        | String(string) -> string 
        | Integer(int) -> int.ToString()
        | Long (int64) -> int64.ToString() 
        | Double (double) -> double.ToString() 
        | Boolean (bool) -> bool.ToString()
    static member from_string (str:string) = 
        let boolret = ref false
        if System.Boolean.TryParse(str,boolret) then 
            Boolean(!boolret)
        else 
            let doubleret = ref 0.0
            if System.Double.TryParse(str,doubleret) then 
                Double(!doubleret)
            else 
                let longref = ref (int64 0)
                if System.Int64.TryParse(str,longref) then 
                    Long(!longref) 
                else 
                    let intref = ref 0
                    if System.Int32.TryParse(str,intref) then   
                        Integer(!intref) 
                    else 
                        String(str)

type QName = 
    /// Name
    | Name of string
    /// Qualified Name, NameSpaceURI
    | QualifiedName of (string * string)
    /// Prefix, LocalName, NameSpace URI
    | FullQName of (string * string * string)
with 
    static member equal (q1:QName) (q2:QName) = 
        match q1 with 
        | Name(n1) -> 
            match q2 with 
            | Name(n2) -> (n1 = n2)
            | _ -> false
        | QualifiedName(qn1,nsURI1) -> 
            match q2 with 
            | Name(_) -> false
            | QualifiedName(qn2,nsURI2) -> ((qn1 = qn2) && (nsURI1 = nsURI2))
            | FullQName(prefix2,lcname2,nsURI2) ->  (qn1 =  (prefix2 + ":" + lcname2)) && (nsURI1 = nsURI2)
        | FullQName(prefix1,lcname1,nsURI1) -> 
            match q2 with 
            | Name(_) -> false
            | QualifiedName(qn2,nsURI2) -> (((prefix1 + ":" + lcname1) = qn2) && (nsURI1 = nsURI2))
            | FullQName(prefix2,lcname2,nsURI2) -> ( (prefix1 = prefix2) && (lcname1 = lcname2) && (nsURI1 = nsURI2))


type Annotation (qName:QName, value:AnnotationValue) =         
    member a.qName = qName
    member a.value = value
and AnnotationValue = 
    | Literal of Literal
    | Uri of string 
    | NestedAnnotation of NestedAnnotation 
and NestedAnnotation (nestedQName:QName,nestedURI:string,annotations:Annotation list) =
    member na.nestedQName = nestedQName
    member na.nestedURI = nestedURI 
    member na.annotations = annotations