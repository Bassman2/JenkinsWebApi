using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenModuleSetBuild")]
    [XmlRoot("mavenModuleSetBuild")]
    public partial class JenkinsMavenMavenModuleSetBuild : JenkinsMavenAbstractMavenBuild
    {
        [XmlElement("mavenArtifacts")]
        public JenkinsMavenReportersMavenAggregatedArtifactRecord MavenArtifacts { get; set; }

        [XmlElement("mavenVersionUsed")]
        public string MavenVersionUsed { get; set; }

    }
}
