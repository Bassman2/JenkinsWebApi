using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.reporters.MavenArtifactRecord")]
    public partial class JenkinsMavenReportersMavenArtifactRecord
    {
        [JsonPropertyName("attachedArtifact")]
        public JenkinsMavenReportersMavenArtifact[] AttachedArtifacts { get; set; }

        [JsonPropertyName("mainArtifact")]
        public JenkinsMavenReportersMavenArtifact MainArtifact { get; set; }

        [JsonPropertyName("parent")]
        public JenkinsMavenMavenBuild Parent { get; set; }

        [JsonPropertyName("pomArtifact")]
        public JenkinsMavenReportersMavenArtifact PomArtifact { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
