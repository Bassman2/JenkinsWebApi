using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace Generator
{
    public static class Parser
    {
        private const string XMLSchemaNamespace = "http://www.w3.org/2001/XMLSchema";
        
        
        public static void Parse(DataBase db, string dir)
        {
            try
            {
                foreach (var file in Directory.EnumerateFiles(dir, "*.xsd"))
                {
                    ParseFile(db, file);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static void ParseFile(DataBase db, string file)
        {
            using XmlReader reader = XmlReader.Create(file);
            XmlNamespaceManager nsm = new XmlNamespaceManager(reader.NameTable);
            nsm.AddNamespace("xsd", XMLSchemaNamespace);

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            var root = doc.DocumentElement;
            if (root.Name != "xsd:schema")
            {
                throw new Exception($"File {file} is not a Schema");
            }

            //-------------------------------------------------------------------------------------
            // find root element
            
            if (root.SelectNodes("xsd:element", nsm).Count != 1)
            {
                throw new Exception($"File {file} has more or less than one base element");
            }
            var element = root.SelectSingleNode("xsd:element", nsm) as XmlElement;
            string elementName = element.GetAttribute("name");
            string elementType = element.GetAttribute("type");

            //-------------------------------------------------------------------------------------
            // find all complexType

            int num = root.SelectNodes("xsd:complexType", nsm).Count;
            var cts = root.GetElementsByTagName("complexType", XMLSchemaNamespace);
            if (num != cts.Count)
            {
                throw new Exception($"File {file} not all complexType are in root");
            }
            foreach (XmlElement c in cts)
            {
                string name = c.GetAttribute("name");

                ClassData ct = db.GetOrCreateClass(name);
               
                if (elementType == ct.Name)
                {
                    ct.Root = elementName;
                }

                //ct.Name = c.GetAttribute("name");
                //ct.Root = (elementType == ct.Name) ? elementName : null;
                var x = c.SelectSingleNode("xsd:complexContent/xsd:extension", nsm) as XmlElement;
                var y = c.GetElementsByTagName("extension", XMLSchemaNamespace).Cast<XmlElement>().FirstOrDefault();

                ct.BaseName = x?.GetAttribute("base");
                ct.Items = c.GetElementsByTagName("element", XMLSchemaNamespace).Cast<XmlElement>().Select(e =>
                    new ClassItem(e.GetAttribute("name"), e.GetAttribute("type"), e.HasAttribute("maxOccurs"), e.SelectSingleNode("xsd:documentation", nsm)?.InnerText)).ToList();
                ct.HasClassAttribut = c.SelectSingleNode("xsd:attribute", nsm) != null ? true : false;
            }

            //-------------------------------------------------------------------------------------
            // parse enums

            var sts = root.GetElementsByTagName("simpleType", XMLSchemaNamespace);
            foreach (XmlElement s in sts)
            {
                db.AddEnum(s.GetAttribute("name"), s.GetElementsByTagName("enumeration", XMLSchemaNamespace).Cast<XmlElement>().Select(e => e.GetAttribute("value")));
            }
        }
    }
}
