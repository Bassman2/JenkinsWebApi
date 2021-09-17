using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-StubItem")]
    public partial class JenkinsModelQueueStubItem
    {
        [XmlElement("task")]
        public JenkinsModelQueueStubTask Task { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
