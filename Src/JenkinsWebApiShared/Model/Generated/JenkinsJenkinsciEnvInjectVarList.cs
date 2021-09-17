using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.envinject.EnvInjectVarList")]
    [XmlRoot("envInjectVarList")]
    public partial class JenkinsJenkinsciEnvInjectVarList
    {
        [XmlElement("envMap")]
        public object EnvMap { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
