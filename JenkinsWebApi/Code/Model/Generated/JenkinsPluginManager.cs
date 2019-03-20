using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.PluginManager
    public partial class JenkinsPluginManager
    {
        [XmlElement("plugin")]
        public JenkinsPluginWrapper[] Plugins { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
