using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.plugins.credentials.ViewCredentialsAction")]
    public partial class JenkinsCloudbeesViewCredentialsAction
    {
        [XmlElement("stores")]
        public object Stores { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
