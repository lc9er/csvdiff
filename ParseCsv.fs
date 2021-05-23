namespace Csvdiff

module ParseCsv =

    /// create a list, using the include/exclude index fields
    let buildList (fields: array<string>) = Array.map (fun x -> fields.[x])

    /// splitLine, given separator. Build pkey and include/exclude fields
    /// This should probably be split into smaller chunks
    let splitLine (line: FSharp.Data.CsvRow) (separator: string) (pKeyFields: array<int>) (modFields: string * array<int>) =

        let fields = line.Columns

        /// capture the fields to include/exclude
        let lineCapture =
            match modFields with
            | (_, extractFields) ->
                extractFields
                |> buildList fields

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
            |> buildList fields
            |> String.concat ""

        // Return Map entry
        pKey, modLine

    let parseLines (fileLines: seq<FSharp.Data.CsvRow>) (separator: string) (pKeyFields: array<int>) (modFields: string * array<int>) =

        fileLines
        |> Seq.map (fun line -> splitLine line separator pKeyFields modFields)
        |> Map.ofSeq
