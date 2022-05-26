[<JavaScript>]
module FSBOL.Annotation


type Literal = 
    | String of string 
    | Integer of int
    | Long of int64
    | Double of double 
    | Boolean of bool
with static member to_string: Literal -> string
     static member from_string: string -> Literal

type QName = 
    | Name of string
    | QualifiedName of (string * string)
    | FullQName of (string * string * string)
with static member equal: QName -> QName -> bool


[<Class>]
type Annotation =
    new: qName:QName * value:AnnotationValue -> Annotation
    member qName:QName
    member value:AnnotationValue
and AnnotationValue = 
    | Literal of Literal
    | Uri of string 
    | NestedAnnotation of NestedAnnotation 
and [<Class>] NestedAnnotation =
    new: nestedQName:QName * nestedURI:string * annotations:Annotation list -> NestedAnnotation
    member nestedQName:QName
    member nestedURI:string 
    member annotations:Annotation list
