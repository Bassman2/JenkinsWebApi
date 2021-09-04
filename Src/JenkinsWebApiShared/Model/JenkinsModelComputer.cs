
namespace JenkinsWebApi.Model
{
    
    public partial class JenkinsModelComputer
    {
        /// <summary>
        /// Access to moitors.
        /// </summary>
        //[XmlArray("monitorData")]
        //[XmlArrayItem("hudson.node_monitors.SwapSpaceMonitor", typeof(JenkinsNodeMonitorsSwapSpaceMonitor))]
        //[XmlArrayItem("hudson.node_monitors.TemporarySpaceMonitor", typeof(JenkinsNodeMonitorsTemporarySpaceMonitor))]
        //[XmlArrayItem("hudson.node_monitors.DiskSpaceMonitor", typeof(JenkinsNodeMonitorsDiskSpaceMonitor))]
        //[XmlArrayItem("hudson.node_monitors.ArchitectureMonitor", typeof(JenkinsNodeMonitorsArchitectureMonitor))]
        //[XmlArrayItem("hudson.node_monitors.ResponseTimeMonitor", typeof(JenkinsNodeMonitorsResponseTimeMonitor))]
        //[XmlArrayItem("hudson.node_monitors.ClockMonitor", typeof(JenkinsNodeMonitorsClockMonitor))]
        public object[] MonitorData { get; set; }
    }
}
