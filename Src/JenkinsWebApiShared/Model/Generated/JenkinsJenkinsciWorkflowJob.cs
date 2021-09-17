using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.workflow.job.WorkflowJob")]
    public partial class JenkinsJenkinsciWorkflowJob : JenkinsModelJob
    {
        [JsonPropertyName("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

        [JsonPropertyName("resumeBlocked")]
        public bool IsResumeBlocked { get; set; }

    }
}
