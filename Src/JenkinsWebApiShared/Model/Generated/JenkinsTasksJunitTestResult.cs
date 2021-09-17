using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.junit.TestResult")]
    public partial class JenkinsTasksJunitTestResult : JenkinsTasksTestMetaTabulatedResult
    {
        [JsonPropertyName("duration")]
        public object Duration { get; set; }

        [JsonPropertyName("empty")]
        public bool IsEmpty { get; set; }

        [JsonPropertyName("failCount")]
        public int FailCount { get; set; }

        [JsonPropertyName("passCount")]
        public int PassCount { get; set; }

        [JsonPropertyName("skipCount")]
        public int SkipCount { get; set; }

        [JsonPropertyName("suite")]
        public JenkinsTasksJunitSuiteResult[] Suites { get; set; }

    }
}
