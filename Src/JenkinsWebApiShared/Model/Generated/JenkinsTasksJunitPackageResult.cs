using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.junit.PackageResult")]
    public partial class JenkinsTasksJunitPackageResult : JenkinsTasksTestMetaTabulatedResult
    {
        [JsonPropertyName("child")]
        public JenkinsTasksJunitClassResult[] Childs { get; set; }

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
