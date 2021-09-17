using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenModuleSet")]
    public partial class JenkinsMavenMavenModuleSet : JenkinsMavenAbstractMavenProject
    {
        [JsonPropertyName("module")]
        public JenkinsMavenMavenModule[] Modules { get; set; }

    }
}
