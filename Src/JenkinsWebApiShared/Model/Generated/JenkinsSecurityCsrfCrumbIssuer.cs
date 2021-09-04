using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.security.csrf.CrumbIssuer
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
