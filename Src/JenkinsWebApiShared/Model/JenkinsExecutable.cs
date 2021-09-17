using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    /// <summary>
    /// Job executable
    /// </summary>
    public class JenkinsExecutable
    {
        /// <summary>
        /// Build number
        /// </summary>
        [JsonPropertyName("number")]
        public int Number { get; set; }

        /// <summary>
        /// Build URL
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
