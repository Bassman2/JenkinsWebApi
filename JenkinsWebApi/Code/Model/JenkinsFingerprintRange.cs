using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint-Range
    public partial class JenkinsFingerprintRange
    {
        [XmlElement("end")]
        public int End { get; set; }

        [XmlElement("start")]
        public int Start { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
