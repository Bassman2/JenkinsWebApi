using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.maven.reporters.MavenAggregatedArtifactRecord
    public partial class JenkinsMavenReportersMavenAggregatedArtifactRecord
    {
        [JsonPropertyName("moduleRecord")]
        public JenkinsMavenReportersMavenArtifactRecord[] ModuleRecords { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
