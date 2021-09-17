using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.MultiStageTimeSeries")]
    public partial class JenkinsModelMultiStageTimeSeries
    {
        [JsonPropertyName("hour")]
        public JenkinsModelTimeSeries Hour { get; set; }

        [JsonPropertyName("min")]
        public JenkinsModelTimeSeries Min { get; set; }

        [JsonPropertyName("sec10")]
        public JenkinsModelTimeSeries Sec10 { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
