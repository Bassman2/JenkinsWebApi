using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.HealthReport
    public partial class JenkinsModelHealthReport
    {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("iconClassName")]
        public string IconClassName { get; set; }

        [XmlElement("iconUrl")]
        public string IconUrl { get; set; }

        [XmlElement("score")]
        public int Score { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
