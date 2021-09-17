using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-BuildableItem")]
    public partial class JenkinsModelQueueBuildableItem : JenkinsModelQueueNotWaitingItem
    {
        [JsonPropertyName("pending")]
        public bool IsPending { get; set; }

    }
}
