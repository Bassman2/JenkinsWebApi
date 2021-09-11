using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.maven.reporters.MavenArtifactRecord
    public partial class JenkinsMavenReportersMavenArtifactRecord
    {
        [XmlElement("attachedArtifact")]
        public JenkinsMavenReportersMavenArtifact[] AttachedArtifacts { get; set; }

        [XmlElement("mainArtifact")]
        public JenkinsMavenReportersMavenArtifact MainArtifact { get; set; }

        [XmlElement("parent")]
        public JenkinsMavenMavenBuild Parent { get; set; }

        [XmlElement("pomArtifact")]
        public JenkinsMavenReportersMavenArtifact PomArtifact { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
