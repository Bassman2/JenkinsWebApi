using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Label
    public partial class JenkinsModelLabel : JenkinsModelActionable
    {
        [XmlElement("busyExecutors")]
        public int BusyExecutors { get; set; }

        [XmlElement("cloud")]
        public JenkinsSlavesCloud[] Clouds { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("idleExecutors")]
        public int IdleExecutors { get; set; }

        [XmlElement("loadStatistics")]
        public JenkinsModelLoadStatistics LoadStatistics { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("node")]
        public JenkinsModelNode[] Nodes { get; set; }

        [XmlElement("offline")]
        public bool IsOffline { get; set; }

        [XmlElement("tiedJob")]
        public JenkinsModelAbstractProject[] TiedJobs { get; set; }

        [XmlElement("totalExecutors")]
        public int TotalExecutors { get; set; }

    }
}
