using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowJob
    public partial class JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowJob : JenkinsModelJob
    {
        [JsonPropertyName("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

        [JsonPropertyName("resumeBlocked")]
        public bool IsResumeBlocked { get; set; }

    }
}
