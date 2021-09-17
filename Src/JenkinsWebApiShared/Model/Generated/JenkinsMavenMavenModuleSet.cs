using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.maven.MavenModuleSet
    public partial class JenkinsMavenMavenModuleSet : JenkinsMavenAbstractMavenProject
    {
        [JsonPropertyName("module")]
        public JenkinsMavenMavenModule[] Modules { get; set; }

    }
}
