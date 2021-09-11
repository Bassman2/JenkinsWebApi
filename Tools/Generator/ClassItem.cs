namespace Generator
{
    public class ClassItem
    {
        public ClassItem(string name, string xmlType, bool isList, string description)
        {
            this.Name = name;
            this.ItemType = isList ? ItemTypes.List : (xmlType == "xsd:boolean" && !name.StartsWith("use") ? ItemTypes.Bool : ItemTypes.Single);
            this.Description = description;

            this.ItemName = this.ItemType switch
            {
                ItemTypes.Single => Converter.JenkinsToItemName(name),
                ItemTypes.Bool => $"Is{Converter.JenkinsToItemName(name)}",
                ItemTypes.List => $"{Converter.JenkinsToItemName(name)}s",
                _ => throw new System.Exception($"Unknown ItemType {this.ItemType}")
            };

            this.DataType = xmlType switch
            {
                "xsd:boolean" => "bool",
                "xsd:string" => "string",
                "xsd:int" => "int",
                "xsd:long" => "long",
                "xsd:anyType" => name switch
                {
                    "executable" => "JenkinsExecutor",
                    "job" => "JenkinsModelJob",
                    "task" => "object",
                    "action" => "object",
                    "result" => "JenkinsTasksTestTestResult",
                    "currentExecutable" => "object",
                    "property" => "object",
                    "timestamp" => "object",
                    "item" => "object",
                    "history" => "object",
                    "latest" => "object",
                    "cloud" => "object",
                    "stores" => "object",
                    "duration" => "object",
                    "dependencies" => "object",
                    "optionalDependencies" => "object",
                    "monitorData" => "object",
                    _ => "object"
                },
                _ => Converter.JenkinsToClassName(xmlType)
            };
        }

        public string Name { get; }
        
        public string ItemName { get; }

        public string DataType { get; }
        
        public ItemTypes ItemType { get; }

        public string Description { get; }

        public enum ItemTypes
        {
            Single,
            Bool,
            List
        }
    }
}
