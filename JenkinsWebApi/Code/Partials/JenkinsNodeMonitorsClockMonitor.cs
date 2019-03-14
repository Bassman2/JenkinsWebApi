using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsClockMonitor
    {
        [XmlElement("diff")]
        public ulong Diff { get; set; }
    }
}
