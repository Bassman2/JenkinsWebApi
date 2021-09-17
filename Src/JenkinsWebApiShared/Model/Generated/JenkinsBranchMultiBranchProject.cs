using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.branch.MultiBranchProject")]
    public partial class JenkinsBranchMultiBranchProject : JenkinsCloudbeesComputedComputedFolder
    {
        [XmlElement("source")]
        public JenkinsBranchBranchSource[] Sources { get; set; }

    }
}
