using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ExternalJob")]
    public partial class JenkinsModelExternalJob : JenkinsModelViewJob
    {
        // empty
    }
}
