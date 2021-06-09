namespace Csvdiff

module Sets =

    open System.Linq
    open System.Collections.Generic
    /// Return a sequence of the map keys
    let getSet (myMap: IReadOnlyDictionary<'Key, 'T>) =

        myMap.Keys.ToArray()

    /// Return results exclusive to the first set
    let getSetExclusive set1 set2 =

        Array.except set2 set1
