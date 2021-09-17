using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ViewJob")]
    public partial class JenkinsModelViewJob : JenkinsModelJob
    {
        // empty
    }
}
