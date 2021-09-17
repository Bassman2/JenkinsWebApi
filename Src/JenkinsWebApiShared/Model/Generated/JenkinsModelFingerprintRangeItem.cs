using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Fingerprint-RangeItem")]
    public partial class JenkinsModelFingerprintRangeItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ranges")]
        public JenkinsModelFingerprintRangeSet Ranges { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
