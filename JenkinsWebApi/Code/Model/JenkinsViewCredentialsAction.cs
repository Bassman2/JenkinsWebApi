using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.plugins.credentials.ViewCredentialsAction
    public partial class JenkinsViewCredentialsAction
    {
        [XmlElement("stores")]
        public object[] Storess { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
