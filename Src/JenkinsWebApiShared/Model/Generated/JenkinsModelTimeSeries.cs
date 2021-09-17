using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.TimeSeries")]
    public partial class JenkinsModelTimeSeries
    {
        [JsonPropertyName("history")]
        public object[] Historys { get; set; }

        [JsonPropertyName("latest")]
        public object Latest { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
