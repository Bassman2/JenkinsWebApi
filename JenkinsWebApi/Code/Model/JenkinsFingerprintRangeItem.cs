using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint-RangeItem
    public partial class JenkinsFingerprintRangeItem
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("ranges")]
        public JenkinsFingerprintRangeSet Ranges { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
