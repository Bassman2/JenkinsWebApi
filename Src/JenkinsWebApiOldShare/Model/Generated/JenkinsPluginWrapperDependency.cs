using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.PluginWrapper-Dependency")]
    public partial class JenkinsPluginWrapperDependency
    {
        [XmlElement("optional")]
        public bool IsOptional { get; set; }

        [XmlElement("shortName")]
        public string ShortName { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
