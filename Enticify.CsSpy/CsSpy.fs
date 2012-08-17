namespace Enticify.CsSpy

module DictionaryVisualizer =
    open Microsoft.VisualStudio.DebuggerVisualizers
    open System.Windows.Forms
    open System.Diagnostics
    open Microsoft.CommerceServer.Runtime
    open System.Runtime.Serialization.Formatters.Binary

    type DictionaryObjectSource() = 
        inherit VisualizerObjectSource() 
        override this.GetData(target : obj, outgoingData : System.IO.Stream) = 
            let formatter = BinaryFormatter() 
            match target with
            | :? DictionaryClass as dic -> 
                let data = 
                    [   for key in dic do
                            match key with
                            | :? string as s -> 
                                let value = 
                                    match dic.[s] with
                                    | null -> "null"
                                    | value -> value.ToString() 
                                yield (s, value) 
                            | _ -> ()//Should never happen! 
                    ]
                formatter.Serialize(outgoingData, data) 
            | _ -> formatter.Serialize(outgoingData, "No data.")
            ()

    type DictionaryVisualizerDialog() =
        inherit DialogDebuggerVisualizer()
        override x.Show(dvs:IDialogVisualizerService, vop:IVisualizerObjectProvider) = 
            let show msg = MessageBox.Show(msg) |> ignore 
            match vop.GetObject() with 
            | :? string as s -> show s 
            | :? ((string*string) list) as sl -> show (sl |> Seq.fold (fun acc (key,value) -> acc + " " + key + ":" + value) "")
            | _ -> show "Unexpected object provided"
            ()

    [<assembly:DebuggerVisualizer(typeof<DictionaryVisualizerDialog>, 
                                  typeof<DictionaryObjectSource>, 
                                  Target = typedefof<DictionaryClass>, 
                                  Description = "Enticify CsSpy Dicitonary Visualizer")>]
    do()