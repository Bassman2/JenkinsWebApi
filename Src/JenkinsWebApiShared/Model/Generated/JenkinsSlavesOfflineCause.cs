using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.slaves.OfflineCause
    public partial class JenkinsSlavesOfflineCause
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
