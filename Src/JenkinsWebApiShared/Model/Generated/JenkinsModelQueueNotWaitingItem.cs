using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-NotWaitingItem
    public partial class JenkinsModelQueueNotWaitingItem : JenkinsModelQueueItem
    {
        [JsonPropertyName("buildableStartMilliseconds")]
        public long BuildableStartMilliseconds { get; set; }

    }
}
