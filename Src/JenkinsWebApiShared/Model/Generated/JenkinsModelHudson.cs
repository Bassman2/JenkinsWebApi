using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Hudson")]
    public partial class JenkinsModelHudson : JenkinsModelJenkins
    {
        // empty
    }
}
