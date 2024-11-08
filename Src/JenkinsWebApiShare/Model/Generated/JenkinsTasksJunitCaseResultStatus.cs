using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.CaseResult-Status
    public enum JenkinsTasksJunitCaseResultStatus
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
