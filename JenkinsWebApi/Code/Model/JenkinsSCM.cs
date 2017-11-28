using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.scm.SCM
    public partial class JenkinsSCM
    {
        [XmlElement("browser")]
        public JenkinsRepositoryBrowser Browser { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
