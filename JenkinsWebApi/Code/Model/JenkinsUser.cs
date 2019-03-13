using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.User
    public partial class JenkinsUser
    {
        [XmlElement("absoluteUrl")]
        public string[] AbsoluteUrls { get; set; }

        [XmlElement("description")]
        public string[] Descriptions { get; set; }

        [XmlElement("fullName")]
        public string[] FullNames { get; set; }

        [XmlElement("id")]
        public string[] Ids { get; set; }

        [XmlElement("property")]
        public JenkinsUserProperty[] Propertys { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
