using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.branch.BranchSource")]
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
