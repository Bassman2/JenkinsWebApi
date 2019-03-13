using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Label
    public partial class JenkinsLabel : JenkinsActionable
    {
        [XmlElement("busyExecutors")]
        public int BusyExecutors { get; set; }

        [XmlElement("cloud")]
        public JenkinsCloud[] Clouds { get; set; }

        [XmlElement("description")]
        public string[] Descriptions { get; set; }

        [XmlElement("idleExecutors")]
        public int IdleExecutors { get; set; }

        [XmlElement("loadStatistics")]
        public JenkinsLoadStatistics[] LoadStatisticss { get; set; }

        [XmlElement("name")]
        public string[] Names { get; set; }

        [XmlElement("node")]
        public JenkinsNode[] Nodes { get; set; }

        [XmlElement("offline")]
        public bool IsOffline { get; set; }

        [XmlElement("tiedJob")]
        public JenkinsAbstractProject[] TiedJobs { get; set; }

        [XmlElement("totalExecutors")]
        public int TotalExecutors { get; set; }

    }
}
