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
    let additions =
        Sets.getSetExclusive deltaKeys baseKeys
        |> Seq.cache

    let removals =
        Sets.getSetExclusive baseKeys deltaKeys
        |> Seq.cache

    let inBoth =
        Sets.getSetBoth baseKeys deltaKeys additions removals
        |> Seq.cache

    // Only keep the spots in both where there
    // are modifications
    let modified =
        inBoth
        |> Seq.filter (fun x -> parsedBaseFile.[x] <> parsedDeltaFile.[x])

    // Print it
    printfn "Additions (%A):" (Seq.length additions)

    additions
    |> Seq.iter (fun x -> printfn "+ %A" parsedDeltaFile.[x])

    printfn "Removals (%A):" (Seq.length removals)

    removals
    |> Seq.iter (fun x -> printfn "- %A" parsedBaseFile.[x])

    printfn "Modified (%A):" (Seq.length modified)

    modified
    |> Seq.iter
        (fun x ->
            printfn "- %A" parsedBaseFile.[x]
            printfn "+ %A" parsedDeltaFile.[x])

    0 // return an integer exit code
