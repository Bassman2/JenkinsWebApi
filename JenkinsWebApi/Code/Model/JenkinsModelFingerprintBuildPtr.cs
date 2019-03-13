using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Fingerprint-BuildPtr
    public partial class JenkinsModelFingerprintBuildPtr
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("number")]
        public int Number { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
