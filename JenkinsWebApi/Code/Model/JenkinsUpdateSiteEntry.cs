using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.UpdateSite-Entry
    public partial class JenkinsUpdateSiteEntry
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("sourceId")]
        public string SourceId { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
