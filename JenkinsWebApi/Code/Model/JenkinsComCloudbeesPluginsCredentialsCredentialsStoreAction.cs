using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.plugins.credentials.CredentialsStoreAction
    public partial class JenkinsComCloudbeesPluginsCredentialsCredentialsStoreAction
    {
        [XmlElement("domains")]
        public object Domains { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
