using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.CaseResult
    public partial class JenkinsCaseResult : JenkinsTestResult
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
        public JenkinsCaseResultStatus Status { get; set; }

        [XmlElement("stderr")]
        public string Stderr { get; set; }

        [XmlElement("stdout")]
        public string Stdout { get; set; }

    }
}
