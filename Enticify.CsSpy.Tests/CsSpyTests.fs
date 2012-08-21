namespace Enticify.CsSpy.Tests

module DictionaryVisualizerTests =
    open Microsoft.VisualStudio.DebuggerVisualizers
    open Xunit
    open Microsoft.CommerceServer.Runtime
    open Enticify.CsSpy
    open Enticify.CsSpy.DictionaryVisualizer

    type CrashToString () =
        override x.ToString() = failwith "Yikes, I crashed!"

    let randomDic () = 
        let dic = DictionaryClass()
        dic.["datetimenow"] <- System.DateTime.Now
        dic.["ticks"] <- System.DateTime.Now.Ticks
        dic.["guid"] <- System.Guid.NewGuid()
        dic

    [<Fact>]
    let ``Does not crash with a bunch of the basic properties :)`` () =

        let dic = DictionaryClass()
        dic.["name"] <- "ben"
        dic.["slints"] <- [1;5;9;15] |> Sl.ofSeq
        dic.["iamnull"] <- null  
        dic.["iamdbnull"] <- System.DBNull.Value 
        let subDic = 
            let dic = DictionaryClass()
            dic.["someInt"] <- 1
            dic.["someString"] <- "This is some string of stuff"
            dic.["someBook"] <- false 
            dic.["slofdics"] <- [randomDic();randomDic();randomDic();randomDic();randomDic();] |> Sl.ofSeq
            dic.["ihazcrashed"] <- CrashToString()
            dic
        dic.["subdic"] <- subDic

        for key in dic do
            printfn "%s : %s" (key.ToString()) (dic.[key.ToString()].ToString())

        let vizHost = VisualizerDevelopmentHost (dic, typeof<DictionaryVisualizerDialog>, typeof<DictionaryObjectSource>)
        vizHost.ShowVisualizer()
        ()

