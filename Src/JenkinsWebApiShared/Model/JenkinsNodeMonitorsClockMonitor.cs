using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsClockMonitor
    {
        [JsonPropertyName("diff")]
        public long Diff { get; set; }
    }
}
