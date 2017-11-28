using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint
    public partial class JenkinsFingerprint
    {
        [XmlElement("fileName")]
        public string FileName { get; set; }

        [XmlElement("hash")]
        public string Hash { get; set; }

        [XmlElement("original")]
        public JenkinsFingerprintBuildPtr Original { get; set; }

        [XmlElement("timestamp")]
        public object Timestamp { get; set; }

        [XmlElement("usage")]
        public JenkinsFingerprintRangeItem[] Usages { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
