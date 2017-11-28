using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.workflow.multibranch.WorkflowMultiBranchProject
    [XmlRoot("workflowMultiBranchProject")]
    public partial class JenkinsWorkflowMultiBranchProject : JenkinsMultiBranchProject
    {
        // empty
    }
}
