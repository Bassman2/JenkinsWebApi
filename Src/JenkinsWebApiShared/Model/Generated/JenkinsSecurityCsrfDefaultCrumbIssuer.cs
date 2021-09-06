using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.security.csrf.DefaultCrumbIssuer
    [XmlRoot("defaultCrumbIssuer")]
    public partial class JenkinsSecurityCsrfDefaultCrumbIssuer : JenkinsSecurityCsrfCrumbIssuer
    {
        // empty
    }
}
