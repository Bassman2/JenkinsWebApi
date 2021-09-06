using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint-Range
    public partial class JenkinsModelFingerprintRange
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
