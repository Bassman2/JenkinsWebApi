using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class ClassData
    {
        public ClassData(string name)
        {
            this.Name = name;
            this.ClassName = Converter.JenkinsToClassName(name);
        }
        private string baseName;

        public string Name { get; }
        public string ClassName { get; }

        public string BaseName
        {
            get { return this.baseName; }
            set { this.baseName = value; this.ClassBaseName = Converter.JenkinsToClassName(value); }
        }

        public string ClassBaseName { get; private set; }

        public List<ClassItem> Items { get; set; }

        public bool HasClassAttribut { get; set; }
        public string Root { get; set; }
    }
}
