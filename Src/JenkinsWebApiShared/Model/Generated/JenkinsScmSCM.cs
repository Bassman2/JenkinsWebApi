using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.scm.SCM
    public partial class JenkinsScmSCM
    {
        [JsonPropertyName("browser")]
        public JenkinsScmRepositoryBrowser Browser { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
