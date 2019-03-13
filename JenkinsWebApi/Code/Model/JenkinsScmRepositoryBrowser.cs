using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.scm.RepositoryBrowser
    public partial class JenkinsScmRepositoryBrowser
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
