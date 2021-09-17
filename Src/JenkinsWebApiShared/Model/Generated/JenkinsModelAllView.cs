using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.AllView")]
    public partial class JenkinsModelAllView : JenkinsModelView
    {
        // empty
    }
}
