using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsSwapSpaceMonitor
    {
        [XmlElement("availablePhysicalMemory")]
        public ulong AvailablePhysicalMemory { get; set; }

        [XmlElement("availableSwapSpace")]
        public ulong AvailableSwapSpace { get; set; }

        [XmlElement("totalPhysicalMemory")]
        public ulong TotalPhysicalMemory { get; set; }

        [XmlElement("totalSwapSpace")]
        public ulong TotalSwapSpace { get; set; }
    }
}
