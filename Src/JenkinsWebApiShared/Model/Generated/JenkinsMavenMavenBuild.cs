using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.maven.MavenBuild
    public partial class JenkinsMavenMavenBuild : JenkinsMavenAbstractMavenBuild
    {
        [JsonPropertyName("mavenArtifacts")]
        public JenkinsMavenReportersMavenArtifactRecord MavenArtifacts { get; set; }

    }
}
