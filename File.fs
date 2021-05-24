namespace Csvdiff

module ReadFile =

    open System.IO

    /// return the lines from the csv file
    let fetchLines filePath = // filename -> string array

        if File.Exists filePath then
            File.ReadAllLines filePath
        else
            failwith filePath
