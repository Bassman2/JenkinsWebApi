using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.junit.ClassResult")]
    public partial class JenkinsTasksJunitClassResult : JenkinsTasksTestTabulatedResult
    {
        [JsonPropertyName("child")]
        public JenkinsTasksJunitCaseResult[] Childs { get; set; }

        [JsonPropertyName("failCount")]
        public int FailCount { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("passCount")]
        public int PassCount { get; set; }

        [JsonPropertyName("skipCount")]
        public int SkipCount { get; set; }

    }
}
