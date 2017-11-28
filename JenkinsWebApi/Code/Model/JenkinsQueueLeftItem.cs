using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-LeftItem
    [XmlRoot("leftItem")]
    public partial class JenkinsQueueLeftItem : JenkinsQueueItem
    {
        [XmlElement("cancelled")]
        public bool IsCancelled { get; set; }

        [XmlElement("executable")]
        public JenkinsExecutor Executable { get; set; }

    }
}
