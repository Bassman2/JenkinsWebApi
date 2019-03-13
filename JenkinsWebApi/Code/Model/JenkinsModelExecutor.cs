using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Executor
    public partial class JenkinsModelExecutor
    {
        [XmlElement("currentExecutable")]
        public object CurrentExecutable { get; set; }

        [XmlElement("idle")]
        public bool IsIdle { get; set; }

        [XmlElement("likelyStuck")]
        public bool IsLikelyStuck { get; set; }

        [XmlElement("number")]
        public int Number { get; set; }

        [XmlElement("progress")]
        public int Progress { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
