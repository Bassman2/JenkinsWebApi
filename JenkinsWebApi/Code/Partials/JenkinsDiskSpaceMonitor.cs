using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsDiskSpaceMonitor
    {
        [XmlElement("timestamp")]
        public ulong Timestamp { get; set; }

        [XmlElement("path")]
        public string Path { get; set; }

        [XmlElement("size")]
        public ulong Size { get; set; }
    }
}
