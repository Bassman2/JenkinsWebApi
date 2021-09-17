using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.OverallLoadStatistics")]
    public partial class JenkinsModelOverallLoadStatistics : JenkinsModelLoadStatistics
    {
        [JsonPropertyName("totalQueueLength")]
        public JenkinsModelMultiStageTimeSeries TotalQueueLength { get; set; }

    }
}
