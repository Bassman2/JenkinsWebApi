using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.User
    [XmlRoot("user")]
    public partial class JenkinsUser
    {
        [XmlElement("absoluteUrl")]
        public string AbsoluteUrl { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("fullName")]
        public string FullName { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("property")]
        public JenkinsUserProperty[] Propertys { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
