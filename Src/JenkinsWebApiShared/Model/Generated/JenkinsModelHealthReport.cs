using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.HealthReport")]
    public partial class JenkinsModelHealthReport
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("iconClassName")]
        public string IconClassName { get; set; }

        [JsonPropertyName("iconUrl")]
        public string IconUrl { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
