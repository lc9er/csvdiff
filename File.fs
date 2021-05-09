namespace Csvdiff

module ReadFile =

    open System.IO

    let fetchLines filePath = // filename -> string array

        let rows = File.ReadAllLines filePath
        rows
