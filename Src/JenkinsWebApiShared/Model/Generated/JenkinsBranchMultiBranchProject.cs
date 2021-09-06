using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // jenkins.branch.MultiBranchProject
    public partial class JenkinsBranchMultiBranchProject : JenkinsComCloudbeesHudsonPluginsFolderComputedComputedFolder
    {
        [XmlElement("source")]
        public JenkinsBranchBranchSource[] Sources { get; set; }

    }
}
