using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.junit.TestResult")]
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
