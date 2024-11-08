using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsArchitectureMonitor
    {
        [XmlText]
        public string Architecture { get; set; }
    }
}
