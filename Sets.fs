namespace Csvdiff

module Sets =

    /// Return a sequence of the map keys
    let getSet (myMap: Map<'Key, 'T>) =

        myMap |> Map.toArray |> Array.map fst

    /// Return results exclusive to the first set
    let getSetExclusive set1 set2 =

        // set set1 - set set2
        Array.except set2 set1

    /// Combine both seqs, combine both exclusive seqs, and exclude those
    let getSetBoth set1 set2 set1ex set2ex =

        let combinedLines = Array.concat [ set1; set2 ]
        let combinedExclusives = Array.concat [ set1ex; set2ex ]

        Array.except combinedExclusives combinedLines
