namespace Csvdiff

module ReadFile =

    open System.IO
    open FSharp.Data

    /// return the lines from the csv file
    let fetchLines filePath separator =

        if File.Exists filePath then
            let csv = CsvFile.Load( __SOURCE_DIRECTORY__ + filePath, (separator |> string ))
            csv.Rows
        else
            failwith filePath
