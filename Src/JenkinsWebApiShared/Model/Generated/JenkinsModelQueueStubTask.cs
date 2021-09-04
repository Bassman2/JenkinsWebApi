using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-StubTask
    public partial class JenkinsModelQueueStubTask
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
