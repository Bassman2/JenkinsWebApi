using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.maven.MavenModuleSet
    [XmlRoot("mavenModuleSet")]
    public partial class JenkinsMavenMavenModuleSet : JenkinsMavenAbstractMavenProject
    {
        [XmlElement("module")]
        public JenkinsMavenMavenModule[] Modules { get; set; }

    }
}
