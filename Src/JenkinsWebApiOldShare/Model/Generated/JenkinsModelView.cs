using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.View")]
    public partial class JenkinsModelView
    {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("job")]
        public JenkinsModelJob[] Jobs { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("property")]
        public object[] Propertys { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
