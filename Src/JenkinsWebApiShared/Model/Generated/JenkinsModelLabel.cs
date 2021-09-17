using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Label")]
    public partial class JenkinsModelLabel : JenkinsModelActionable
    {
        [JsonPropertyName("busyExecutors")]
        public int BusyExecutors { get; set; }

        [JsonPropertyName("cloud")]
        public JenkinsSlavesCloud[] Clouds { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("idleExecutors")]
        public int IdleExecutors { get; set; }

        [JsonPropertyName("loadStatistics")]
        public JenkinsModelLoadStatistics LoadStatistics { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("node")]
        public JenkinsModelNode[] Nodes { get; set; }

        [JsonPropertyName("offline")]
        public bool IsOffline { get; set; }

        [JsonPropertyName("tiedJob")]
        public JenkinsModelAbstractProject[] TiedJobs { get; set; }

        [JsonPropertyName("totalExecutors")]
        public int TotalExecutors { get; set; }

    }
}
