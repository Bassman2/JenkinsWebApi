using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.branch.OrganizationFolder")]
    [XmlRoot("organizationFolder")]
    public partial class JenkinsBranchOrganizationFolder : JenkinsCloudbeesComputedComputedFolder
    {
        // empty
    }
}
