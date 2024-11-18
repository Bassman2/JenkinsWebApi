using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.buildgraphview.BuildGraph")]
    [XmlRoot("buildGraph")]
    public partial class JenkinsJenkinsciBuildGraph
    {
        [XmlElement("buildGraph")]
        public string BuildGraph { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
