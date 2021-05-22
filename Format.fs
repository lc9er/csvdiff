namespace Csvdiff

module Format =
    open System

    let printFormattedResults line color =

        let headerColor = ConsoleColor.Blue
        let additionColor = ConsoleColor.Green
        let deletionColor = ConsoleColor.Red

        let consoleColor =
            match color with
            | "blue" -> headerColor
            | "red" -> deletionColor
            | _ -> additionColor

        Console.ForegroundColor <- consoleColor

        printfn "%s" line

// Console.ForegroundColor <- defaultColor
// Console.ResetColor()
