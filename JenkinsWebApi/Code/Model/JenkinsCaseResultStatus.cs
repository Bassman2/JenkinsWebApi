using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.CaseResult-Status
    public enum JenkinsCaseResultStatus
    {
        [XmlEnum("PASSED")]
        Passed,

        [XmlEnum("SKIPPED")]
        Skipped,

        [XmlEnum("FAILED")]
        Failed,

        [XmlEnum("FIXED")]
        Fixed,

        [XmlEnum("REGRESSION")]
        Regression,

    }
}
