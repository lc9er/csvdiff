namespace Csvdiff

module Sets = 

    // Return a sequence of the map keys
    let getSet (myMap: Map<'Key, 'T>) =
        myMap
        |> Map.toSeq
        |> Seq.map fst


    // Return a set of results exclusive to the first set
    let getSetExclusive set1 set2 =
        set set1 - set set2

    // Return a set of results in both sets
    let getSetBoth set1 set2 =
        Set.intersect (set set1) (set set2)
