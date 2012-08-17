namespace Enticify.CsSpy.Tests

module CsSpyTests =
    open Microsoft.VisualStudio.DebuggerVisualizers
    open Xunit
    open Microsoft.CommerceServer.Runtime
    open Enticify.CsSpy.DictionaryVisualizer

    [<Fact>]
    let ``runs ok`` () =
        let dic = DictionaryClass()
        dic.["_name"] <- "ben"
        dic.["_iamnull"] <- null  
        dic.["_iamdbnull"] <- System.DBNull.Value 
        let vizHost = VisualizerDevelopmentHost (dic, typeof<DictionaryVisualizerDialog>, typeof<DictionaryObjectSource>)
        vizHost.ShowVisualizer()
        ()

