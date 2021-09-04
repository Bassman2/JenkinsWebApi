using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint-RangeSet
    public partial class JenkinsModelFingerprintRangeSet
    {
        [JsonPropertyName("range")]
        public JenkinsModelFingerprintRange[] Ranges { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
