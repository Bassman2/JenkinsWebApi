using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenBuild")]
    public partial class JenkinsMavenMavenBuild : JenkinsMavenAbstractMavenBuild
    {
        [JsonPropertyName("mavenArtifacts")]
        public JenkinsMavenReportersMavenArtifactRecord MavenArtifacts { get; set; }

    }
}
