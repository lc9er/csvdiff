open System

let headerColor = ConsoleColor.Blue
let additionColor = ConsoleColor.Green
let deletionColor = ConsoleColor.Red

Console.ForegroundColor <- headerColor
printfn "Haaaaaay GURRRRL"
Console.ForegroundColor <- deletionColor
printfn "Oh. Hello."
Console.ForegroundColor <- additionColor
printfn "Noice. BYeeeeeeeeee"
Console.ResetColor()

let myList = [1;2;3;4]
let printLine = "List size (" + myList.Length.ToString() + ")"
printfn "%s" printLine
