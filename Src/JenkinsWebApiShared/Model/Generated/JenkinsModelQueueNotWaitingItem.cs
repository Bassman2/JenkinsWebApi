using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-NotWaitingItem")]
    public partial class JenkinsModelQueueNotWaitingItem : JenkinsModelQueueItem
    {
        [JsonPropertyName("buildableStartMilliseconds")]
        public long BuildableStartMilliseconds { get; set; }

    }
}
