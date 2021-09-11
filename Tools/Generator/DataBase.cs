using System.Collections.Generic;

namespace Generator
{
    public class DataBase
    {
        private Dictionary<string, EnumData> enums = new Dictionary<string, EnumData>();
        private Dictionary<string, ClassData> classes = new Dictionary<string, ClassData>();

        public IEnumerable<EnumData> Enums { get { return this.enums.Values; } }
        public IEnumerable<ClassData> Classes { get { return this.classes.Values; } }

        public void AddEnum(string name, IEnumerable<string> values)
        {
            if (!this.enums.ContainsKey(name))
            {
                this.enums.Add(name, new EnumData(name, values));
            }
        }
        
        public ClassData GetOrCreateClass(string name)
        {
            if (!classes.TryGetValue(name, out ClassData cl))
            {
                cl = new ClassData(name);
                classes.Add(name, cl);
            }
            return cl;
        }
    }
}
