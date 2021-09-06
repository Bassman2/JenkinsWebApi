using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Hudson
    [XmlRoot("hudson")]
    public partial class JenkinsModelHudson : JenkinsModelJenkins
    {
        // empty
    }
}
