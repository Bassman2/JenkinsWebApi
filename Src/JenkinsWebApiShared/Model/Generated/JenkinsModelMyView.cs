using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.MyView")]
    public partial class JenkinsModelMyView : JenkinsModelView
    {
        // empty
    }
}
