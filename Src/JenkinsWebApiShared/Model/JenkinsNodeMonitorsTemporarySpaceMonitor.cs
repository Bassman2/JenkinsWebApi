using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsTemporarySpaceMonitor
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }
    }
}
