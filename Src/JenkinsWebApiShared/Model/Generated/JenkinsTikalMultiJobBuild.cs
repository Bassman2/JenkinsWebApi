using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.tikal.jenkins.plugins.multijob.MultiJobBuild")]
    public partial class JenkinsTikalMultiJobBuild : JenkinsModelBuild
    {
        [JsonPropertyName("subBuild")]
        public JenkinsTikalMultiJobBuildSubBuild[] SubBuilds { get; set; }

    }
}
