using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsClockMonitor
    {
        [XmlElement("diff")]
        public ulong Diff { get; set; }
    }
}
