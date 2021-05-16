namespace Csvdiff

module CliArgs =

    type CsvArgs =
        { OldFile: string
          NewFile: string
          Separator: char }

    let getArgs (argv: string []) =
        { OldFile = argv.[0]
          NewFile = argv.[1] 
          Separator = argv.[2] |> char}
          // PrimaryKey = argv.[2] |> int }
