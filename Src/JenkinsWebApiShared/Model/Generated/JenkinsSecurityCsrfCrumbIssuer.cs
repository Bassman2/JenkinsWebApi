using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.security.csrf.CrumbIssuer")]
    public partial class JenkinsSecurityCsrfCrumbIssuer
    {
        [JsonPropertyName("crumb")]
        public string Crumb { get; set; }

        [JsonPropertyName("crumbRequestField")]
        public string CrumbRequestField { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
