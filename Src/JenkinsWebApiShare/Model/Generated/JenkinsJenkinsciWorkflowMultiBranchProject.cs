using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.workflow.multibranch.WorkflowMultiBranchProject")]
    [XmlRoot("workflowMultiBranchProject")]
    public partial class JenkinsJenkinsciWorkflowMultiBranchProject : JenkinsBranchMultiBranchProject
    {
        // empty
    }
}
