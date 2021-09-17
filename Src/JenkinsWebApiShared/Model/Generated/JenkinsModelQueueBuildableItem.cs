using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-BuildableItem
    public partial class JenkinsModelQueueBuildableItem : JenkinsModelQueueNotWaitingItem
    {
        [JsonPropertyName("pending")]
        public bool IsPending { get; set; }

    }
}
