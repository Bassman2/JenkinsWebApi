using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-LeftItem
    [XmlRoot("leftItem")]
    public partial class JenkinsModelQueueLeftItem : JenkinsModelQueueItem
    {
        [XmlElement("cancelled")]
        public bool IsCancelled { get; set; }

        [XmlElement("executable")]
        public JenkinsExecutable Executable { get; set; }

    }
}
