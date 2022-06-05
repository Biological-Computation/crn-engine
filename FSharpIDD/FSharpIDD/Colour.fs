namespace FSharpIDD

open WebSharper

/// This module is to reduce the dependencies count and to ease possible WebSharper compilation
[<JavaScript>]
module Colour =    
    open System

    type RgbColour = {
        R: byte
        G: byte
        B: byte
    }

    type Colour = 
    | Default
    | Rgb of RgbColour    

    let createColour red green blue = Rgb {R=red; G=green;B=blue}

    let private namesToColours =
        let m = Map.empty
        let m = Map.add "AliceBlue"             (createColour (byte 0xF0) (byte 0xF8) (byte 0xFF)) m
        let m = Map.add "AntiqueWhite"          (createColour (byte 0xFA) (byte 0xEB) (byte 0xD7)) m
        let m = Map.add "Aqua"                  (createColour (byte 0x00) (byte 0xFF) (byte 0xFF)) m
        let m = Map.add "Aquamarine"            (createColour (byte 0x7F) (byte 0xFF) (byte 0xD4)) m
        let m = Map.add "Azure"                 (createColour (byte 0xF0) (byte 0xFF) (byte 0xFF)) m
        let m = Map.add "Beige"                 (createColour (byte 0xF5) (byte 0xF5) (byte 0xDC)) m
        let m = Map.add "Bisque"                (createColour (byte 0xFF) (byte 0xE4) (byte 0xC4)) m
        let m = Map.add "Black"                 (createColour (byte 0x00) (byte 0x00) (byte 0x00)) m
        let m = Map.add "BlanchedAlmond"        (createColour (byte 0xFF) (byte 0xEB) (byte 0xCD)) m
        let m = Map.add "Blue"                  (createColour (byte 0x00) (byte 0x00) (byte 0xFF)) m
        let m = Map.add "BlueViolet"            (createColour (byte 0x8A) (byte 0x2B) (byte 0xE2)) m
        let m = Map.add "Brown"                 (createColour (byte 0xA5) (byte 0x2A) (byte 0x2A)) m
        let m = Map.add "BurlyWood"             (createColour (byte 0xDE) (byte 0xB8) (byte 0x87)) m
        let m = Map.add "CadetBlue"             (createColour (byte 0x5F) (byte 0x9E) (byte 0xA0)) m
        let m = Map.add "Chartreuse"            (createColour (byte 0x7F) (byte 0xFF) (byte 0x00)) m
        let m = Map.add "Chocolate"             (createColour (byte 0xD2) (byte 0x69) (byte 0x1E)) m
        let m = Map.add "Coral"                 (createColour (byte 0xFF) (byte 0x7F) (byte 0x50)) m
        let m = Map.add "CornflowerBlue"        (createColour (byte 0x64) (byte 0x95) (byte 0xED)) m
        let m = Map.add "Cornsilk"              (createColour (byte 0xFF) (byte 0xF8) (byte 0xDC)) m
        let m = Map.add "Crimson"               (createColour (byte 0xDC) (byte 0x14) (byte 0x3C)) m
        let m = Map.add "Cyan"                  (createColour (byte 0x00) (byte 0xFF) (byte 0xFF)) m
        let m = Map.add "DarkBlue"              (createColour (byte 0x00) (byte 0x00) (byte 0x8B)) m
        let m = Map.add "DarkCyan"              (createColour (byte 0x00) (byte 0x8B) (byte 0x8B)) m
        let m = Map.add "DarkGoldenRod"         (createColour (byte 0xB8) (byte 0x86) (byte 0x0B)) m
        let m = Map.add "DarkGray"              (createColour (byte 0xA9) (byte 0xA9) (byte 0xA9)) m
        let m = Map.add "DarkGrey"              (createColour (byte 0xA9) (byte 0xA9) (byte 0xA9)) m
        let m = Map.add "DarkGreen"             (createColour (byte 0x00) (byte 0x64) (byte 0x00)) m
        let m = Map.add "DarkKhaki"             (createColour (byte 0xBD) (byte 0xB7) (byte 0x6B)) m
        let m = Map.add "DarkMagenta"           (createColour (byte 0x8B) (byte 0x00) (byte 0x8B)) m
        let m = Map.add "DarkOliveGreen"        (createColour (byte 0x55) (byte 0x6B) (byte 0x2F)) m
        let m = Map.add "DarkOrange"            (createColour (byte 0xFF) (byte 0x8C) (byte 0x00)) m
        let m = Map.add "DarkOrchid"            (createColour (byte 0x99) (byte 0x32) (byte 0xCC)) m
        let m = Map.add "DarkRed"               (createColour (byte 0x8B) (byte 0x00) (byte 0x00)) m
        let m = Map.add "DarkSalmon"            (createColour (byte 0xE9) (byte 0x96) (byte 0x7A)) m
        let m = Map.add "DarkSeaGreen"          (createColour (byte 0x8F) (byte 0xBC) (byte 0x8F)) m
        let m = Map.add "DarkSlateBlue"         (createColour (byte 0x48) (byte 0x3D) (byte 0x8B)) m
        let m = Map.add "DarkSlateGray"         (createColour (byte 0x2F) (byte 0x4F) (byte 0x4F)) m
        let m = Map.add "DarkSlateGrey"         (createColour (byte 0x2F) (byte 0x4F) (byte 0x4F)) m
        let m = Map.add "DarkTurquoise"         (createColour (byte 0x00) (byte 0xCE) (byte 0xD1)) m
        let m = Map.add "DarkViolet"            (createColour (byte 0x94) (byte 0x00) (byte 0xD3)) m
        let m = Map.add "DeepPink"              (createColour (byte 0xFF) (byte 0x14) (byte 0x93)) m
        let m = Map.add "DeepSkyBlue"           (createColour (byte 0x00) (byte 0xBF) (byte 0xFF)) m
        let m = Map.add "DimGray"               (createColour (byte 0x69) (byte 0x69) (byte 0x69)) m
        let m = Map.add "DimGrey"               (createColour (byte 0x69) (byte 0x69) (byte 0x69)) m
        let m = Map.add "DodgerBlue"            (createColour (byte 0x1E) (byte 0x90) (byte 0xFF)) m
        let m = Map.add "FireBrick"             (createColour (byte 0xB2) (byte 0x22) (byte 0x22)) m
        let m = Map.add "FloralWhite"           (createColour (byte 0xFF) (byte 0xFA) (byte 0xF0)) m
        let m = Map.add "ForestGreen"           (createColour (byte 0x22) (byte 0x8B) (byte 0x22)) m
        let m = Map.add "Fuchsia"               (createColour (byte 0xFF) (byte 0x00) (byte 0xFF)) m
        let m = Map.add "Gainsboro"             (createColour (byte 0xDC) (byte 0xDC) (byte 0xDC)) m
        let m = Map.add "GhostWhite"            (createColour (byte 0xF8) (byte 0xF8) (byte 0xFF)) m
        let m = Map.add "Gold"                  (createColour (byte 0xFF) (byte 0xD7) (byte 0x00)) m
        let m = Map.add "GoldenRod"             (createColour (byte 0xDA) (byte 0xA5) (byte 0x20)) m
        let m = Map.add "Gray"                  (createColour (byte 0x80) (byte 0x80) (byte 0x80)) m
        let m = Map.add "Grey"                  (createColour (byte 0x80) (byte 0x80) (byte 0x80)) m
        let m = Map.add "Green"                 (createColour (byte 0x00) (byte 0x80) (byte 0x00)) m
        let m = Map.add "GreenYellow"           (createColour (byte 0xAD) (byte 0xFF) (byte 0x2F)) m
        let m = Map.add "HoneyDew"              (createColour (byte 0xF0) (byte 0xFF) (byte 0xF0)) m
        let m = Map.add "HotPink"               (createColour (byte 0xFF) (byte 0x69) (byte 0xB4)) m
        let m = Map.add "IndianRed"             (createColour (byte 0xCD) (byte 0x5C) (byte 0x5C)) m
        let m = Map.add "Indigo"                (createColour (byte 0x4B) (byte 0x00) (byte 0x82)) m
        let m = Map.add "Ivory"                 (createColour (byte 0xFF) (byte 0xFF) (byte 0xF0)) m
        let m = Map.add "Khaki"                 (createColour (byte 0xF0) (byte 0xE6) (byte 0x8C)) m
        let m = Map.add "Lavender"              (createColour (byte 0xE6) (byte 0xE6) (byte 0xFA)) m
        let m = Map.add "LavenderBlush"         (createColour (byte 0xFF) (byte 0xF0) (byte 0xF5)) m
        let m = Map.add "LawnGreen"             (createColour (byte 0x7C) (byte 0xFC) (byte 0x00)) m
        let m = Map.add "LemonChiffon"          (createColour (byte 0xFF) (byte 0xFA) (byte 0xCD)) m
        let m = Map.add "LightBlue"             (createColour (byte 0xAD) (byte 0xD8) (byte 0xE6)) m
        let m = Map.add "LightCoral"            (createColour (byte 0xF0) (byte 0x80) (byte 0x80)) m
        let m = Map.add "LightCyan"             (createColour (byte 0xE0) (byte 0xFF) (byte 0xFF)) m
        let m = Map.add "LightGoldenRodYellow"  (createColour (byte 0xFA) (byte 0xFA) (byte 0xD2)) m
        let m = Map.add "LightGray"             (createColour (byte 0xD3) (byte 0xD3) (byte 0xD3)) m
        let m = Map.add "LightGrey"             (createColour (byte 0xD3) (byte 0xD3) (byte 0xD3)) m
        let m = Map.add "LightGreen"            (createColour (byte 0x90) (byte 0xEE) (byte 0x90)) m
        let m = Map.add "LightPink"             (createColour (byte 0xFF) (byte 0xB6) (byte 0xC1)) m
        let m = Map.add "LightSalmon"           (createColour (byte 0xFF) (byte 0xA0) (byte 0x7A)) m
        let m = Map.add "LightSeaGreen"         (createColour (byte 0x20) (byte 0xB2) (byte 0xAA)) m
        let m = Map.add "LightSkyBlue"          (createColour (byte 0x87) (byte 0xCE) (byte 0xFA)) m
        let m = Map.add "LightSlateGray"        (createColour (byte 0x77) (byte 0x88) (byte 0x99)) m
        let m = Map.add "LightSlateGrey"        (createColour (byte 0x77) (byte 0x88) (byte 0x99)) m
        let m = Map.add "LightSteelBlue"        (createColour (byte 0xB0) (byte 0xC4) (byte 0xDE)) m
        let m = Map.add "LightYellow"           (createColour (byte 0xFF) (byte 0xFF) (byte 0xE0)) m
        let m = Map.add "Lime"                  (createColour (byte 0x00) (byte 0xFF) (byte 0x00)) m
        let m = Map.add "LimeGreen"             (createColour (byte 0x32) (byte 0xCD) (byte 0x32)) m
        let m = Map.add "Linen"                 (createColour (byte 0xFA) (byte 0xF0) (byte 0xE6)) m
        let m = Map.add "Magenta"               (createColour (byte 0xFF) (byte 0x00) (byte 0xFF)) m
        let m = Map.add "Maroon"                (createColour (byte 0x80) (byte 0x00) (byte 0x00)) m
        let m = Map.add "MediumAquaMarine"      (createColour (byte 0x66) (byte 0xCD) (byte 0xAA)) m
        let m = Map.add "MediumBlue"            (createColour (byte 0x00) (byte 0x00) (byte 0xCD)) m
        let m = Map.add "MediumOrchid"          (createColour (byte 0xBA) (byte 0x55) (byte 0xD3)) m
        let m = Map.add "MediumPurple"          (createColour (byte 0x93) (byte 0x70) (byte 0xDB)) m
        let m = Map.add "MediumSeaGreen"        (createColour (byte 0x3C) (byte 0xB3) (byte 0x71)) m
        let m = Map.add "MediumSlateBlue"       (createColour (byte 0x7B) (byte 0x68) (byte 0xEE)) m
        let m = Map.add "MediumSpringGreen"     (createColour (byte 0x00) (byte 0xFA) (byte 0x9A)) m
        let m = Map.add "MediumTurquoise"       (createColour (byte 0x48) (byte 0xD1) (byte 0xCC)) m
        let m = Map.add "MediumVioletRed"       (createColour (byte 0xC7) (byte 0x15) (byte 0x85)) m
        let m = Map.add "MidnightBlue"          (createColour (byte 0x19) (byte 0x19) (byte 0x70)) m
        let m = Map.add "MintCream"             (createColour (byte 0xF5) (byte 0xFF) (byte 0xFA)) m
        let m = Map.add "MistyRose"             (createColour (byte 0xFF) (byte 0xE4) (byte 0xE1)) m
        let m = Map.add "Moccasin"              (createColour (byte 0xFF) (byte 0xE4) (byte 0xB5)) m
        let m = Map.add "NavajoWhite"           (createColour (byte 0xFF) (byte 0xDE) (byte 0xAD)) m
        let m = Map.add "Navy"                  (createColour (byte 0x00) (byte 0x00) (byte 0x80)) m
        let m = Map.add "OldLace"               (createColour (byte 0xFD) (byte 0xF5) (byte 0xE6)) m
        let m = Map.add "Olive"                 (createColour (byte 0x80) (byte 0x80) (byte 0x00)) m
        let m = Map.add "OliveDrab"             (createColour (byte 0x6B) (byte 0x8E) (byte 0x23)) m
        let m = Map.add "Orange"                (createColour (byte 0xFF) (byte 0xA5) (byte 0x00)) m
        let m = Map.add "OrangeRed"             (createColour (byte 0xFF) (byte 0x45) (byte 0x00)) m
        let m = Map.add "Orchid"                (createColour (byte 0xDA) (byte 0x70) (byte 0xD6)) m
        let m = Map.add "PaleGoldenRod"         (createColour (byte 0xEE) (byte 0xE8) (byte 0xAA)) m
        let m = Map.add "PaleGreen"             (createColour (byte 0x98) (byte 0xFB) (byte 0x98)) m
        let m = Map.add "PaleTurquoise"         (createColour (byte 0xAF) (byte 0xEE) (byte 0xEE)) m
        let m = Map.add "PaleVioletRed"         (createColour (byte 0xDB) (byte 0x70) (byte 0x93)) m
        let m = Map.add "PapayaWhip"            (createColour (byte 0xFF) (byte 0xEF) (byte 0xD5)) m
        let m = Map.add "PeachPuff"             (createColour (byte 0xFF) (byte 0xDA) (byte 0xB9)) m
        let m = Map.add "Peru"                  (createColour (byte 0xCD) (byte 0x85) (byte 0x3F)) m
        let m = Map.add "Pink"                  (createColour (byte 0xFF) (byte 0xC0) (byte 0xCB)) m
        let m = Map.add "Plum"                  (createColour (byte 0xDD) (byte 0xA0) (byte 0xDD)) m
        let m = Map.add "PowderBlue"            (createColour (byte 0xB0) (byte 0xE0) (byte 0xE6)) m
        let m = Map.add "Purple"                (createColour (byte 0x80) (byte 0x00) (byte 0x80)) m
        let m = Map.add "RebeccaPurple"         (createColour (byte 0x66) (byte 0x33) (byte 0x99)) m
        let m = Map.add "Red"                   (createColour (byte 0xFF) (byte 0x00) (byte 0x00)) m
        let m = Map.add "RosyBrown"             (createColour (byte 0xBC) (byte 0x8F) (byte 0x8F)) m
        let m = Map.add "RoyalBlue"             (createColour (byte 0x41) (byte 0x69) (byte 0xE1)) m
        let m = Map.add "SaddleBrown"           (createColour (byte 0x8B) (byte 0x45) (byte 0x13)) m
        let m = Map.add "Salmon"                (createColour (byte 0xFA) (byte 0x80) (byte 0x72)) m
        let m = Map.add "SandyBrown"            (createColour (byte 0xF4) (byte 0xA4) (byte 0x60)) m
        let m = Map.add "SeaGreen"              (createColour (byte 0x2E) (byte 0x8B) (byte 0x57)) m
        let m = Map.add "SeaShell"              (createColour (byte 0xFF) (byte 0xF5) (byte 0xEE)) m
        let m = Map.add "Sienna"                (createColour (byte 0xA0) (byte 0x52) (byte 0x2D)) m
        let m = Map.add "Silver"                (createColour (byte 0xC0) (byte 0xC0) (byte 0xC0)) m
        let m = Map.add "SkyBlue"               (createColour (byte 0x87) (byte 0xCE) (byte 0xEB)) m
        let m = Map.add "SlateBlue"             (createColour (byte 0x6A) (byte 0x5A) (byte 0xCD)) m
        let m = Map.add "SlateGray"             (createColour (byte 0x70) (byte 0x80) (byte 0x90)) m
        let m = Map.add "SlateGrey"             (createColour (byte 0x70) (byte 0x80) (byte 0x90)) m
        let m = Map.add "Snow"                  (createColour (byte 0xFF) (byte 0xFA) (byte 0xFA)) m
        let m = Map.add "SpringGreen"           (createColour (byte 0x00) (byte 0xFF) (byte 0x7F)) m
        let m = Map.add "SteelBlue"             (createColour (byte 0x46) (byte 0x82) (byte 0xB4)) m
        let m = Map.add "Tan"                   (createColour (byte 0xD2) (byte 0xB4) (byte 0x8C)) m
        let m = Map.add "Teal"                  (createColour (byte 0x00) (byte 0x80) (byte 0x80)) m
        let m = Map.add "Thistle"               (createColour (byte 0xD8) (byte 0xBF) (byte 0xD8)) m
        let m = Map.add "Tomato"                (createColour (byte 0xFF) (byte 0x63) (byte 0x47)) m
        let m = Map.add "Turquoise"             (createColour (byte 0x40) (byte 0xE0) (byte 0xD0)) m
        let m = Map.add "Violet"                (createColour (byte 0xEE) (byte 0x82) (byte 0xEE)) m
        let m = Map.add "Wheat"                 (createColour (byte 0xF5) (byte 0xDE) (byte 0xB3)) m
        let m = Map.add "White"                 (createColour (byte 0xFF) (byte 0xFF) (byte 0xFF)) m
        let m = Map.add "WhiteSmoke"            (createColour (byte 0xF5) (byte 0xF5) (byte 0xF5)) m
        let m = Map.add "Yellow"                (createColour (byte 0xFF) (byte 0xFF) (byte 0x00)) m
        let m = Map.add "YellowGreen"           (createColour (byte 0x9A) (byte 0xCD) (byte 0x32)) m
        m
    
    [<Inline "parseInt($0, 16)">]
    let private parseHex hexStr =
        System.Convert.ToUInt32(hexStr,16)        

    let parse colourString =
        let (|RGB|_|) (str: string) = 
            let tryParseHex hexStr = 
                try
                    Some(parseHex hexStr|> byte)
                with
                | :? ArgumentException | :? ArgumentOutOfRangeException | :? FormatException | :? OverflowException -> None            
            if (str.Length = 7) && (str.[0] = '#') then
                //#RRGGBB
                let r = tryParseHex (str.Substring(1,2))
                let g = tryParseHex (str.Substring(3,2))
                let b = tryParseHex (str.Substring(5,2))
                match r,g,b with
                |   Some(r),Some(g),Some(b) -> Some(Rgb {R=r; G=g; B=b})
                |   _ -> None                    
            else None
        let (|NamedColour|_|) (str:string) =
            Map.tryFind str namesToColours
        match colourString with
        |   RGB colour | NamedColour colour -> Some(colour)
        |   _   -> None

    
    let AliceBlue               = Map.find "AliceBlue"            namesToColours 
    let AntiqueWhite            = Map.find "AntiqueWhite"         namesToColours 
    let Aqua                    = Map.find "Aqua"                 namesToColours 
    let Aquamarine              = Map.find "Aquamarine"           namesToColours 
    let Azure                   = Map.find "Azure"                namesToColours 
    let Beige                   = Map.find "Beige"                namesToColours 
    let Bisque                  = Map.find "Bisque"               namesToColours 
    let Black                   = Map.find "Black"                namesToColours 
    let BlanchedAlmond          = Map.find "BlanchedAlmond"       namesToColours 
    let Blue                    = Map.find "Blue"                 namesToColours 
    let BlueViolet              = Map.find "BlueViolet"           namesToColours 
    let Brown                   = Map.find "Brown"                namesToColours 
    let BurlyWood               = Map.find "BurlyWood"            namesToColours 
    let CadetBlue               = Map.find "CadetBlue"            namesToColours 
    let Chartreuse              = Map.find "Chartreuse"           namesToColours 
    let Chocolate               = Map.find "Chocolate"            namesToColours 
    let Coral                   = Map.find "Coral"                namesToColours 
    let CornflowerBlue          = Map.find "CornflowerBlue"       namesToColours 
    let Cornsilk                = Map.find "Cornsilk"             namesToColours 
    let Crimson                 = Map.find "Crimson"              namesToColours 
    let Cyan                    = Map.find "Cyan"                 namesToColours 
    let DarkBlue                = Map.find "DarkBlue"             namesToColours 
    let DarkCyan                = Map.find "DarkCyan"             namesToColours 
    let DarkGoldenRod           = Map.find "DarkGoldenRod"        namesToColours 
    let DarkGray                = Map.find "DarkGray"             namesToColours 
    let DarkGrey                = Map.find "DarkGrey"             namesToColours 
    let DarkGreen               = Map.find "DarkGreen"            namesToColours 
    let DarkKhaki               = Map.find "DarkKhaki"            namesToColours 
    let DarkMagenta             = Map.find "DarkMagenta"          namesToColours 
    let DarkOliveGreen          = Map.find "DarkOliveGreen"       namesToColours 
    let DarkOrange              = Map.find "DarkOrange"           namesToColours 
    let DarkOrchid              = Map.find "DarkOrchid"           namesToColours 
    let DarkRed                 = Map.find "DarkRed"              namesToColours 
    let DarkSalmon              = Map.find "DarkSalmon"           namesToColours 
    let DarkSeaGreen            = Map.find "DarkSeaGreen"         namesToColours 
    let DarkSlateBlue           = Map.find "DarkSlateBlue"        namesToColours 
    let DarkSlateGray           = Map.find "DarkSlateGray"        namesToColours 
    let DarkSlateGrey           = Map.find "DarkSlateGrey"        namesToColours 
    let DarkTurquoise           = Map.find "DarkTurquoise"        namesToColours 
    let DarkViolet              = Map.find "DarkViolet"           namesToColours 
    let DeepPink                = Map.find "DeepPink"             namesToColours 
    let DeepSkyBlue             = Map.find "DeepSkyBlue"          namesToColours 
    let DimGray                 = Map.find "DimGray"              namesToColours 
    let DimGrey                 = Map.find "DimGrey"              namesToColours 
    let DodgerBlue              = Map.find "DodgerBlue"           namesToColours 
    let FireBrick               = Map.find "FireBrick"            namesToColours 
    let FloralWhite             = Map.find "FloralWhite"          namesToColours 
    let ForestGreen             = Map.find "ForestGreen"          namesToColours 
    let Fuchsia                 = Map.find "Fuchsia"              namesToColours 
    let Gainsboro               = Map.find "Gainsboro"            namesToColours 
    let GhostWhite              = Map.find "GhostWhite"           namesToColours 
    let Gold                    = Map.find "Gold"                 namesToColours 
    let GoldenRod               = Map.find "GoldenRod"            namesToColours 
    let Gray                    = Map.find "Gray"                 namesToColours 
    let Grey                    = Map.find "Grey"                 namesToColours 
    let Green                   = Map.find "Green"                namesToColours 
    let GreenYellow             = Map.find "GreenYellow"          namesToColours 
    let HoneyDew                = Map.find "HoneyDew"             namesToColours 
    let HotPink                 = Map.find "HotPink"              namesToColours 
    let IndianRed               = Map.find "IndianRed"            namesToColours 
    let Indigo                  = Map.find "Indigo"               namesToColours 
    let Ivory                   = Map.find "Ivory"                namesToColours 
    let Khaki                   = Map.find "Khaki"                namesToColours 
    let Lavender                = Map.find "Lavender"             namesToColours 
    let LavenderBlush           = Map.find "LavenderBlush"        namesToColours 
    let LawnGreen               = Map.find "LawnGreen"            namesToColours 
    let LemonChiffon            = Map.find "LemonChiffon"         namesToColours 
    let LightBlue               = Map.find "LightBlue"            namesToColours 
    let LightCoral              = Map.find "LightCoral"           namesToColours 
    let LightCyan               = Map.find "LightCyan"            namesToColours 
    let LightGoldenRodYellow    = Map.find "LightGoldenRodYellow" namesToColours 
    let LightGray               = Map.find "LightGray"            namesToColours 
    let LightGrey               = Map.find "LightGrey"            namesToColours 
    let LightGreen              = Map.find "LightGreen"           namesToColours 
    let LightPink               = Map.find "LightPink"            namesToColours 
    let LightSalmon             = Map.find "LightSalmon"          namesToColours 
    let LightSeaGreen           = Map.find "LightSeaGreen"        namesToColours 
    let LightSkyBlue            = Map.find "LightSkyBlue"         namesToColours 
    let LightSlateGray          = Map.find "LightSlateGray"       namesToColours 
    let LightSlateGrey          = Map.find "LightSlateGrey"       namesToColours 
    let LightSteelBlue          = Map.find "LightSteelBlue"       namesToColours 
    let LightYellow             = Map.find "LightYellow"          namesToColours 
    let Lime                    = Map.find "Lime"                 namesToColours 
    let LimeGreen               = Map.find "LimeGreen"            namesToColours 
    let Linen                   = Map.find "Linen"                namesToColours 
    let Magenta                 = Map.find "Magenta"              namesToColours 
    let Maroon                  = Map.find "Maroon"               namesToColours 
    let MediumAquaMarine        = Map.find "MediumAquaMarine"     namesToColours 
    let MediumBlue              = Map.find "MediumBlue"           namesToColours 
    let MediumOrchid            = Map.find "MediumOrchid"         namesToColours 
    let MediumPurple            = Map.find "MediumPurple"         namesToColours 
    let MediumSeaGreen          = Map.find "MediumSeaGreen"       namesToColours 
    let MediumSlateBlue         = Map.find "MediumSlateBlue"      namesToColours 
    let MediumSpringGreen       = Map.find "MediumSpringGreen"    namesToColours 
    let MediumTurquoise         = Map.find "MediumTurquoise"      namesToColours 
    let MediumVioletRed         = Map.find "MediumVioletRed"      namesToColours 
    let MidnightBlue            = Map.find "MidnightBlue"         namesToColours 
    let MintCream               = Map.find "MintCream"            namesToColours 
    let MistyRose               = Map.find "MistyRose"            namesToColours 
    let Moccasin                = Map.find "Moccasin"             namesToColours 
    let NavajoWhite             = Map.find "NavajoWhite"          namesToColours 
    let Navy                    = Map.find "Navy"                 namesToColours 
    let OldLace                 = Map.find "OldLace"              namesToColours 
    let Olive                   = Map.find "Olive"                namesToColours 
    let OliveDrab               = Map.find "OliveDrab"            namesToColours 
    let Orange                  = Map.find "Orange"               namesToColours 
    let OrangeRed               = Map.find "OrangeRed"            namesToColours 
    let Orchid                  = Map.find "Orchid"               namesToColours 
    let PaleGoldenRod           = Map.find "PaleGoldenRod"        namesToColours 
    let PaleGreen               = Map.find "PaleGreen"            namesToColours 
    let PaleTurquoise           = Map.find "PaleTurquoise"        namesToColours 
    let PaleVioletRed           = Map.find "PaleVioletRed"        namesToColours 
    let PapayaWhip              = Map.find "PapayaWhip"           namesToColours 
    let PeachPuff               = Map.find "PeachPuff"            namesToColours 
    let Peru                    = Map.find "Peru"                 namesToColours 
    let Pink                    = Map.find "Pink"                 namesToColours 
    let Plum                    = Map.find "Plum"                 namesToColours 
    let PowderBlue              = Map.find "PowderBlue"           namesToColours 
    let Purple                  = Map.find "Purple"               namesToColours 
    let RebeccaPurple           = Map.find "RebeccaPurple"        namesToColours 
    let Red                     = Map.find "Red"                  namesToColours 
    let RosyBrown               = Map.find "RosyBrown"            namesToColours 
    let RoyalBlue               = Map.find "RoyalBlue"            namesToColours 
    let SaddleBrown             = Map.find "SaddleBrown"          namesToColours 
    let Salmon                  = Map.find "Salmon"               namesToColours 
    let SandyBrown              = Map.find "SandyBrown"           namesToColours 
    let SeaGreen                = Map.find "SeaGreen"             namesToColours 
    let SeaShell                = Map.find "SeaShell"             namesToColours 
    let Sienna                  = Map.find "Sienna"               namesToColours 
    let Silver                  = Map.find "Silver"               namesToColours 
    let SkyBlue                 = Map.find "SkyBlue"              namesToColours 
    let SlateBlue               = Map.find "SlateBlue"            namesToColours 
    let SlateGray               = Map.find "SlateGray"            namesToColours 
    let SlateGrey               = Map.find "SlateGrey"            namesToColours 
    let Snow                    = Map.find "Snow"                 namesToColours 
    let SpringGreen             = Map.find "SpringGreen"          namesToColours 
    let SteelBlue               = Map.find "SteelBlue"            namesToColours 
    let Tan                     = Map.find "Tan"                  namesToColours 
    let Teal                    = Map.find "Teal"                 namesToColours 
    let Thistle                 = Map.find "Thistle"              namesToColours 
    let Tomato                  = Map.find "Tomato"               namesToColours 
    let Turquoise               = Map.find "Turquoise"            namesToColours 
    let Violet                  = Map.find "Violet"               namesToColours 
    let Wheat                   = Map.find "Wheat"                namesToColours 
    let White                   = Map.find "White"                namesToColours 
    let WhiteSmoke              = Map.find "WhiteSmoke"           namesToColours 
    let Yellow                  = Map.find "Yellow"               namesToColours 
    let YellowGreen             = Map.find "YellowGreen"          namesToColours 