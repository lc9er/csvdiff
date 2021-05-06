open System
open Csvdiff

[<EntryPoint>]
let main argv =
    let baseFile = ReadFile.fetchLines argv.[0]
    let deltaFile = ReadFile.fetchLines argv.[1]

    baseFile
    |> Array.iter (fun x -> printfn "%s" x)

    deltaFile
    |> Array.iter (fun x -> printfn "%s" x)
    0 // return an integer exit code
