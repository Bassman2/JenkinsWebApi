using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-StubItem
    public partial class JenkinsQueueStubItem
    {
        [XmlElement("task")]
        public JenkinsQueueStubTask Task { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
