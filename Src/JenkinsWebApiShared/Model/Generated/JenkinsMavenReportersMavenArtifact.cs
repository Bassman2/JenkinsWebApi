using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.maven.reporters.MavenArtifact
    public partial class JenkinsMavenReportersMavenArtifact
    {
        [JsonPropertyName("artifactId")]
        public string ArtifactId { get; set; }

        [JsonPropertyName("canonicalName")]
        public string CanonicalName { get; set; }

        [JsonPropertyName("classifier")]
        public string Classifier { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }

        [JsonPropertyName("md5sum")]
        public string Md5sum { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
