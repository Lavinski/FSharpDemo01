module IntroToFSharp.Types

type Record =
    {
        prop: int
        Thing: unit
    }
    
    
type ClassLike(thing: int) =
    member val Thing = thing
        
        
        