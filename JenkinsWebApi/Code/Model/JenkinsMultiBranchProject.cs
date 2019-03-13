using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.branch.MultiBranchProject
    public partial class JenkinsMultiBranchProject : JenkinsComputedFolder
    {
        [XmlElement("source")]
        public JenkinsBranchSource[] Sources { get; set; }

    }
}
