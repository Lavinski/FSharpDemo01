// Learn more about F# at http://fsharp.org

open System

module rec Test =
    
//let fu (a: ^a) (b: 'b) = 1
    
open Test

type ResultExpr() =
    member d.Bind(comp, func) = Option.bind func comp
    member this.Return(value) = Some value
  
[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    
    
    //let x = fu 1
    
    
    
    let m x =
        match x with
        | CaseA -> 1
        | CaseB -> 1
    
    let f x y = 1
    
    let attempt = ResultExpr ()
    
    // Result
    let result = attempt {
        let! x = Some 1
        //let! z = None
        let! y = Some 2
        
        //printf "%A %A %A" x z y
        return x + y
    }
    
    
    
    printfn "%A" result
    
    match result with
    | None -> printfn "None"
    | Some e -> printfn "%A" e
    
    //printfn "%s" <| f.GetType().BaseType.BaseType.ToString()
    
    //List.fold (fun x y -> match y with | None -> None | _ -> None) None [ Some { prob1 = 1; prob2 = "x"  } ] |> ignore
    
    //IntroToFSharp.Runner.run ()
    
    Console.ReadLine() |> ignore
    0 // return an integer exit code
