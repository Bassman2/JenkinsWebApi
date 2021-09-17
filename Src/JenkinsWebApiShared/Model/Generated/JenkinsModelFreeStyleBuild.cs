using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.FreeStyleBuild")]
    [XmlRoot("freeStyleBuild")]
    public partial class JenkinsModelFreeStyleBuild : JenkinsModelBuild
    {
        // empty
    }
}
