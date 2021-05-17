namespace Csvdiff

module ParseCsv =

    let splitLine (line: string) (separator: char) (pKeyFields: list<int>) (excludeFields: list<int>) =

        let fields = line.Split(separator)

        let lineExcludes =
            excludeFields
            |> List.map (fun x -> fields.[x])
            |> Array.ofList

        let modLine =
            fields
            |> Array.except lineExcludes
            |> String.concat (separator |> string)

        let pKey =
            pKeyFields
            |> List.map (fun x -> fields.[x])
            |> String.concat ""

        pKey.GetHashCode(), modLine

    let parseLines (fileLines: string []) (separator: char) (pKeyFields: list<int>) (excludeFields: list<int>) =

        fileLines
        |> Array.map (fun line -> splitLine line separator pKeyFields excludeFields)
        |> Map.ofArray
