using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.CaseResult-Status
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JenkinsTasksJunitCaseResultStatus
    {
        [EnumMember(Value = "PASSED")]
        Passed,

        [EnumMember(Value = "SKIPPED")]
        Skipped,

        [EnumMember(Value = "FAILED")]
        Failed,

        [EnumMember(Value = "FIXED")]
        Fixed,

        [EnumMember(Value = "REGRESSION")]
        Regression,

    }
}
