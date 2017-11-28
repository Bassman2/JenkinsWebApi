using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.queue.WorkUnit
    public partial class JenkinsWorkUnit
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
