using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.CaseResult
    public partial class JenkinsCaseResult : JenkinsTestResult
    {
        [XmlElement("age")]
        public int Age { get; set; }

        [XmlElement("className")]
        public string[] ClassNames { get; set; }

        [XmlElement("duration")]
        public object Duration { get; set; }

        [XmlElement("errorDetails")]
        public string[] ErrorDetailss { get; set; }

        [XmlElement("errorStackTrace")]
        public string[] ErrorStackTraces { get; set; }

        [XmlElement("failedSince")]
        public int FailedSince { get; set; }

        [XmlElement("name")]
        public string[] Names { get; set; }

        [XmlElement("skipped")]
        public bool IsSkipped { get; set; }

        [XmlElement("skippedMessage")]
        public string[] SkippedMessages { get; set; }

        [XmlElement("status")]
        public JenkinsCaseResultStatus[] Statuss { get; set; }

        [XmlElement("stderr")]
        public string[] Stderrs { get; set; }

        [XmlElement("stdout")]
        public string[] Stdouts { get; set; }

    }
}
