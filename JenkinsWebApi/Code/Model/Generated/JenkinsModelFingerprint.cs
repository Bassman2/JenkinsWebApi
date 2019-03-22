using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint
    public partial class JenkinsModelFingerprint
    {
        [XmlElement("fileName")]
        public string FileName { get; set; }

        [XmlElement("hash")]
        public string Hash { get; set; }

        [XmlElement("original")]
        public JenkinsModelFingerprintBuildPtr Original { get; set; }

        [XmlElement("timestamp")]
        public object Timestamp { get; set; }

        [XmlElement("usage")]
        public JenkinsModelFingerprintRangeItem[] Usages { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
