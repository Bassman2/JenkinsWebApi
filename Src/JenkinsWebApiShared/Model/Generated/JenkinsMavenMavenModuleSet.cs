using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenModuleSet")]
    public partial class JenkinsMavenMavenModuleSet : JenkinsMavenAbstractMavenProject
    {
        [JsonPropertyName("module")]
        public JenkinsMavenMavenModule[] Modules { get; set; }

    }
}
