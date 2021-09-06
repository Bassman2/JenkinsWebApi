using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.security.csrf.CrumbIssuer
    public partial class JenkinsSecurityCsrfCrumbIssuer
    {
        [XmlElement("crumb")]
        public string Crumb { get; set; }

        [XmlElement("crumbRequestField")]
        public string CrumbRequestField { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
