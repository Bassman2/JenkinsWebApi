using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.FreeStyleBuild
    [XmlRoot("freeStyleBuild")]
    public partial class JenkinsFreeStyleBuild : JenkinsBuild
    {
        // empty
    }
}
