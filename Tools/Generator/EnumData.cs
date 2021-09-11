using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class EnumData
    {
        public EnumData(string name, IEnumerable<string> items)
        {
            this.Name = name;
            this.ClassName = Converter.JenkinsToClassName(name);
            this.Items = items.Select(v => new EnumItem(v)).ToList();
        }

        public string Name { get; }
        public string ClassName { get; }

        public List<EnumItem> Items { get; }
    }
}
