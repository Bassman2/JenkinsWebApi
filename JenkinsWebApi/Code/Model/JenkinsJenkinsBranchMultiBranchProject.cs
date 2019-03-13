using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.branch.MultiBranchProject
    public partial class JenkinsJenkinsBranchMultiBranchProject : JenkinsComCloudbeesHudsonPluginsFolderComputedComputedFolder
    {
        [XmlElement("source")]
        public JenkinsJenkinsBranchBranchSource[] Sources { get; set; }

    }
}
