using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.FreeStyleProject")]
    [XmlRoot("freeStyleProject")]
    public partial class JenkinsModelFreeStyleProject : JenkinsModelProject
    {
        // empty
    }
}
