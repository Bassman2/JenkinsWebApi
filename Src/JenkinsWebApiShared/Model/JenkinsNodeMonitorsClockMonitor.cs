using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsClockMonitor
    {
        [XmlElement("diff")]
        public long Diff { get; set; }
    }
}
