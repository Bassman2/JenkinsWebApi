using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractProject
    public partial class JenkinsModelAbstractProject : JenkinsModelJob
    {
        [JsonPropertyName("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

        [JsonPropertyName("disabled")]
        public bool IsDisabled { get; set; }

        [JsonPropertyName("downstreamProject")]
        public JenkinsModelAbstractProject[] DownstreamProjects { get; set; }

        [JsonPropertyName("labelExpression")]
        public string LabelExpression { get; set; }

        [JsonPropertyName("scm")]
        public JenkinsScmSCM Scm { get; set; }

        [JsonPropertyName("upstreamProject")]
        public JenkinsModelAbstractProject[] UpstreamProjects { get; set; }

    }
}
