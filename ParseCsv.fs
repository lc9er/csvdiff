namespace Csvdiff

module ParseCsv =

    /// create a list, using the include/exclude index fields
    let buildList (fields: array<string>) = List.map (fun x -> fields.[x])

    /// splitLine, given separator. Build pkey and include/exclude fields
    /// This should probably be split into smaller chunks
    let splitLine (line: string) (separator: char) (pKeyFields: list<int>) (modFields: string * list<int>) =

        let fields = line.Split(separator)

        /// capture the fields to include/exclude
        let lineCapture =
            match modFields with
            | (_, extractFields) ->
                extractFields
                // |> List.map (fun x -> fields.[x])
                |> buildList fields
                |> Array.ofList

        /// If include, use lineCapture
        /// If exclude, use everything except lineCapture
        let modLine =
            match modFields with
            | ("-i", _) -> lineCapture |> String.concat (separator |> string)
            | _ ->
                fields
                |> Array.except lineCapture
                |> String.concat (separator |> string)


        /// build pKey off of unedited fields list
        /// this allows excluded fields to be used, 
        /// but not printed
        let pKey =
            pKeyFields
            // |> List.map (fun x -> fields.[x])
            |> buildList fields
            |> String.concat ""

        // Return Map entry
        pKey, modLine

    let parseLines (fileLines: string []) (separator: char) (pKeyFields: list<int>) (modFields: string * list<int>) =

        fileLines
        |> Array.map (fun line -> splitLine line separator pKeyFields modFields)
        |> Map.ofArray
