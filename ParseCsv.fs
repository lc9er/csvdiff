namespace Csvdiff

module ParseCsv =
    // Record of each line - a hash of the text for comparison
    // and the actual text, for printing.
    type lineRecord = { Key: int; LineText: string }

    let splitLine (line: string) (separator: string) =
        let pKey = line.Split(separator).[0]
        pKey.GetHashCode(), { Key = line.GetHashCode(); LineText = line }

    let parseLines (fileLines: string []) (separator: string) =

        fileLines
        |> Array.map (fun x -> splitLine x separator)
        |> Map.ofArray
