using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.job.WorkflowRun
    [XmlRoot("workflowRun")]
    public partial class JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun : JenkinsModelRun
    {
        [XmlElement("changeSet")]
        public JenkinsScmChangeLogSet[] ChangeSets { get; set; }

        [XmlElement("culprit")]
        public JenkinsModelUser[] Culprits { get; set; }

        [XmlElement("nextBuild")]
        public JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun NextBuild { get; set; }

        [XmlElement("previousBuild")]
        public JenkinsOrgJenkinsciPluginsWorkflowJobWorkflowRun PreviousBuild { get; set; }

    }
}
