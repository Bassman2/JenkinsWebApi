using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenBuild")]
    public partial class JenkinsMavenMavenBuild : JenkinsMavenAbstractMavenBuild
    {
        [XmlElement("mavenArtifacts")]
        public JenkinsMavenReportersMavenArtifactRecord MavenArtifacts { get; set; }

    }
}
