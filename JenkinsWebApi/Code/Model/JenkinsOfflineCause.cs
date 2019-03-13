using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.slaves.OfflineCause
    public partial class JenkinsOfflineCause
    {
        [XmlElement("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
