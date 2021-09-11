namespace Generator
{
    public class ClassItem
    {
        public ClassItem(string name, string type, bool isList, string description)
        {
            this.Name = name;
            this.Type = type;
            this.IsList = IsList;
            this.Description = description;

            if (isList)
            {
                this.ItemName = $"{Converter.JenkinsToItemName(name)}s";
            }
            else if (type == "xsd:boolean" && !name.StartsWith("use"))
            {
                this.ItemName = $"Is{Converter.JenkinsToItemName(name)}";
            }
            else
            {
                this.ItemName = Converter.JenkinsToItemName(name);
            }
        }

        public string Name { get; }
        
        public string ItemName { get; }

        public string Type { get; }

        public bool IsList { get; }

        public string Description { get; }
    }
}
