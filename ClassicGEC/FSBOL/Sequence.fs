[<JavaScript>]
module FSBOL.Sequence
open FSBOL.TopLevel

type Encoding = 
   | IUPACDNA
   | IUPACPROTEIN
   | SMILES
   | OtherEncoding of string
   static member fromURI (s:string) =
     match s with 
     | "http://www.chem.qmul.ac.uk/iubmb/misc/naseq.html" -> IUPACDNA
     | "http://www.chem.qmul.ac.uk/iupac/AminoAcid/" -> IUPACPROTEIN
     | "http://www.opensmiles.org/opensmiles.html" -> SMILES
     | _ -> OtherEncoding(s)
   static member toURI (e:Encoding) = 
     match e with 
     | IUPACDNA -> "http://www.chem.qmul.ac.uk/iubmb/misc/naseq.html"
     | IUPACPROTEIN -> "http://www.chem.qmul.ac.uk/iupac/AminoAcid/"
     | SMILES -> "http://www.opensmiles.org/opensmiles.html" 
     | OtherEncoding(s) -> s 

type Sequence(uri:string,name:string option, displayId:string option, version:string option, persistantId:string option, attachments:string list, sequence:string, encoding:Encoding) = 
    inherit TopLevel(uri, name, displayId, version, persistantId,attachments)

    member x.elements = sequence

    member x.encoding = encoding



