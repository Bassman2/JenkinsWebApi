using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsResponseTimeMonitor
    {
        [XmlElement("timestamp")]
        public ulong Timestamp { get; set; }

        [XmlElement("average")]
        public ulong Average { get; set; }
    }
}
