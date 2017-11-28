using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.security.csrf.DefaultCrumbIssuer
    [XmlRoot("defaultCrumbIssuer")]
    public partial class JenkinsDefaultCrumbIssuer : JenkinsCrumbIssuer
    {
        // empty
    }
}
