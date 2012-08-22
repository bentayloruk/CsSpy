namespace Enticify.CsSpy

///Operations related to Commerce Server ISimpleList types.
[<RequireQualifiedAccess>]
[<AutoOpen>]
module Sl =
    open Microsoft.CommerceServer.Runtime
    ///Converts a seq into a SimpleList.
    let ofSeq (source:seq<'T>) =
        let sl = SimpleListClass()
        for x in source do
            let o = x :> obj
            sl.Add(ref o)
        sl

[<RequireQualifiedAccess>]
[<AutoOpen>]
module Dic =
    open Microsoft.CommerceServer.Runtime

    //Creates a DictionaryClass for a list of key value tuples.
    let ofList (kvps:list<_*_>) =
        let dic = new DictionaryClass()
        for (key, value) in kvps do
            dic.[key] <- value
        dic

    let ofSeq(kvps:seq<_*_>) =
        let dic = new DictionaryClass()
        for (key, value) in kvps do
            dic.[key] <- value
        dic
    
