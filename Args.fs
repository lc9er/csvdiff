namespace Csvdiff

module CliArgs =

    /// Args record
    type CsvArgs =
        { OldFile: string
          NewFile: string
          Separator: string
          PrimaryKey: array<int>
          ModFields: string * array<int> }

    /// Parse argv for a flag
    let findArg argv flag =
        argv |> Array.tryFindIndex (fun x -> x = flag)

    let splitArgString (flag: string) =
        flag.Split ','
        |> Array.map (fun x -> x |> int)

    /// default = ','. Return as character tp allow for `t etc
    let getSeparator (argv: array<string>) =
        let myArg = findArg argv "-s"

        match myArg with
        | Some i -> argv.[(Some(i + 1)).Value]
        | None -> ","

    let getFields (argv: array<string>) flag =
        let myArg = findArg argv flag

        match myArg with
        | Some i -> splitArgString argv.[(Some(i + 1)).Value]
        | None ->
            match flag with
            | "-p" -> [| 0 |]
            | _ -> [||]

    let getArgs (argv: array<string>) =

        /// Check for include/exclude here
        /// If both present, ignore exclusions
        /// if includeFields returns []
        /// then proceed with excludeFields
        let fields =
            let includeFields = getFields argv "-i"

            match includeFields with
            | [||] -> ("-e", getFields argv "-e")
            | _ -> ("-i", includeFields)

        { OldFile = argv.[0]
          NewFile = argv.[1]
          Separator = getSeparator argv
          PrimaryKey = getFields argv "-p"
          ModFields = fields }
