namespace Enticify.CsSpy

module DictionaryVisualizer =
    open Microsoft.VisualStudio.DebuggerVisualizers
    open System.Windows.Forms
    open System.Diagnostics
    open Microsoft.CommerceServer.Runtime
    open System.Runtime.Serialization.Formatters.Binary

    ///The tree type...
    type 'T Tree =
        | Node of 'T * list<'T Tree>
        | Leaf of 'T

    ///Maps a CS dictionary to our Tree type.
    let mapDictionaryToTree (dic:IDictionary) =
        let rec inner (src:obj) desc = 
            match src with
            | :? IDictionary as dic ->
                let children =  
                    [   for key in dic do
                        yield inner dic.[key.ToString()] (key.ToString()) ]
                Node(desc + "<" + "IDictionary" + ">", children)
            | :? ISimpleList as sl -> 
                let children =  
                    [   for i in 0..sl.Count-1 do
                        yield inner sl.[i] (i.ToString())  ]
                Node(desc + "<" + "ISimpleList" + ">", children)
            | value -> 
                let sValue =
                    try
                        value.ToString()
                    with
                    | ex -> sprintf "Value fail: %s" ex.Message
                Leaf(desc + "<" + value.GetType().Name + ">=" + sValue) 
        inner dic "Root"

    ///The type that serializes the source data for debuggee to debugger transport.
    type DictionaryObjectSource() = 
        inherit VisualizerObjectSource() 
        override this.GetData(target : obj, outgoingData : System.IO.Stream) = 
            let formatter = BinaryFormatter() 
            match target with
            | :? DictionaryClass as dic -> 
                let data = mapDictionaryToTree dic
                formatter.Serialize(outgoingData, data) 
            | _ -> formatter.Serialize(outgoingData, "No data.")
            ()

    ///The type that visualizes our data.
    type DictionaryVisualizerDialog() =
        inherit DialogDebuggerVisualizer()
        override x.Show(dvs:IDialogVisualizerService, vop:IVisualizerObjectProvider) = 
            //Get out lovely tree data...
            let treeData = 
                let data = vop.GetObject()
                match data with
                | :? Tree<string> as tree -> tree
                | _ -> Leaf("No data.")
            //Create TreeNode hierarchy
            let nodeTree = 
                let rec buildTreeView (node:TreeNode) (tree:Tree<string>) = 
                    match tree with 
                    | Leaf(text) -> 
                        node.Nodes.Add(text) |> ignore
                    | Node(text, nodeList) -> 
                        let childNode = new TreeNode(text)
                        node.Nodes.Add(childNode) |> ignore
                        for node in nodeList do
                            buildTreeView childNode node 
                let rootNode = new TreeNode()
                buildTreeView rootNode treeData
                rootNode.Nodes.[0]//discard the root node as it is noise.
            //Bang it in to windows forms.
            let tv = new TreeView(Dock = DockStyle.Fill)
            tv.Nodes.Add(nodeTree) |> ignore
            let form = new Form()
            form.Controls.Add(tv)
            dvs.ShowDialog(form) |> ignore
            ()

    [<assembly:DebuggerVisualizer(typeof<DictionaryVisualizerDialog>, 
                                  typeof<DictionaryObjectSource>, 
                                  Target = typedefof<DictionaryClass>, 
                                  Description = "Enticify CsSpy Dicitonary Visualizer")>]
    do()