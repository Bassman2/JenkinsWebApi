using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // com.tikal.jenkins.plugins.multijob.MultiJobBuild
    [XmlRoot("multiJobBuild")]
    public partial class JenkinsComTikalJenkinsPluginsMultijobMultiJobBuild : JenkinsModelBuild
    {
        [XmlElement("subBuild")]
        public JenkinsComTikalJenkinsPluginsMultijobMultiJobBuildSubBuild[] SubBuilds { get; set; }

    }
}
