using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue
    [XmlRoot("queue")]
    public partial class JenkinsQueue
    {
        [XmlElement("discoverableItem")]
        public JenkinsQueueStubItem[] DiscoverableItems { get; set; }

        [XmlElement("item")]
        public JenkinsQueueItem[] Items { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
