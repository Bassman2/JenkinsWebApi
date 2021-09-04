using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsResponseTimeMonitor
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("average")]
        public long Average { get; set; }
    }
}
