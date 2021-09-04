using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.SuiteResult
    public partial class JenkinsTasksJunitSuiteResult
    {
        [JsonPropertyName("case")]
        public JenkinsTasksJunitCaseResult[] Cases { get; set; }

        [JsonPropertyName("duration")]
        public object Duration { get; set; }

        [JsonPropertyName("enclosingBlockName")]
        public string[] EnclosingBlockNames { get; set; }

        [JsonPropertyName("enclosingBlock")]
        public string[] EnclosingBlocks { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; }

        [JsonPropertyName("stderr")]
        public string Stderr { get; set; }

        [JsonPropertyName("stdout")]
        public string Stdout { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
