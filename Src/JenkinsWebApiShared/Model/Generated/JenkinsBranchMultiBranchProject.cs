using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.branch.MultiBranchProject
    public partial class JenkinsBranchMultiBranchProject : JenkinsCloudbeesComputedComputedFolder
    {
        [JsonPropertyName("source")]
        public JenkinsBranchBranchSource[] Sources { get; set; }

    }
}
