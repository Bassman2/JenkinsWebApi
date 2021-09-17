using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.FreeStyleProject")]
    public partial class JenkinsModelFreeStyleProject : JenkinsModelProject
    {
        // empty
    }
}
