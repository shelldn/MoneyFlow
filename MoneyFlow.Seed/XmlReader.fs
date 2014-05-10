namespace MoneyFlow.Seed

[<AutoOpen>]
module internal XmlReader =

    open System.Collections.Generic
    open System.Reflection
    open System.IO
    open System.Xml
    open System.Xml.Serialization
    open MoneyFlow.Model

    let private asmPath = 
        Assembly.GetExecutingAssembly().CodeBase
        |> Path.GetDirectoryName

    let read<'a> fileName =
        
        let root = Path.GetFileNameWithoutExtension(fileName)
        let filePath = Path.Combine(asmPath, "App_Data", fileName)

        use xr = XmlReader.Create(filePath)
        let xs = XmlSerializer(typeof<List<'a>>, XmlRootAttribute(root))

        xs.Deserialize(xr) :?> IEnumerable<Category>