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

        [XmlElement("enclosingBlockName")]
        public string[] EnclosingBlockNames { get; set; }

        [XmlElement("enclosingBlock")]
        public string[] EnclosingBlocks { get; set; }

        [XmlElement("id")]
        public string[] Ids { get; set; }

        [XmlElement("name")]
        public string[] Names { get; set; }

        [XmlElement("nodeId")]
        public string[] NodeIds { get; set; }

        [XmlElement("stderr")]
        public string[] Stderrs { get; set; }

        [XmlElement("stdout")]
        public string[] Stdouts { get; set; }

        [XmlElement("timestamp")]
        public string[] Timestamps { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
