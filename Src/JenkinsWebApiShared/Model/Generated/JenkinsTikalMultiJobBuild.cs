using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // com.tikal.jenkins.plugins.multijob.MultiJobBuild
    [XmlRoot("multiJobBuild")]
    public partial class JenkinsTikalMultiJobBuild : JenkinsModelBuild
    {
        [XmlElement("subBuild")]
        public JenkinsTikalMultiJobBuildSubBuild[] SubBuilds { get; set; }

    }
}
