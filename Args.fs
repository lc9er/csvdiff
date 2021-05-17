namespace Csvdiff

module CliArgs =

    type CsvArgs =
        { OldFile: string
          NewFile: string
          Separator: char
          PrimaryKey: list<int>
          ExcludeFields: list<int> }

    let findArg argv flag =
        argv |> List.tryFindIndex (fun x -> x = flag)

    let splitArgString (flag: string) =
        flag.Split ','
        |> Array.map (fun x -> x |> int)
        |> Array.toList

    let getSeparator (argv: list<string>) =
        let myArg = findArg argv "-s"

        match myArg with
        | Some i -> argv.[(Some(i + 1)).Value] |> char
        | None -> ','

    let getFields (argv: list<string>) flag =
        let myArg = findArg argv flag

        match myArg with
        | Some i -> splitArgString argv.[(Some(i + 1)).Value]
        | None ->
            match flag with
            | "-p" -> [ 0 ]
            | _ -> []

    let getArgs (argv: list<string>) =

        { OldFile = argv.[0]
          NewFile = argv.[1]
          Separator = getSeparator argv
          PrimaryKey = getFields argv "-p"
          ExcludeFields = getFields argv "-e" }
