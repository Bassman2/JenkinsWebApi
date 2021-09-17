using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Project")]
    public partial class JenkinsModelProject : JenkinsModelAbstractProject
    {
        // empty
    }
}
