using System.Xml.Serialization;
#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsTemporarySpaceMonitor
    {
        [XmlElement("timestamp")]
        public long Timestamp { get; set; }

        [XmlElement("path")]
        public string Path { get; set; }

        [XmlElement("size")]
        public long Size { get; set; }
    }
}
