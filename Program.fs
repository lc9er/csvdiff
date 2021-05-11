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
    let baseKeys = Sets.getSet parsedBaseFile
    let deltaKeys = Sets.getSet parsedDeltaFile

    // Get exclusive and inclusive sets
    let additions = Sets.getSetExclusive deltaKeys baseKeys

    let removals = Sets.getSetExclusive baseKeys deltaKeys

    let inBoth = Sets.getSetBoth baseKeys deltaKeys additions removals

    // Only keep the spots in both where there
    // are modifications
    let modified =
        inBoth
        |> Array.filter (fun x -> parsedBaseFile.[x] <> parsedDeltaFile.[x])

    // Print it
    printfn "Additions (%A):" (Array.length additions)

    additions
    |> Array.iter (fun x -> printfn "+ %A" parsedDeltaFile.[x])

    printfn "Removals (%A):" (Array.length removals)

    removals
    |> Array.iter (fun x -> printfn "- %A" parsedBaseFile.[x])

    printfn "Modified (%A):" (Array.length modified)

    modified
    |> Array.iter
        (fun x ->
            printfn "- %A" parsedBaseFile.[x]
            printfn "+ %A" parsedDeltaFile.[x])

    0 // return an integer exit code
