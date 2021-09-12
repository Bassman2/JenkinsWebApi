using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Run
    public partial class JenkinsModelRun : JenkinsModelActionable
    {
        [XmlElement("artifact")]
        public JenkinsModelRunArtifact[] Artifacts { get; set; }

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
        public JenkinsModelExecutor Executor { get; set; }

        [XmlElement("fingerprint")]
        public JenkinsModelFingerprint[] Fingerprints { get; set; }

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
