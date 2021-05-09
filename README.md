# csvdiff - diff two csv files

DONE 1. Read in two csv files.
2. Exclude/Include fields.
DONE 3. Create map
DONE    a. Key = primary key (1st column default. Can specify one or more.)
DONE     b. Value = { PKey ; Line }
DONE 4. Filter both maps. 
DONE     a. Compare hashes of records 
DONE 5. Print unique and instances of mismatched hashes.
DONE 6. Use the addition, deletion, modification model in the go-based csvdiff 
7. Output formatting/coloring.
8. Compound primary keys.
9. Argument handling.

Reference: https://aswinkarthik.github.io/csvdiff/

```FSharp
let myMap = 
    ["uid25",["hash";"uid25,staff,coolguy";"0";"OGFILE"]; "uid26",["hash";"uid26,student,dork";"1";"OGFILE"]] |> Map.ofList
```

myMap = [ HASHofKey, RECORD; HASHofKey2, RECORD2; etc ]
Note: Use a record, instead of a list. Something like:
BECAUSE records get hashed. 

type myLine = 
    {   Key : string
        LineText : string }
