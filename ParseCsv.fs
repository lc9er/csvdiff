namespace Csvdiff

module ParseCsv =

    type lineRecord = 
        { Key : string
          LineText : string  }

    let splitLine (line : string) (separator : string) = 
        let pKey = line.Split(separator).[0]
        pKey.GetHashCode(), { Key = pKey; LineText = line }

    let parseLines (fileLines: string []) (separator: string) =
    
        fileLines
        |> Array.map (fun x -> splitLine x separator)
        |> Map.ofArray
