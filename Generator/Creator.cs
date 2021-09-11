using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Generator
{
    public static class Creator
    {
        public static void Create(DataBase db, string dir)
        {
            CreateEnums(db, dir);
            CreateClasses(db, dir);
        }

        public static void CreateEnums(DataBase db, string dir)
        {
            foreach (var st in db.Enums)
            {
                string filePath = Path.Combine(dir, $"{st.ClassName}.cs");
                if (File.Exists(filePath))
                {
                    throw new Exception($"File {filePath} already exists");
                }
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine("using System.Xml.Serialization;");
                    writer.WriteLine();
                    writer.WriteLine("#pragma warning disable CS1591");
                    writer.WriteLine();
                    writer.WriteLine("namespace JenkinsWebApi.Model");
                    writer.WriteLine("{");

                    writer.WriteLine($"    // {st.Name}");
                    writer.WriteLine($"    public enum {st.ClassName}");
                    writer.WriteLine("    {");
                    foreach (var val in st.Items)
                    {
                        writer.WriteLine($"        [XmlEnum(\"{val.Name}\")]");
                        writer.WriteLine($"        {val.ItemName},");
                        writer.WriteLine();
                    }
                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }
        }

        public static void CreateClasses(DataBase db, string dir)
        {
            foreach (var ct in db.Classes)
            {
                string filePath = Path.Combine(dir, $"{ct.ClassName}.cs");
                if (File.Exists(filePath))
                {
                    throw new Exception($"File {filePath} already exists");
                }
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine("using System.Xml.Serialization;");
                    writer.WriteLine();
                    writer.WriteLine("#pragma warning disable CS1591");
                    writer.WriteLine();
                    writer.WriteLine("namespace JenkinsWebApi.Model");
                    writer.WriteLine("{");

                    writer.WriteLine($"    // {ct.Name}");

                    if (!string.IsNullOrEmpty(ct.Root))
                    {
                        writer.WriteLine($"    [XmlRoot(\"{ct.Root}\")]");
                    }

                    writer.Write($"    public partial class {ct.ClassName}");
                    writer.WriteLine(ct.BaseName != null ? $" : {Converter.JenkinsToClassName(ct.BaseName)}" : "");
                    writer.WriteLine("    {");
                    foreach (var e in ct.Items)
                    {
                        if (e.Name != "monitorData")
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
                                writer.WriteLine($"        public {GetDataType(e.Type, e.Name)}[] {e.ItemName}s {{ get; set; }}");
                            }
                            else if (e.Type == "xsd:boolean" && !e.Name.StartsWith("use"))
                            {
                                writer.WriteLine($"        public {GetDataType(e.Type, e.Name)} Is{e.ItemName} {{ get; set; }}");
                            }
                            else
                            {
                                writer.WriteLine($"        public {GetDataType(e.Type, e.Name)} {e.ItemName} {{ get; set; }}");
                            }
                            writer.WriteLine();
                        }
                    }
                    if (ct.Items.Count == 0 && !ct.HasClassAttribut)
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
        }

        //private static string Format(string str)
        //{
        //    if (str.Contains('_'))
        //    {
        //        return str.Split('_').Cast<string>().Select(s => UpperFirstChar(s.ToLower())).Aggregate((a, b) => a + b);
        //    }
        //    return UpperFirstChar(str.ToLower());
        //}

        //public static string CreateClassName(string name)
        //{
        //    if (name.StartsWith("hudson."))
        //    {
        //        name = name.Substring(7);
        //    }
        //    if (name.StartsWith("jenkins."))
        //    {
        //        name = name.Substring(8);
        //    }

        //    StringBuilder sb = new StringBuilder(name);

        //    sb.Replace(".a", "A");
        //    sb.Replace(".b", "B");
        //    sb.Replace(".c", "C");
        //    sb.Replace(".d", "D");
        //    sb.Replace(".e", "E");
        //    sb.Replace(".f", "F");
        //    sb.Replace(".g", "G");
        //    sb.Replace(".h", "H");
        //    sb.Replace(".i", "I");
        //    sb.Replace(".j", "J");
        //    sb.Replace(".k", "K");
        //    sb.Replace(".l", "L");
        //    sb.Replace(".m", "M");
        //    sb.Replace(".n", "N");
        //    sb.Replace(".o", "O");
        //    sb.Replace(".p", "P");
        //    sb.Replace(".q", "Q");
        //    sb.Replace(".r", "R");
        //    sb.Replace(".s", "S");
        //    sb.Replace(".t", "T");
        //    sb.Replace(".u", "U");
        //    sb.Replace(".v", "V");
        //    sb.Replace(".w", "W");
        //    sb.Replace(".x", "X");
        //    sb.Replace(".y", "Y");
        //    sb.Replace(".z", "Z");
        //    sb.Replace(".", "");
        //    sb.Replace("-", "");

        //    sb[0] = char.ToUpper(sb[0]);

        //    return "Jenkins" + sb.ToString();
        //}

        //private static string UpperFirstChar(string str)
        //{
        //    StringBuilder sb = new StringBuilder(str);
        //    sb[0] = char.ToUpper(sb[0]);
        //    return sb.ToString();
        //}

        //private string LastItem(string item)
        //{
        //    return item.Substring(item.LastIndexOfAny(new char[] { '.' }) + 1).Replace("-", "");
        //}

        private static string GetDataType(string xmlType, string name)
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
                case "job": res = "JenkinsModelJob"; break;
                case "task": res = "object"; break;
                case "action": res = "object"; break;
                case "result": res = "JenkinsTasksTestTestResult"; break;
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
                case "monitorData": res = "object"; break;
                default:
                    Debug.WriteLine($"##### {name}");
                    res = "object"; break;
                }
                break;
            default:
                res = Converter.JenkinsToClassName(xmlType);
                break;
            }
            return res;
        }
    }
}
