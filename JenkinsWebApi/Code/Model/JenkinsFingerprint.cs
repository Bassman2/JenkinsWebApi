using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint
    public partial class JenkinsFingerprint
    {
        [XmlElement("fileName")]
        public string[] FileNames { get; set; }

        [XmlElement("hash")]
        public string[] Hashs { get; set; }

        [XmlElement("original")]
        public JenkinsFingerprintBuildPtr[] Originals { get; set; }

        [XmlElement("timestamp")]
        public object[] Timestamps { get; set; }

        [XmlElement("usage")]
        public JenkinsFingerprintRangeItem[] Usages { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
