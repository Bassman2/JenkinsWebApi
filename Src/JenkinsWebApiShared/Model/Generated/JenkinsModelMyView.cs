using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.MyView")]
    public partial class JenkinsModelMyView : JenkinsModelView
    {
        // empty
    }
}
