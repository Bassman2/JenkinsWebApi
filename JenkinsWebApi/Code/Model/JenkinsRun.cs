using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Run
    public partial class JenkinsRun : JenkinsActionable
    {
        [XmlElement("artifact")]
        public JenkinsRunArtifact[] Artifacts { get; set; }

        [XmlElement("building")]
        public bool IsBuilding { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("displayName")]
        public string DisplayName { get; set; }

        [XmlElement("duration")]
        public long Duration { get; set; }

        [XmlElement("estimatedDuration")]
        public long EstimatedDuration { get; set; }

        [XmlElement("executor")]
        public JenkinsExecutor Executor { get; set; }

        [XmlElement("fullDisplayName")]
        public string FullDisplayName { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("keepLog")]
        public bool IsKeepLog { get; set; }

        [XmlElement("number")]
        public int Number { get; set; }

        [XmlElement("queueId")]
        public long QueueId { get; set; }

        [XmlElement("result")]
        public JenkinsResult Result { get; set; }

        [XmlElement("timestamp")]
        public long Timestamp { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

    }
}
