using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Executor")]
    public partial class JenkinsModelExecutor
    {
        [JsonPropertyName("currentExecutable")]
        public object CurrentExecutable { get; set; }

        [JsonPropertyName("idle")]
        public bool IsIdle { get; set; }

        [JsonPropertyName("likelyStuck")]
        public bool IsLikelyStuck { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("progress")]
        public int Progress { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
