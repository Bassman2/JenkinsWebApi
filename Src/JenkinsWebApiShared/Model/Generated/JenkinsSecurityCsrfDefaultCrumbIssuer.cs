using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.security.csrf.DefaultCrumbIssuer")]
    public partial class JenkinsSecurityCsrfDefaultCrumbIssuer : JenkinsSecurityCsrfCrumbIssuer
    {
        // empty
    }
}
