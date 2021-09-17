using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.FreeStyleBuild")]
    public partial class JenkinsModelFreeStyleBuild : JenkinsModelBuild
    {
        // empty
    }
}
