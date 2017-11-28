using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-StubTask
    public partial class JenkinsQueueStubTask
    {
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
