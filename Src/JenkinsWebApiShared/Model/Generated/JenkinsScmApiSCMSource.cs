using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.scm.api.SCMSource
    public partial class JenkinsScmApiSCMSource
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
