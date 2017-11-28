using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                new Program().Run(Path.GetFullPath(args[0].Trim('"')));
            }
            else
            {
                Console.WriteLine("Generator file.xsd outputFolder");
            }
        }

        public void Run(string dir)
        {
            Parse(dir);
            Create(dir);
        }

        private class Element
        {
            public string Name { get; set; }

            public string Type { get; set; }

            public bool IsList { get; set; }

            public string Description { get; set; }
        }

        private class ComplexType
        {
            public string Name { get; set; }
            public string Base { get; set; }

            public List<Element> Elements { get; set; }

            public bool HasClassAttribut { get; set; }
            public string Root { get; set; }
        }

        private class SimpleType
        {
            public string Name { get; set; }

            public List<string> Values { get; set; }
        }

        private List<ComplexType> complexTypes = new List<ComplexType>();
        private List<SimpleType> simpleTypes = new List<SimpleType>();

        public void Parse(string dir)
        {
            try
            {
                foreach (var file in Directory.EnumerateFiles(Path.Combine(dir, "Schema"), "*.xsd"))
                {

                    string ns = "http://www.w3.org/2001/XMLSchema";

                    XmlTextReader reader = new XmlTextReader(file);
                    XmlNamespaceManager nsm = new XmlNamespaceManager(reader.NameTable);
                    nsm.AddNamespace("xsd", ns);
                    bool has = nsm.HasNamespace("xsd");

                    XmlDocument doc = new XmlDocument();
                    doc.Load(reader);
                    var root = doc.DocumentElement;
                    var element = root.GetElementsByTagName("element", ns).Cast<XmlElement>().First();

                    string elementName = element.GetAttribute("name");
                    string elementType = element.GetAttribute("type");

                    //this.complexType = root.GetElementsByTagName("complexType", ns).Cast<XmlElement>().Select(c =>
                    //    new ComplexType()
                    //    {
                    //        Name = c.GetAttribute("name"),
                    //        Root = (elementType == ct.Name) ? elementName : null,
                    //        Base = (c.SelectSingleNode("xsd:complexContent/xsd:extension", nsm) as XmlElement)?.GetAttribute("base"),
                    //        Elements = c.SelectNodes("element", nsm).Cast<XmlElement>().Select(e =>
                    //            new Element()
                    //            {
                    //                Name = e.GetAttribute("name"),
                    //                Type = e.GetAttribute("type"),
                    //                IsList = e.HasAttribute("maxOccurs"),
                    //                Description = e.SelectSingleNode("documentation")?.InnerText
                    //            }).ToList(),
                    //        HasClassAttribut = c.SelectSingleNode("attribute") != null ? true : false
                    //    }
                    //).ToList();

                    var cts = root.GetElementsByTagName("complexType", ns);
                    foreach (XmlElement c in cts)
                    {
                        ComplexType ct = new ComplexType();
                        ct.Name = c.GetAttribute("name");
                        ct.Root = (elementType == ct.Name) ? elementName : null;
                        var x = c.SelectSingleNode("xsd:complexContent/xsd:extension", nsm) as XmlElement;
                        var y = c.GetElementsByTagName("extension", ns).Cast<XmlElement>().FirstOrDefault();
                        ct.Base = x?.GetAttribute("base");
                        //ct.Elements = c.SelectNodes("xsd:element", nsm).Cast<XmlElement>().Select(e =>
                        ct.Elements = c.GetElementsByTagName("element", ns).Cast<XmlElement>().Select(e =>
                            new Element()
                            {
                                Name = e.GetAttribute("name"),
                                Type = e.GetAttribute("type"),
                                IsList = e.HasAttribute("maxOccurs"),
                                Description = e.SelectSingleNode("xsd:documentation", nsm)?.InnerText
                            }).ToList();
                        ct.HasClassAttribut = c.SelectSingleNode("xsd:attribute", nsm) != null ? true : false;
                        this.complexTypes.Add(ct);
                    }
                    //this.complexType = root.GetElementsByTagName("complexType", ns).Cast<XmlElement>().Select(c =>
                    //    new ComplexType()
                    //    {
                    //        Name = c.GetAttribute("name"),
                    //        Base = (c.SelectSingleNode("extension", nsm) as XmlElement).GetAttribute("base"),
                    //        Elements = c.SelectNodes("element", nsm).Cast<XmlElement>().Select(e =>
                    //            new Element()
                    //            {
                    //                Name = e.GetAttribute("name"),
                    //                Type = e.GetAttribute("type"),
                    //                IsList = e.HasAttribute("maxOccurs"),
                    //                Description = e.SelectSingleNode("documentation")?.InnerText
                    //            }).ToList(),
                    //        HasClassAttribut = c.SelectSingleNode("attribute") != null ? true : false
                    //    }
                    //).ToList();

                    var sts = root.GetElementsByTagName("simpleType", ns);
                    foreach (XmlElement s in sts)
                    {
                        SimpleType st = new SimpleType();
                        st.Name = s.GetAttribute("name");
                        st.Values = s.GetElementsByTagName("enumeration", ns).Cast<XmlElement>().Select(e => e.GetAttribute("value")).ToList();
                        this.simpleTypes.Add(st);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private string LastItem(string item)
        {
            return item.Substring(item.LastIndexOfAny(new char[] { '.' }) + 1).Replace("-", "");
        }

        private string UpperFirstChar(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb[0] = char.ToUpper(sb[0]);
            return sb.ToString();
        }

        private string Format(string str)
        {
            if (str.Contains('_'))
            {
                return str.Split('_').Cast<string>().Select(s => UpperFirstChar(s.ToLower())).Aggregate((a, b) => a + b);
            }
            return UpperFirstChar(str.ToLower());
        }

        private string GetDataType(string xmlType, string name)
        {
            string res = string.Empty;
            switch (xmlType)
            {
            case "xsd:boolean": res = "bool"; break;
            case "xsd:string": res = "string"; break;
            case "xsd:int": res = "int"; break;
            case "xsd:long": res = "long"; break;
            case "xsd:anyType":
                switch (name)
                {
                case "executable": res = "JenkinsExecutor"; break;
                case "job": res = "JenkinsJob"; break;
                case "task": res = "object"; break;
                case "action": res = "object"; break;
                case "result": res = "JenkinsResult"; break;
                case "currentExecutable": res = "object"; break;
                case "property": res = "object"; break;
                case "timestamp": res = "object"; break;
                case "item": res = "object"; break;
                case "history": res = "object"; break;
                case "latest": res = "object"; break;
                case "cloud": res = "object"; break;
                case "stores": res = "object"; break;
                case "duration": res = "object"; break;
                case "dependencies": res = "object"; break;
                case "optionalDependencies": res = "object"; break;
                default:
                    Debug.WriteLine($"##### {name}");
                    res = "object"; break;
                }
                break;
            default: res = "Jenkins" + LastItem(xmlType);

                break;
            }

            return res;

        }

        public void Create(string dir)
        {
            dir = Path.Combine(dir, "Model");
            new DirectoryInfo(dir).GetFiles().ToList().ForEach(f => f.Delete());
            
            foreach(var ct in complexTypes)
            {
                string className = "Jenkins" + LastItem(ct.Name);
                using (StreamWriter writer = File.CreateText(Path.Combine(dir, $"{className}.cs")))
                {
                    writer.WriteLine("using System.Xml.Serialization;");
                    writer.WriteLine();
                    writer.WriteLine("namespace JenkinsWebApi.Model");
                    writer.WriteLine("{");

                    writer.WriteLine($"    // {ct.Name}");

                    if (!string.IsNullOrEmpty(ct.Root))
                    {
                        writer.WriteLine($"    [XmlRoot(\"{ct.Root}\")]");
                    }
                    
                    writer.Write($"    public partial class {className}");
                    writer.WriteLine(ct.Base != null ? $" : Jenkins{LastItem(ct.Base)}" : "");
                    writer.WriteLine("    {");
                    foreach (var e in ct.Elements)
                    {
                        if (!string.IsNullOrEmpty(e.Description))
                        {
                            writer.WriteLine("        /// <summary>");
                            writer.WriteLine($"        /// {e.Description}");
                            writer.WriteLine("        /// </summary>");
                        }
                        writer.WriteLine($"        [XmlElement(\"{e.Name}\")]");
                        if (e.IsList)
                        {
                            writer.WriteLine($"        public {GetDataType(e.Type, e.Name)}[] {UpperFirstChar(e.Name)}s {{ get; set; }}");
                        }
                        else if (e.Type == "xsd:boolean" && !e.Name.StartsWith("use"))
                        {
                            writer.WriteLine($"        public {GetDataType(e.Type, e.Name)} Is{UpperFirstChar(e.Name)} {{ get; set; }}");
                        }
                        else
                        {
                            writer.WriteLine($"        public {GetDataType(e.Type, e.Name)} {UpperFirstChar(e.Name)} {{ get; set; }}");
                        }
                        writer.WriteLine();
                    }
                    if (ct.Elements.Count == 0 && !ct.HasClassAttribut)
                    {
                        writer.WriteLine("        // empty");

                    }
                    if (ct.HasClassAttribut)
                    {
                        writer.WriteLine("        /// <summary>");
                        writer.WriteLine("        /// Jenkins Java class name.");
                        writer.WriteLine("        /// </summary>");
                        writer.WriteLine("        [XmlAttribute(\"_class\")]");
                        writer.WriteLine("        public string Class { get; set; }");
                    }

                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }

            foreach (var st in simpleTypes)
            {
                string className = "Jenkins" + LastItem(st.Name);
                using (StreamWriter writer = File.CreateText(Path.Combine(dir, $"{className}.cs")))
                {
                    writer.WriteLine("using System.Xml.Serialization;");
                    writer.WriteLine();
                    writer.WriteLine("namespace JenkinsWebApi.Model");
                    writer.WriteLine("{");

                    writer.WriteLine($"    // {st.Name}");
                    writer.WriteLine($"    public enum {className}");
                    writer.WriteLine("    {");
                    foreach (var e in st.Values)
                    {
                        writer.WriteLine($"        [XmlEnum(\"{e}\")]");
                        writer.WriteLine($"        {Format(e)},");
                        writer.WriteLine();
                    }
                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }
        }
    }
}




