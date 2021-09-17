using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Fingerprint")]
    public partial class JenkinsModelFingerprint
    {
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("original")]
        public JenkinsModelFingerprintBuildPtr Original { get; set; }

        [JsonPropertyName("timestamp")]
        public object Timestamp { get; set; }

        [JsonPropertyName("usage")]
        public JenkinsModelFingerprintRangeItem[] Usages { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
