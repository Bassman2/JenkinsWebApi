using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.buildgraphview.BuildGraph
    [XmlRoot("buildGraph")]
    public partial class JenkinsOrgJenkinsciPluginsBuildgraphviewBuildGraph
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
