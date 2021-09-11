using System;
using System.IO;

namespace Generator
{
    public static class Creator
    {
        public static void Create(DataBase db, string dir, APIType apiType)
        {
            CreateEnums(db, dir, apiType);
            CreateClasses(db, dir, apiType);
        }

        public static void CreateEnums(DataBase db, string dir, APIType apiType)
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
                    switch (apiType)
                    {
                    case APIType.XML:
                        writer.WriteLine("using System.Xml.Serialization;");
                        writer.WriteLine();
                        writer.WriteLine("#pragma warning disable CS1591");
                        break;
                    case APIType.JSON:
                        writer.WriteLine("using System.Runtime.Serialization;");
                        writer.WriteLine("using System.Text.Json.Serialization;");
                        break;
                    }
                    writer.WriteLine();
                    writer.WriteLine("namespace JenkinsWebApi.Model");
                    writer.WriteLine("{");

                    writer.WriteLine($"    // {st.Name}");
                    if (apiType == APIType.JSON)
                    {
                        writer.WriteLine("    [JsonConverter(typeof(JsonStringEnumConverter))]");
                    }
                    writer.WriteLine($"    public enum {st.ClassName}");
                    writer.WriteLine("    {");
                    foreach (var val in st.Items)
                    {
                        switch (apiType)
                        {
                        case APIType.XML:
                            writer.WriteLine($"        [XmlEnum(\"{val.Name}\")]");
                            break;
                        case APIType.JSON:
                            writer.WriteLine($"        [EnumMember(Value = \"{val.Name}\")]");
                            break;
                        }
                        writer.WriteLine($"        {val.ItemName},");
                        writer.WriteLine();
                    }
                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }
        }

        public static void CreateClasses(DataBase db, string dir, APIType apiType)
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
                    switch (apiType)
                    {
                    case APIType.XML:
                        writer.WriteLine("using System.Xml.Serialization;");
                        writer.WriteLine();
                        writer.WriteLine("#pragma warning disable CS1591");
                        break;
                    case APIType.JSON:
                        writer.WriteLine("using System.Text.Json.Serialization;");
                        break;
                    }
                    writer.WriteLine();
                    writer.WriteLine("namespace JenkinsWebApi.Model");
                    writer.WriteLine("{");

                    writer.WriteLine($"    // {ct.Name}");

                    if (apiType == APIType.XML && !string.IsNullOrEmpty(ct.Root))
                    {
                        writer.WriteLine($"    [XmlRoot(\"{ct.Root}\")]");
                    }

                    writer.Write($"    public partial class {ct.ClassName}");
                    writer.WriteLine(ct.HasBase ? $" : {ct.ClassBaseName}" : "");
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
                            switch (apiType)
                            {
                            case APIType.XML:
                                writer.WriteLine($"        [XmlElement(\"{e.Name}\")]");
                                break;
                            case APIType.JSON:
                                writer.WriteLine($"        [JsonPropertyName(\"{e.Name}\")]");
                                break;
                            }
                            
                            switch (e.ItemType)
                            {
                            case ClassItem.ItemTypes.Single:
                                writer.WriteLine($"        public {e.DataType} {e.ItemName} {{ get; set; }}");
                                break;
                            case ClassItem.ItemTypes.Bool:
                                writer.WriteLine($"        public {e.DataType} {e.ItemName} {{ get; set; }}");
                                break;
                            case ClassItem.ItemTypes.List:
                                writer.WriteLine($"        public {e.DataType}[] {e.ItemName} {{ get; set; }}");
                                break;
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
                        switch (apiType)
                        {
                        case APIType.XML:
                            writer.WriteLine("        [XmlAttribute(\"_class\")]");
                            break;
                        case APIType.JSON:
                            writer.WriteLine("        [JsonPropertyName(\"_class\")]");
                            break;
                        }
                        writer.WriteLine("        public string Class { get; set; }");
                    }

                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }
        }
    }
}
