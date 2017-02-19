#r @".\packages\Tesseract\lib\net45\Tesseract.dll"

open System
open System.Globalization
open Tesseract

let parseLine (line : string) =
    let values = line.Split(' ') |> Array.map (fun value -> Convert.ToDecimal(value, CultureInfo("no-nb")))
    let left = values |> Array.take 3 |> Array.max
    let right = values |> Array.skip 3 |> Array.max
    (left, right)

let engine = new TesseractEngine(@"./tessdata", "nor", EngineMode.Default)
let image = Pix.LoadFromFile("oppgave1.png")
let page = engine.Process(image)
let text = page.GetText();

text.Split([| "\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.filter (fun line -> line <> " ")
    |> Array.skip 2 
    |> Array.map parseLine
    |> printfn "%A"