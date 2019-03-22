using System.Xml.Serialization;
#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsResponseTimeMonitor
    {
        [XmlElement("timestamp")]
        public ulong Timestamp { get; set; }

        [XmlElement("average")]
        public ulong Average { get; set; }
    }
}
