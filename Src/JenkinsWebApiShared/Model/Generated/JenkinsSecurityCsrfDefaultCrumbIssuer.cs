using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.security.csrf.DefaultCrumbIssuer")]
    public partial class JenkinsSecurityCsrfDefaultCrumbIssuer : JenkinsSecurityCsrfCrumbIssuer
    {
        // empty
    }
}
