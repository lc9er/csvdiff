namespace Csvdiff

module ParseCsv =

    let splitLine (line: string) (separator: char) =

        let pKey = line.Split(separator).[0]
        pKey.GetHashCode(), line

    let parseLines (fileLines: string []) (separator: char) =

        fileLines
        |> Array.map (fun line -> splitLine line separator)
        |> Map.ofArray
