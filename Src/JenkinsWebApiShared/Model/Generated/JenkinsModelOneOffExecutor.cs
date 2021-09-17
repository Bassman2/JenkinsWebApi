using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.OneOffExecutor")]
    public partial class JenkinsModelOneOffExecutor : JenkinsModelExecutor
    {
        // empty
    }
}
