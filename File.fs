namespace Csvdiff

module ReadFile =

    open FSharp.Data

    /// return the lines from the csv file
    let fetchLines filePath separator = // filename -> string array

        let lines = CsvFile.Load(__SOURCE_DIRECTORY__ + filePath, separator).Cache()
        lines.Rows
