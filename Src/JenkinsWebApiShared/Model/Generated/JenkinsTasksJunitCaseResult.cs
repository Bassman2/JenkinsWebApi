using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.CaseResult
    public partial class JenkinsTasksJunitCaseResult : JenkinsTasksTestTestResult
    {
        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("className")]
        public string ClassName { get; set; }

        [JsonPropertyName("duration")]
        public object Duration { get; set; }

        [JsonPropertyName("errorDetails")]
        public string ErrorDetails { get; set; }

        [JsonPropertyName("errorStackTrace")]
        public string ErrorStackTrace { get; set; }

        [JsonPropertyName("failedSince")]
        public int FailedSince { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("skipped")]
        public bool IsSkipped { get; set; }

        [JsonPropertyName("skippedMessage")]
        public string SkippedMessage { get; set; }

        [JsonPropertyName("status")]
        public JenkinsTasksJunitCaseResultStatus Status { get; set; }

        [JsonPropertyName("stderr")]
        public string Stderr { get; set; }

        [JsonPropertyName("stdout")]
        public string Stdout { get; set; }

    }
}
