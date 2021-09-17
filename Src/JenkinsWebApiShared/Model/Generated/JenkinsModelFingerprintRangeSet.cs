using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Fingerprint-RangeSet")]
    public partial class JenkinsModelFingerprintRangeSet
    {
        [XmlElement("range")]
        public JenkinsModelFingerprintRange[] Ranges { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
