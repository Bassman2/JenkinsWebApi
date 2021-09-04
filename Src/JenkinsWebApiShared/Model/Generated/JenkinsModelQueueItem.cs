using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-Item
    public partial class JenkinsModelQueueItem : JenkinsModelActionable
    {
        [JsonPropertyName("blocked")]
        public bool IsBlocked { get; set; }

        [JsonPropertyName("buildable")]
        public bool IsBuildable { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("inQueueSince")]
        public long InQueueSince { get; set; }

        [JsonPropertyName("params")]
        public string Params { get; set; }

        [JsonPropertyName("stuck")]
        public bool IsStuck { get; set; }

        [JsonPropertyName("task")]
        public object Task { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("why")]
        public string Why { get; set; }

    }
}
