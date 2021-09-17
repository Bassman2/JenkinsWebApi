using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

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
