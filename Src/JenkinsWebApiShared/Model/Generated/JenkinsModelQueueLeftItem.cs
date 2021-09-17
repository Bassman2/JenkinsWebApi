using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-LeftItem")]
    public partial class JenkinsModelQueueLeftItem : JenkinsModelQueueItem
    {
        [JsonPropertyName("cancelled")]
        public bool IsCancelled { get; set; }

        [JsonPropertyName("executable")]
        public JenkinsExecutable Executable { get; set; }

    }
}
