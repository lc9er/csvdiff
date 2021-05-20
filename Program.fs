open System
open Csvdiff

[<EntryPoint>]
let main argv =

    // Setup
    let myArgs = CliArgs.getArgs (argv |> Array.toList)

    // printfn
    //     "OldFile: %A, NewFile: %A, Separator: %A, PK: %A, ExcludeFields: %A"
    //     myArgs.OldFile
    //     myArgs.NewFile
    //     myArgs.Separator
    //     myArgs.PrimaryKey
    //     myArgs.ExcludeFields

    let baseFile = ReadFile.fetchLines myArgs.OldFile
    let deltaFile = ReadFile.fetchLines myArgs.NewFile
    let separator = myArgs.Separator
    let primary = myArgs.PrimaryKey
    let exclude = myArgs.ModFields

    // Parse data into a map
    let parsedBaseFile =
        ParseCsv.parseLines baseFile separator primary exclude

    let parsedDeltaFile =
        ParseCsv.parseLines deltaFile separator primary exclude

    // Build line sets
    let baseKeys = Sets.getSet parsedBaseFile
    let deltaKeys = Sets.getSet parsedDeltaFile

    // Get exclusive and inclusive sets
    let additions = Sets.getSetExclusive deltaKeys baseKeys
    let removals = Sets.getSetExclusive baseKeys deltaKeys

    let inBoth =
        Sets.getSetBoth baseKeys deltaKeys additions removals

    // Keep lines where keys match but values don't
    let modified =
        inBoth
        |> Array.filter (fun x -> parsedBaseFile.[x] <> parsedDeltaFile.[x])

    //// Print it
    //
    // Additions
    let adds =
        "Additions (" + additions.Length.ToString() + "):"

    Format.printFormattedResults adds "blue"

    additions
    |> Array.iter
        (fun x ->
            let line = "+ " + parsedDeltaFile.[x]
            Format.printFormattedResults line "green")

    // Deletions
    let dels =
        "Removals (" + removals.Length.ToString() + "):"

    Format.printFormattedResults dels "blue"

    removals
    |> Array.iter
        (fun x ->
            let line = "- " + parsedBaseFile.[x]
            Format.printFormattedResults line "red")

    // Modifications
    let mods =
        "Modified (" + modified.Length.ToString() + "):"

    Format.printFormattedResults mods "blue"

    modified
    |> Array.iter
        (fun x ->
            let origLine = "- " + parsedBaseFile.[x]
            let modLine = "+ " + parsedDeltaFile.[x]
            Format.printFormattedResults origLine "red"
            Format.printFormattedResults modLine "green")

    // Reset the console, or everything will print in the last color
    Console.ResetColor()
    0 // return an integer exit code
