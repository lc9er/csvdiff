namespace Csvdiff

module ParseCsv =

    let splitLine (line: string) (separator: string) =

        let pKey = line.Split(separator).[0]
        pKey.GetHashCode(), line

    let parseLines (fileLines: string []) (separator: string) =

        fileLines
        |> Array.map (fun x -> splitLine x separator)
        |> Map.ofArray
