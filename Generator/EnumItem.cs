namespace Generator
{
    public class EnumItem
    {
        public EnumItem(string name)
        {
            this.Name = name;
            this.ItemName = Converter.JenkinsToItemName(name);
        }

        public string Name { get; }

        public string ItemName { get; }
    }
}
