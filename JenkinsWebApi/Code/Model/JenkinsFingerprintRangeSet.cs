using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint-RangeSet
    public partial class JenkinsFingerprintRangeSet
    {
        [XmlElement("range")]
        public JenkinsFingerprintRange[] Ranges { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
