using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    /// <summary>
    /// Job executable
    /// </summary>
    public class JenkinsExecutable
    {
        /// <summary>
        /// Build number
        /// </summary>
        [XmlElement("number")]
        public int Number { get; set; }

        /// <summary>
        /// Build URL
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
