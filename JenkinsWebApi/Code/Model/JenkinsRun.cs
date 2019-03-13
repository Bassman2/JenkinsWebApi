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
        public string[] Descriptions { get; set; }

        [XmlElement("displayName")]
        public string[] DisplayNames { get; set; }

        [XmlElement("duration")]
        public long Duration { get; set; }

        [XmlElement("estimatedDuration")]
        public long EstimatedDuration { get; set; }

        [XmlElement("executor")]
        public JenkinsExecutor[] Executors { get; set; }

        [XmlElement("fingerprint")]
        public JenkinsFingerprint[] Fingerprints { get; set; }

        [XmlElement("fullDisplayName")]
        public string[] FullDisplayNames { get; set; }

        [XmlElement("id")]
        public string[] Ids { get; set; }

        [XmlElement("keepLog")]
        public bool IsKeepLog { get; set; }

        [XmlElement("number")]
        public int Number { get; set; }

        [XmlElement("queueId")]
        public long QueueId { get; set; }

        [XmlElement("result")]
        public JenkinsResult[] Results { get; set; }

        [XmlElement("timestamp")]
        public long[] Timestamps { get; set; }

        [XmlElement("url")]
        public string[] Urls { get; set; }

    }
}
