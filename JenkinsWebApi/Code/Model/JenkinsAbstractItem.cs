using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractItem
    public partial class JenkinsAbstractItem : JenkinsActionable
    {
        [XmlElement("description")]
        public string[] Descriptions { get; set; }

        [XmlElement("displayName")]
        public string[] DisplayNames { get; set; }

        [XmlElement("displayNameOrNull")]
        public string[] DisplayNameOrNulls { get; set; }

        [XmlElement("fullDisplayName")]
        public string[] FullDisplayNames { get; set; }

        [XmlElement("fullName")]
        public string[] FullNames { get; set; }

        [XmlElement("name")]
        public string[] Names { get; set; }

        [XmlElement("url")]
        public string[] Urls { get; set; }

    }
}
