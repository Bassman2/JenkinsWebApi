using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Run-Artifact
    public partial class JenkinsRunArtifact
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
