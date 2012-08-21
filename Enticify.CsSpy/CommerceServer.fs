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
