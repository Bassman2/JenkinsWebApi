using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.buildgraphview.BuildGraph
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
