using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint-BuildPtr
    public partial class JenkinsModelFingerprintBuildPtr
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
