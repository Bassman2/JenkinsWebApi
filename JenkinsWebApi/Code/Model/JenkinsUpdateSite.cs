using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.UpdateSite
    public partial class JenkinsUpdateSite
    {
        [XmlElement("available")]
        public JenkinsUpdateSitePlugin[] Availables { get; set; }

        [XmlElement("connectionCheckUrl")]
        public string ConnectionCheckUrl { get; set; }

        [XmlElement("dataTimestamp")]
        public long DataTimestamp { get; set; }

        [XmlElement("hasUpdates")]
        public bool IsHasUpdates { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("update")]
        public JenkinsUpdateSitePlugin[] Updates { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
