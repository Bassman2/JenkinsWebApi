using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractItem
    public partial class JenkinsModelAbstractItem : JenkinsModelActionable
    {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("displayName")]
        public string DisplayName { get; set; }

        [XmlElement("displayNameOrNull")]
        public string DisplayNameOrNull { get; set; }

        [XmlElement("fullDisplayName")]
        public string FullDisplayName { get; set; }

        [XmlElement("fullName")]
        public string FullName { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

    }
}
