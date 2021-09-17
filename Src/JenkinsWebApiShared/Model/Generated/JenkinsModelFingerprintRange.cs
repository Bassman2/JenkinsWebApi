using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Fingerprint-Range")]
    public partial class JenkinsModelFingerprintRange
    {
        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
