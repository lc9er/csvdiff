open System
open Csvdiff

[<EntryPoint>]
let main argv =
    let baseFile = ReadFile.fetchLines argv.[0]
    let deltaFile = ReadFile.fetchLines argv.[1]
    let separator = ","

    let parsedBaseFile = ParseCsv.parseLines baseFile separator
    let parsedDeltaFile = ParseCsv.parseLines baseFile separator

    parsedBaseFile
    |> Map.iter (fun x -> printfn "%A%A" x)

    parsedDeltaFile
    |> Map.iter (fun x -> printfn "%A%A" x)
    0 // return an integer exit code
