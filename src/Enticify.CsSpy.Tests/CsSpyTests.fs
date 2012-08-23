namespace Enticify.CsSpy.Tests

module DictionaryVisualizerTests =
    open Microsoft.VisualStudio.DebuggerVisualizers
    open Xunit
    open Microsoft.CommerceServer.Runtime
    open Enticify.CsSpy
    open Enticify.CsSpy.DictionaryVisualizer
    open System.Threading

    //Test type that will throw exception when we call ToString().
    type ToStringException() =
        override x.ToString() = failwith "Yikes, I crashed!"

    //Test type that will throw exception when we call ToString().
    type ToStringTimeOut() =
        override x.ToString() = 
            Thread.Sleep(20000)
            ""
    //Operator shortcut for ":> obj"
    let inline (!*) (item:#obj) = item :> obj

    //Shows the Visualizer in the VizDevHost.
    let visualize item =
        let vizHost = VisualizerDevelopmentHost (item, typeof<DictionaryVisualizerDialog>, typeof<TreeViewObjectSource>)
        vizHost.ShowVisualizer()

    let randomDic () = 
        Dic.ofList [
            "datetimenow", !* System.DateTime.Now;
            "ticks", !* System.DateTime.Now.Ticks;
            "guid", !* System.Guid.NewGuid(); ]

    //NOTE:  These tests are basic integration tests.  Not unit tests at the mo.

    [<Fact>]
    let ``Maps SimpleListClass`` () =
        let sl = Sl.ofSeq ["ben";"mike";]
        let tree = mapObjectToTree sl 
        visualize sl 
        ()

    [<Fact>]
    let ``Simulates function timeout`` () =
        let dic = Dic.ofList ["timecrash", ToStringTimeOut()]
        visualize dic 
        ()

    [<Fact>]
    let ``Creates nodes for IEnumerable values`` () =
        let dic = Dic.ofList ["items", ["1";"2";"3"]]
        let tree = mapObjectToTree dic
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
                        "ihazcrashed", !* ToStringException();
                    ])
            ]
        visualize dic
        ()

