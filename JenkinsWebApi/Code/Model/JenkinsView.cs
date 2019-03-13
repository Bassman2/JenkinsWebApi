using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.View
    public partial class JenkinsView
    {
        [XmlElement("description")]
        public string[] Descriptions { get; set; }

        [XmlElement("job")]
        public JenkinsJob[] Jobs { get; set; }

        [XmlElement("name")]
        public string[] Names { get; set; }

        [XmlElement("property")]
        public object[] Propertys { get; set; }

        [XmlElement("url")]
        public string[] Urls { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
