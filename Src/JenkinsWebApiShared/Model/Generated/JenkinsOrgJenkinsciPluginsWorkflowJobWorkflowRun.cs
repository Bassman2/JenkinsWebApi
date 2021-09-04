using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowRun
    public partial class JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun : JenkinsModelRun
    {
        [JsonPropertyName("changeSet")]
        public JenkinsScmChangeLogSet[] ChangeSets { get; set; }

        [JsonPropertyName("culprit")]
        public JenkinsModelUser[] Culprits { get; set; }

        [JsonPropertyName("nextBuild")]
        public JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun NextBuild { get; set; }

        [JsonPropertyName("previousBuild")]
        public JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun PreviousBuild { get; set; }

    }
}
