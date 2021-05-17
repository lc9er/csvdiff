namespace Csvdiff

module CliArgs =

    type CsvArgs =
        { OldFile: string
          NewFile: string
          Separator: char
          PrimaryKey: list<int> }

    let findArg argv param =
        argv |> List.tryFindIndex (fun x -> x = param)

    let splitArgString (param: string) =
        param.Split ','
        |> Array.map (fun x -> x |> int)
        |> Array.toList

    let getSeparator (argv: list<string>) =
        let myArg = findArg argv "-s"

        match myArg with
        | Some i -> argv.[(Some(i + 1)).Value] |> char
        | None -> ','

    let getPrimaryKey (argv: list<string>) =
        let myArg = findArg argv "-p"

        match myArg with
        | Some i -> splitArgString argv.[(Some(i + 1)).Value]
        | None -> [0]

    let getArgs (argv: list<string>) =

        { OldFile = argv.[0]
          NewFile = argv.[1]
          Separator = getSeparator argv
          PrimaryKey = getPrimaryKey argv }
