using System.Xml.Serialization;
#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsResponseTimeMonitor
    {
        [XmlElement("timestamp")]
        public long Timestamp { get; set; }

        [XmlElement("average")]
        public long Average { get; set; }
    }
}
