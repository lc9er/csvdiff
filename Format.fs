namespace Csvdiff

module Format =
    open System

    type PrintColor =
        | Header
        | Addition
        | Deletion

        member this.ApplyColor =
            match this with
            | Addition -> ConsoleColor.Green
            | Deletion -> ConsoleColor.Red
            | Header -> ConsoleColor.Blue
        
    /// Print line, with appropriate color scheme
    let printFormattedResults line (color : PrintColor) =
        
        let consoleColor = color 

        Console.ForegroundColor <- consoleColor.ApplyColor

        printfn "%s" line
