using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.branch.BranchSource
    public partial class JenkinsBranchBranchSource
    {
        [JsonPropertyName("source")]
        public JenkinsScmApiSCMSource Source { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
