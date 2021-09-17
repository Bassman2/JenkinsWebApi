using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Run")]
    public partial class JenkinsModelRun : JenkinsModelActionable
    {
        [JsonPropertyName("artifact")]
        public JenkinsModelRunArtifact[] Artifacts { get; set; }

        [JsonPropertyName("building")]
        public bool IsBuilding { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("estimatedDuration")]
        public long EstimatedDuration { get; set; }

        [JsonPropertyName("executor")]
        public JenkinsModelExecutor Executor { get; set; }

        [JsonPropertyName("fingerprint")]
        public JenkinsModelFingerprint[] Fingerprints { get; set; }

        [JsonPropertyName("fullDisplayName")]
        public string FullDisplayName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("keepLog")]
        public bool IsKeepLog { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("queueId")]
        public long QueueId { get; set; }

        [JsonPropertyName("result")]
        public JenkinsResult Result { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

    }
}
