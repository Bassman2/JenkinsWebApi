using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // com.tikal.jenkins.plugins.multijob.MultiJobBuild
    public partial class JenkinsComTikalJenkinsPluginsMultijobMultiJobBuild : JenkinsModelBuild
    {
        [JsonPropertyName("subBuild")]
        public JenkinsComTikalJenkinsPluginsMultijobMultiJobBuildSubBuild[] SubBuilds { get; set; }

    }
}
