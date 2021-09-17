using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.reporters.MavenAggregatedArtifactRecord")]
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
