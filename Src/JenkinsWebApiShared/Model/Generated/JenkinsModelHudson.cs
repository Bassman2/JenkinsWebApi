using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Hudson")]
    public partial class JenkinsModelHudson : JenkinsModelJenkins
    {
        // empty
    }
}
