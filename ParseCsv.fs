namespace Csvdiff

module ParseCsv =

    let splitLine (line: string) (separator: char) (pKeyFields: list<int>) =

        // let pKey = line.Split(separator).[0]
        let pKey = 
            let fields = line.Split(separator)

            pKeyFields
            |> List.map (fun x -> fields.[x])
            // |> List.toArray
            |> String.concat ""

        pKey.GetHashCode(), line

    let parseLines (fileLines: string []) (separator: char) (pKeyFields: list<int>) =

        fileLines
        |> Array.map (fun line -> splitLine line separator pKeyFields)
        |> Map.ofArray
