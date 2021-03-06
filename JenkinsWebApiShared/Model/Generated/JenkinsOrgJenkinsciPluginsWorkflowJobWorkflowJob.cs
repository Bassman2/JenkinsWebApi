using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowJob
    [XmlRoot("workflowJob")]
    public partial class JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowJob : JenkinsModelJob
    {
        [XmlElement("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

        [XmlElement("resumeBlocked")]
        public bool IsResumeBlocked { get; set; }

    }
}
