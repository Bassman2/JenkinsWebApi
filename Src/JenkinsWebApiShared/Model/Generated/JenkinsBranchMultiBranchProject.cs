using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.branch.MultiBranchProject")]
    public partial class JenkinsBranchMultiBranchProject : JenkinsCloudbeesComputedComputedFolder
    {
        [JsonPropertyName("source")]
        public JenkinsBranchBranchSource[] Sources { get; set; }

    }
}
