using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.TestResult
    [XmlRoot("testResult")]
    public partial class JenkinsTasksJunitTestResult : JenkinsTasksTestMetaTabulatedResult
    {
        [XmlElement("duration")]
        public object Duration { get; set; }

        [XmlElement("empty")]
        public bool IsEmpty { get; set; }

        [XmlElement("failCount")]
        public int FailCount { get; set; }

        [XmlElement("passCount")]
        public int PassCount { get; set; }

        [XmlElement("skipCount")]
        public int SkipCount { get; set; }

        [XmlElement("suite")]
        public JenkinsTasksJunitSuiteResult[] Suites { get; set; }

    }
}
