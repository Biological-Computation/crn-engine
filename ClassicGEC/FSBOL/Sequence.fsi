[<JavaScript>]
module FSBOL.Sequence
open FSBOL.TopLevel

type Encoding = 
   | IUPACDNA
   | IUPACPROTEIN
   | SMILES
   | OtherEncoding of string
   static member fromURI: string -> Encoding
   static member toURI:Encoding -> string

type Sequence = 
    inherit TopLevel

    new : uri:string * name:string option * displayId:string option * version:string option * persistantId:string option * attachments:string list * sequence:string * encoding:Encoding -> Sequence

    (**)
    member elements:string

    (**) 
    member encoding:Encoding