/// This module mimics the WebSharper API to make the rest of the code use the same syntax in both .NET and WebSharper enabled compilations
module WebSharper

open System

[<AttributeUsage(AttributeTargets.All)>]
type JavaScriptAttribute() =
    class
    end

[<AttributeUsage(AttributeTargets.All)>]
type InlineAttribute(expression) =
    class
    end