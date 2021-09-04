using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue
    [XmlRoot("queue")]
    public partial class JenkinsModelQueue
    {
        [XmlElement("discoverableItem")]
        public JenkinsModelQueueStubItem[] DiscoverableItems { get; set; }

        [XmlElement("item")]
        public JenkinsModelQueueItem[] Items { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
