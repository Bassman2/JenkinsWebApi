using System.Xml.Serialization;

#pragma warning disable CS1591

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
