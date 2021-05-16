namespace Csvdiff

module CliArgs =

    type CsvArgs =
        { OldFile: string
          NewFile: string
          Separator: char }

    let getSeparator (argv: list<string>) =
        let myArg =
            argv |> List.tryFindIndex (fun x -> x = "-s")

        match myArg with
        | Some i -> argv.[(Some(i + 1)).Value] |> char
        | None -> ','

    let getArgs (argv: list<string>) =

        { OldFile = argv.[0]
          NewFile = argv.[1]
          Separator = getSeparator argv }
// PrimaryKey = argv.[2] |> int }
