using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.security.csrf.CrumbIssuer
    public partial class JenkinsCrumbIssuer
    {
        [XmlElement("crumb")]
        public string[] Crumbs { get; set; }

        [XmlElement("crumbRequestField")]
        public string[] CrumbRequestFields { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
