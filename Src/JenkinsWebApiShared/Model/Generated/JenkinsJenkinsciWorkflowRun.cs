using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowRun
    [XmlRoot("workflowRun")]
    public partial class JenkinsJenkinsciWorkflowRun : JenkinsModelRun
    {
        [XmlElement("changeSet")]
        public JenkinsScmChangeLogSet[] ChangeSets { get; set; }

        [XmlElement("culprit")]
        public JenkinsModelUser[] Culprits { get; set; }

        [XmlElement("nextBuild")]
        public JenkinsJenkinsciWorkflowRun NextBuild { get; set; }

        [XmlElement("previousBuild")]
        public JenkinsJenkinsciWorkflowRun PreviousBuild { get; set; }

    }
}
