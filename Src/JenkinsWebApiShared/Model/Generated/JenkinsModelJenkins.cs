using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.model.Jenkins")]
    public partial class JenkinsModelJenkins : JenkinsModelAbstractCIBase
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("job")]
        public JenkinsModelJob[] Jobs { get; set; }

        [JsonPropertyName("overallLoad")]
        public JenkinsModelOverallLoadStatistics OverallLoad { get; set; }

        [JsonPropertyName("primaryView")]
        public JenkinsModelView PrimaryView { get; set; }

        [JsonPropertyName("quietDownReason")]
        public string QuietDownReason { get; set; }

        [JsonPropertyName("quietingDown")]
        public bool IsQuietingDown { get; set; }

        [JsonPropertyName("slaveAgentPort")]
        public int SlaveAgentPort { get; set; }

        [JsonPropertyName("unlabeledLoad")]
        public JenkinsModelLoadStatistics UnlabeledLoad { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("useCrumbs")]
        public bool UseCrumbs { get; set; }

        [JsonPropertyName("useSecurity")]
        public bool UseSecurity { get; set; }

        [JsonPropertyName("view")]
        public JenkinsModelView[] Views { get; set; }

    }
}
