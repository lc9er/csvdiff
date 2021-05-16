open System
open Argu
open Csvdiff

type CliArgs =
    {
        OldFile: string
        NewFile: string
    }
[<EntryPoint>]
let main argv =

    // Setup
    let myArgs = { OldFile = argv.[0]; NewFile = argv.[1] }
    printfn "OldFile: %A, NewFile: %A" myArgs.OldFile myArgs.NewFile
    let baseFile = ReadFile.fetchLines myArgs.OldFile
    let deltaFile = ReadFile.fetchLines myArgs.NewFile
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
