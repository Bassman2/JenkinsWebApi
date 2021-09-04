using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    public class JenkinsNodeMonitorsSwapSpaceMonitor
    {
        [JsonPropertyName("availablePhysicalMemory")]
        public ulong AvailablePhysicalMemory { get; set; }

        [JsonPropertyName("availableSwapSpace")]
        public ulong AvailableSwapSpace { get; set; }

        [JsonPropertyName("totalPhysicalMemory")]
        public ulong TotalPhysicalMemory { get; set; }

        [JsonPropertyName("totalSwapSpace")]
        public ulong TotalSwapSpace { get; set; }
    }
}
