using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenModuleSetBuild")]
    public partial class JenkinsMavenMavenModuleSetBuild : JenkinsMavenAbstractMavenBuild
    {
        [JsonPropertyName("mavenArtifacts")]
        public JenkinsMavenReportersMavenAggregatedArtifactRecord MavenArtifacts { get; set; }

        [JsonPropertyName("mavenVersionUsed")]
        public string MavenVersionUsed { get; set; }

    }
}
