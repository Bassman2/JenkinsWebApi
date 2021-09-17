using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.reporters.MavenAggregatedArtifactRecord")]
    public partial class JenkinsMavenReportersMavenAggregatedArtifactRecord
    {
        [XmlElement("moduleRecord")]
        public JenkinsMavenReportersMavenArtifactRecord[] ModuleRecords { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
