using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.SuiteResult
    public partial class JenkinsSuiteResult
    {
        [XmlElement("case")]
        public JenkinsCaseResult[] Cases { get; set; }

        [XmlElement("duration")]
        public object Duration { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("stderr")]
        public string Stderr { get; set; }

        [XmlElement("stdout")]
        public string Stdout { get; set; }

        [XmlElement("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
