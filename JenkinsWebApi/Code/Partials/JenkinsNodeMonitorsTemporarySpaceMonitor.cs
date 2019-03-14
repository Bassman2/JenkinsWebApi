using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsTemporarySpaceMonitor
    {
        [XmlElement("timestamp")]
        public ulong Timestamp { get; set; }

        [XmlElement("path")]
        public string Path { get; set; }

        [XmlElement("size")]
        public ulong Size { get; set; }
    }
}
