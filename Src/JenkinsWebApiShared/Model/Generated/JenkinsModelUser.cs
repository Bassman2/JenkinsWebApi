using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.User
    [XmlRoot("user")]
    public partial class JenkinsModelUser
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
        public JenkinsModelUserProperty[] Propertys { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
