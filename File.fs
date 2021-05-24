namespace Csvdiff

module ReadFile =

    open System
    open System.IO
    open ParseCsv

    /// return the lines from the csv file
    let fetchLines filePath separator primary fields = // filename -> string array

        if File.Exists filePath then
            try
                /// read file, line by line
                /// parse each line and return Map
                let readlines =
                    seq {
                        use sr = new StreamReader(filePath)

                        while not sr.EndOfStream do
                            let spl =
                                splitLine (sr.ReadLine()) separator primary fields

                            yield spl
                    }

                readlines |> Map.ofSeq

            with
            | :? FormatException as e -> failwith e.Message
            | :? IOException as e -> failwith e.Message
            | e -> failwith e.Message
        else
            printfn "File not found: %s" filePath
            Environment.Exit 1
            failwith filePath
