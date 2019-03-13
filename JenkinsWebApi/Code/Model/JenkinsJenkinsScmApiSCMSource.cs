using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.scm.api.SCMSource
    public partial class JenkinsJenkinsScmApiSCMSource
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
