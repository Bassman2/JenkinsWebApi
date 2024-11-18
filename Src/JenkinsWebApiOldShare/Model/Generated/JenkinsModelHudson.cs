using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Hudson")]
    [XmlRoot("hudson")]
    public partial class JenkinsModelHudson : JenkinsModelJenkins
    {
        // empty
    }
}
