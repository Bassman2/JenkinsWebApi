using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowRun
    [XmlRoot("workflowRun")]
    public partial class JenkinsWorkflowRun : JenkinsRun
    {
        [XmlElement("changeSet")]
        public JenkinsChangeLogSet[] ChangeSets { get; set; }

        [XmlElement("culprit")]
        public JenkinsUser[] Culprits { get; set; }

        [XmlElement("nextBuild")]
        public JenkinsWorkflowRun[] NextBuilds { get; set; }

        [XmlElement("previousBuild")]
        public JenkinsWorkflowRun[] PreviousBuilds { get; set; }

    }
}
