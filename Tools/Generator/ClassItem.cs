namespace Generator
{
    public class ClassItem
    {
        public ClassItem(string className, string methodName, string xmlType, bool isList, string description)
        {
            this.Name = methodName;
            this.ItemType = isList ? ItemTypes.List : (xmlType == "xsd:boolean" && !methodName.StartsWith("use") ? ItemTypes.Bool : ItemTypes.Single);
            this.Description = description;

            this.ItemName = this.ItemType switch
            {
                ItemTypes.Single => methodName.HungarianNotation(),
                ItemTypes.Bool => $"Is{methodName.HungarianNotation()}",
                ItemTypes.List => $"{methodName.HungarianNotation()}s",
                _ => throw new System.Exception($"Unknown ItemType {this.ItemType}")
            };

            this.DataType = xmlType switch
            {
                "xsd:boolean" => "bool",
                "xsd:string" => "string",
                "xsd:int" => "int",
                "xsd:long" => "long",
                "xsd:anyType" => methodName switch
                {
                    "executable" => "JenkinsExecutable",
                    "job" => "JenkinsModelJob",
                    "task" => "object",
                    "action" => "object",
                    //"result" => "JenkinsResult",
                    
                    "result" => className switch
                    {
                        "hudson.model.Run" => "JenkinsResult",
                        _ => "object"
                    },
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
