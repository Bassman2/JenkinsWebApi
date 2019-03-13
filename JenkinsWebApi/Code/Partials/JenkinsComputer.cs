using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public partial class JenkinsComputer
    {
        [XmlArray("monitorData")]
        [XmlArrayItem("hudson.node_monitors.SwapSpaceMonitor", typeof(JenkinsSwapSpaceMonitor))]
        [XmlArrayItem("hudson.node_monitors.TemporarySpaceMonitor ", typeof(JenkinsTemporarySpaceMonitor))]
        [XmlArrayItem("hudson.node_monitors.DiskSpaceMonitor", typeof(JenkinsDiskSpaceMonitor))]
        [XmlArrayItem("hudson.node_monitors.ArchitectureMonitor", typeof(JenkinsArchitectureMonitor))]
        [XmlArrayItem("hudson.node_monitors.ResponseTimeMonitor ", typeof(JenkinsResponseTimeMonitor))]
        [XmlArrayItem("hudson.node_monitors.ClockMonitor ", typeof(JenkinsClockMonitor))]
        public object[] MonitorData { get; set; }
    }
}
