using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.branch.MultiBranchProject")]
    public partial class JenkinsBranchMultiBranchProject : JenkinsCloudbeesComputedComputedFolder
    {
        [JsonPropertyName("source")]
        public JenkinsBranchBranchSource[] Sources { get; set; }

    }
}
