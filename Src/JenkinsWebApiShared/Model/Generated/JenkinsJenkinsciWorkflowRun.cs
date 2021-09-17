using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.workflow.job.WorkflowRun")]
    public partial class JenkinsJenkinsciWorkflowRun : JenkinsModelRun
    {
        [JsonPropertyName("changeSet")]
        public JenkinsScmChangeLogSet[] ChangeSets { get; set; }

        [JsonPropertyName("culprit")]
        public JenkinsModelUser[] Culprits { get; set; }

        [JsonPropertyName("nextBuild")]
        public JenkinsJenkinsciWorkflowRun NextBuild { get; set; }

        [JsonPropertyName("previousBuild")]
        public JenkinsJenkinsciWorkflowRun PreviousBuild { get; set; }

    }
}
