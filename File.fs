namespace Csvdiff

module ReadFile =

    open System
    open System.IO

    /// return the lines from the csv file
    let fetchLines filePath = // filename -> string array

        if File.Exists filePath then
            try 
                File.ReadAllLines filePath
            with
            | :? FormatException as e ->
                failwith e.Message
            | :? IOException as e ->
                failwith e.Message
            | _ as e ->
                failwith e.Message
        else
            printfn "File not found: %s" filePath
            Environment.Exit 1
            failwith filePath
