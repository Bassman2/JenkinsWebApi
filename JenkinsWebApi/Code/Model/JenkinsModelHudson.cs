using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Hudson
    [XmlRoot("hudson")]
    public partial class JenkinsModelHudson : JenkinsJenkinsModelJenkins
    {
        // empty
    }
}
