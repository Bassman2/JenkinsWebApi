using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.workflow.job.WorkflowJob")]
    [XmlRoot("workflowJob")]
    public partial class JenkinsJenkinsciWorkflowJob : JenkinsModelJob
    {
        [XmlElement("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

        [XmlElement("resumeBlocked")]
        public bool IsResumeBlocked { get; set; }

    }
}
