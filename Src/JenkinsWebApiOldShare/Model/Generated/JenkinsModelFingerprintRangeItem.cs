using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Fingerprint-RangeItem")]
    public partial class JenkinsModelFingerprintRangeItem
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("ranges")]
        public JenkinsModelFingerprintRangeSet Ranges { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
