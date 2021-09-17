using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.buildgraphview.BuildGraph")]
    public partial class JenkinsJenkinsciBuildGraph
    {
        [JsonPropertyName("buildGraph")]
        public string BuildGraph { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
