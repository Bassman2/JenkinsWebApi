using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.branch.MultiBranchProject
    public partial class JenkinsBranchMultiBranchProject : JenkinsComCloudbeesHudsonPluginsFolderComputedComputedFolder
    {
        [JsonPropertyName("source")]
        public JenkinsBranchBranchSource[] Sources { get; set; }

    }
}
