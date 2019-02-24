namespace IntroToFSharp

module Messages =
    open System
    
    type Command<'t> = 
        {
            AggregateId: Guid
            Payload: 't
        }
    
    type StartCommand =
        {
            name: string
        }

    type StopCommand =
        {
            name: string
        }
    
    type TaskCommands =
        | StartCommand of StartCommand
        | StopCommand of StopCommand


    type StartedEvent =
        {
            name: string
        }

    type StoppedEvent =
        {
            name: string
        }
    
    type TaskEvents =
        | StartedEvent of StoppedEvent
        | StoppedEvent of StoppedEvent

module Domain =
    open Messages
    
    let mutable events: TaskEvents list = []
    
    let load id =
        events
    
    let save id event =
        events <- event :: events

    type TaskStatus =
        | Started
        | Stopped

    type TaskAggregate =
        {
            status: TaskStatus
        }

    let exec state = function
        | StartCommand c ->
            printfn "Started"
            StartedEvent { name = c.name }
        | StopCommand c ->
            printfn "Stopped"
            StoppedEvent { name = c.name }
    
    let apply state = function
        | StartedEvent c -> { status = Started }
        | StoppedEvent c -> { status = Stopped }
    
    let commandHandler command =
        let events = load command.AggregateId
        // Start pure things
        let state = events |> List.fold apply { status = Stopped }
        let event = exec state command.Payload
        apply state event |> ignore // POI
        // Stop pure things
        save command.AggregateId event 

module AppCompRoot =
    open System
    open Messages
    open Domain

    let clock () = DateTime.Now
    
    
    let x = obj()
    let y = x :? String
    
    //let s = typedefof<list<>>
    
    
    let send (message: obj) =
        match message with 
        | :? Command<TaskCommands> as command ->
            Ok <| commandHandler command
        | c ->
            Error <| sprintf "Woah did not expect that %A" c

module Runner =
    open System
    open Messages
    open AppCompRoot
    
    let run () =
        let id = Guid.NewGuid()
        let list =
            [
                {
                    AggregateId = id
                    Payload = StartCommand { name = "Something" }
                }
                {
                    AggregateId = id
                    Payload = StopCommand { name = "Something" }
                }
            ]
      
        List.iter (send >> ignore) list
        
        
        
        

