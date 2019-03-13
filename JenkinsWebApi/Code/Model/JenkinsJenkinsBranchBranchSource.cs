using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.branch.BranchSource
    public partial class JenkinsJenkinsBranchBranchSource
    {
        [XmlElement("source")]
        public JenkinsJenkinsScmApiSCMSource Source { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
