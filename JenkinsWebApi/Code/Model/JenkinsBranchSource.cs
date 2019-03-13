using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.branch.BranchSource
    public partial class JenkinsBranchSource
    {
        [XmlElement("source")]
        public JenkinsSCMSource[] Sources { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
