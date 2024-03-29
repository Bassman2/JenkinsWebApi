using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Run-Artifact")]
    public partial class JenkinsModelRunArtifact
    {
        [XmlElement("displayPath")]
        public string DisplayPath { get; set; }

        [XmlElement("fileName")]
        public string FileName { get; set; }

        [XmlElement("relativePath")]
        public string RelativePath { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
