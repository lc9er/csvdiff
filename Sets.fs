namespace Csvdiff

module Sets =

    /// Return a sequence of the map keys
    let getSet (myMap: Map<'Key, 'T>) =

        myMap |> Map.toArray |> Array.map fst

    /// Return results exclusive to the first set
    let getSetExclusive set1 set2 =

        // set set1 - set set2
        Array.except set2 set1

    /// combine two sets
    let getCombinedSet set1 set2 =

        Array.concat [ set1; set2 ]

    /// Combine both seqs, combine both exclusive seqs, and exclude those
    let getSetBoth combinedExclusives combinedLines =

        Array.except combinedExclusives combinedLines
