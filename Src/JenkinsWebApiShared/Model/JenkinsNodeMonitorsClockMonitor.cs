using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsClockMonitor
    {
        [JsonPropertyName("diff")]
        public long Diff { get; set; }
    }
}
