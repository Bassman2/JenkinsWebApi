using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowJob
    [XmlRoot("workflowJob")]
    public partial class JenkinsWorkflowJob : JenkinsJob
    {
        [XmlElement("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

    }
}
