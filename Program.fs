open System
open Csvdiff
open HelpVersion
open CliArgs
open ReadFile
open Sets
open Format

[<EntryPoint>]
let main argv =

    let argvList = argv
    let help = findArg argvList "-h"
    let version = findArg argvList "-v"

    // if help or version exist, print and exit
    match (help, version) with
    | (Some (_), _) ->
        printfn "%s" getCsvdiffHelp
        1
    | (_, Some (_)) ->
        printfn "%s" getCsvdiffVersion
        2
    | _ ->
        // Setup
        let myArgs = getArgs argvList
        let separator = myArgs.Separator
        let primary = myArgs.PrimaryKey
        let fields = myArgs.ModFields

        let parsedBaseFile =
            fetchLines myArgs.OldFile separator primary fields

        let parsedDeltaFile =
            fetchLines myArgs.NewFile separator primary fields

        // Build line sets
        let baseKeys = getSet parsedBaseFile
        let deltaKeys = getSet parsedDeltaFile

        // Get exclusive and inclusive sets
        let additions = getSetExclusive deltaKeys baseKeys
        let removals = getSetExclusive baseKeys deltaKeys

        let combinedLines = getCombinedSet baseKeys deltaKeys
        let combinedExclusives = getCombinedSet additions removals

        let inBoth =
            getSetBoth combinedExclusives combinedLines

        // Keep lines where keys match but values don't
        let modified =
            inBoth
            |> Array.filter (fun x -> parsedBaseFile.[x] <> parsedDeltaFile.[x])

        // Print it
        //
        // Additions
        let adds =
            "Additions (" + additions.Length.ToString() + "):"

        printFormattedResults adds Header

        additions
        |> Array.iter
            (fun x ->
                let line = "+ " + parsedDeltaFile.[x]
                printFormattedResults line Addition)

        // Deletions
        let dels =
            "Removals (" + removals.Length.ToString() + "):"

        printFormattedResults dels Header

        removals
        |> Array.iter
            (fun x ->
                let line = "- " + parsedBaseFile.[x]
                printFormattedResults line Deletion)

        // Modifications
        let mods =
            "Modified (" + modified.Length.ToString() + "):"

        printFormattedResults mods Header

        modified
        |> Array.iter
            (fun x ->
                let origLine = "- " + parsedBaseFile.[x]
                let modLine = "+ " + parsedDeltaFile.[x]
                printFormattedResults origLine Deletion
                printFormattedResults modLine Addition)

        // Reset the console, or everything will print in the last color
        Console.ResetColor()
        0 // return an integer exit code
