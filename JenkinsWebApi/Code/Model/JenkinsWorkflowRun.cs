using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowRun
    [XmlRoot("workflowRun")]
    public partial class JenkinsWorkflowRun : JenkinsRun
    {
        [XmlElement("changeSet")]
        public JenkinsChangeLogSet[] ChangeSets { get; set; }

        [XmlElement("nextBuild")]
        public JenkinsWorkflowRun NextBuild { get; set; }

        [XmlElement("previousBuild")]
        public JenkinsWorkflowRun PreviousBuild { get; set; }

    }
}
