namespace Enticify.CsSpy.Tests

module DictionaryVisualizerTests =
    open Microsoft.VisualStudio.DebuggerVisualizers
    open Xunit
    open Microsoft.CommerceServer.Runtime
    open Enticify.CsSpy
    open Enticify.CsSpy.DictionaryVisualizer

    //Test type that will throw exception when we call ToString().
    type CrashToString () =
        override x.ToString() = failwith "Yikes, I crashed!"

    //Operator shortcut for ":> obj"
    let inline (!*) (item:#obj) = item :> obj

    //Shows the Visualizer in the VizDevHost.
    let visualize dic =
        let vizHost = VisualizerDevelopmentHost (dic, typeof<DictionaryVisualizerDialog>, typeof<DictionaryObjectSource>)
        vizHost.ShowVisualizer()

    let randomDic () = 
        Dic.ofList [
            "datetimenow", !* System.DateTime.Now;
            "ticks", !* System.DateTime.Now.Ticks;
            "guid", !* System.Guid.NewGuid(); ]

    //NOTE:  These tests are basic integration tests.  Not unit tests at the mo.

    [<Fact>]
    let ``Creates nodes for IEnumerable values`` () =
        let dic = Dic.ofList ["items", ["1";"2";"3"]]
        let tree = mapDictionaryToTree dic
        visualize dic
        ()

    [<Fact>]
    let ``Does not crash with a bunch of the basic properties :)`` () =
        let dic = 
           Dic.ofList [ 
                "name", !* "ben";
                "slints", !* ([1;5;9;15] |> Sl.ofSeq);
                "iamnull", null;
                "iamdbnull", !* System.DBNull.Value;
                "subdic", !* (Dic.ofList 
                    [
                        "someInt", !* 1;
                        "someString", !* "This is some string of stuff";
                        "someBook", !* false;
                        "slofdics", !* ([for i in 1..5 do yield randomDic()] |> Sl.ofSeq);
                        "ihazcrashed", !* CrashToString();
                    ])
            ]
        visualize dic
        ()

