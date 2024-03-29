using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.junit.CaseResult")]
    [XmlRoot("caseResult")]
    public partial class JenkinsTasksJunitCaseResult : JenkinsTasksTestTestResult
    {
        [XmlElement("age")]
        public int Age { get; set; }

        [XmlElement("className")]
        public string ClassName { get; set; }

        [XmlElement("duration")]
        public object Duration { get; set; }

        [XmlElement("errorDetails")]
        public string ErrorDetails { get; set; }

        [XmlElement("errorStackTrace")]
        public string ErrorStackTrace { get; set; }

        [XmlElement("failedSince")]
        public int FailedSince { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("skipped")]
        public bool IsSkipped { get; set; }

        [XmlElement("skippedMessage")]
        public string SkippedMessage { get; set; }

        [XmlElement("status")]
        public JenkinsTasksJunitCaseResultStatus Status { get; set; }

        [XmlElement("stderr")]
        public string Stderr { get; set; }

        [XmlElement("stdout")]
        public string Stdout { get; set; }

    }
}
