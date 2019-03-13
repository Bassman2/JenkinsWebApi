using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.PluginWrapper-Dependency
    public partial class JenkinsPluginWrapperDependency
    {
        [XmlElement("optional")]
        public bool IsOptional { get; set; }

        [XmlElement("shortName")]
        public string[] ShortNames { get; set; }

        [XmlElement("version")]
        public string[] Versions { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
