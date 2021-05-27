# csvdiff - diff two csv files  

## DESC:
    csvdiff - diff tool for csv files. Compare two csv files, using a primary key. Find additions, deletions, and modifications.\n
## USAGE:
    csvdiff <oldfile> <newfile> [flags]\n
## FLAGS:
    -p int      Primary key position(s). Can use multiple columns to create composite key. Comma separated list. Defaults to 0.
    -s char     Separator character. Defaults to ','.
    -e int      Exclude fields. List of columns to exclude (by index).
    -i int      Include fields. List of columns to include (by index). Ignored if exclude used.
    -h          Print this help.
    -v          Print version info.\n
## EXAMPLES:
    // diff files, using cols 0 & 1 as primary key
    csvdiff oldFile.csv newFile.csv -p 0,1

    // Exclude column 4
    csvdiff oldFile.csv newFile.csv -e 4

    // Include cols [0,1,4,5], create composite key [0,1], use tab char as separator
    csvdiff oldFile.tsv newFile.tsv -i 0,1,4,5 -p 0,1 -s `t

    // Colon is separator char, exclude col 2
    csvdiff oldFile.csv newFile.csv -s : -e 2

## Reference: https://aswinkarthik.github.io/csvdiff/  

The reference implementation is much faster for large datasets. A good 5x+ faster. This is a playground to help me
learn F#.
