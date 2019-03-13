using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.ClassResult
    public partial class JenkinsClassResult : JenkinsTabulatedResult
    {
        [XmlElement("child")]
        public JenkinsCaseResult[] Childs { get; set; }

        [XmlElement("failCount")]
        public int FailCount { get; set; }

        [XmlElement("name")]
        public string[] Names { get; set; }

        [XmlElement("passCount")]
        public int PassCount { get; set; }

        [XmlElement("skipCount")]
        public int SkipCount { get; set; }

    }
}
