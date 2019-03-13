using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.ClassResult
    public partial class JenkinsTasksJunitClassResult : JenkinsTasksTestTabulatedResult
    {
        [XmlElement("child")]
        public JenkinsTasksJunitCaseResult[] Childs { get; set; }

        [XmlElement("failCount")]
        public int FailCount { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("passCount")]
        public int PassCount { get; set; }

        [XmlElement("skipCount")]
        public int SkipCount { get; set; }

    }
}
