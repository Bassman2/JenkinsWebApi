using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.scm.SCM")]
    public partial class JenkinsScmSCM
    {
        [XmlElement("browser")]
        public JenkinsScmRepositoryBrowser Browser { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
