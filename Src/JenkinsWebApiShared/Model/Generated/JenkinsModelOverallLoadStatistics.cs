using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.OverallLoadStatistics")]
    public partial class JenkinsModelOverallLoadStatistics : JenkinsModelLoadStatistics
    {
        [JsonPropertyName("totalQueueLength")]
        public JenkinsModelMultiStageTimeSeries TotalQueueLength { get; set; }

    }
}
