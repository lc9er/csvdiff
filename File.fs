namespace Csvdiff

module ReadFile = 

    open System.IO

    let fetchLines filePath =
        let rows = File.ReadAllLines filePath
        rows
