using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.reporters.MavenArtifact")]
    public partial class JenkinsMavenReportersMavenArtifact
    {
        [XmlElement("artifactId")]
        public string ArtifactId { get; set; }

        [XmlElement("canonicalName")]
        public string CanonicalName { get; set; }

        [XmlElement("classifier")]
        public string Classifier { get; set; }

        [XmlElement("fileName")]
        public string FileName { get; set; }

        [XmlElement("groupId")]
        public string GroupId { get; set; }

        [XmlElement("md5sum")]
        public string Md5sum { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
