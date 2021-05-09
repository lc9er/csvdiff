open System
open Csvdiff

[<EntryPoint>]
let main argv =
    // Setup
    let baseFile = ReadFile.fetchLines argv.[0]
    let deltaFile = ReadFile.fetchLines argv.[1]
    let separator = ","
    // let primary = 0

    // Parse data into a map
    let parsedBaseFile = ParseCsv.parseLines baseFile separator
    let parsedDeltaFile = ParseCsv.parseLines deltaFile separator

    // Build line sets 
    let baseKeys = Sets.getSet parsedBaseFile |> Seq.cache
    let deltaKeys = Sets.getSet parsedDeltaFile |> Seq.cache

    // Get exclusive and inclusive sets
    let additions = Sets.getSetExclusive deltaKeys baseKeys
    let removals = Sets.getSetExclusive baseKeys deltaKeys
    let inBoth = Sets.getSetBoth baseKeys deltaKeys
    
    // Only keep the spots in both where there 
    // are modifications
    // let modified = inBoth |> Set.filter (fun x -> parsedBaseFile.[x].Key <> parsedDeltaFile.[x].Key)
    let modified = inBoth |> Set.filter (fun x -> parsedBaseFile.[x] <> parsedDeltaFile.[x])

    // Print it
    printfn "Additions (%i)" additions.Count
    additions
    |> Set.iter (fun x -> printfn "+ %A" parsedDeltaFile.[x])

    printfn "Removals (%i)" removals.Count
    removals
    |> Set.iter (fun x -> printfn "- %A" parsedBaseFile.[x])

    printfn "Modified (%i)" modified.Count
    modified
    |> Set.iter (fun x -> 
                    printfn "- %A" parsedBaseFile.[x]
                    printfn "+ %A" parsedDeltaFile.[x])

    0 // return an integer exit code
