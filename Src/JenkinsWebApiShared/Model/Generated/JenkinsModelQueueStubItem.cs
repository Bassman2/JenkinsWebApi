using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-StubItem
    public partial class JenkinsModelQueueStubItem
    {
        [JsonPropertyName("task")]
        public JenkinsModelQueueStubTask Task { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
